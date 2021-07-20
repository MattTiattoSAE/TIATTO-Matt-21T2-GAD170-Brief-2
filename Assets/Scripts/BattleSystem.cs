using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Functions to complete:
/// - Do Round
/// - Fight Over
/// </summary>
public class BattleSystem : MonoBehaviour
{
    public DanceTeam teamA,teamB; //References to TeamA and TeamB
    public FightManager fightManager; // References to our FightManager.

    public float battlePrepTime = 2;  // the amount of time we need to wait before a battle starts
    public float fightCompletedWaitTime = 2; // the amount of time we need to wait till a fight/round is completed.
    
    /// <summary>
    /// This occurs every round or every X number of seconds, is the core battle logic/game loop.
    /// </summary>
    /// <returns></returns>
    IEnumerator DoRound()
    {     
        // waits for a float number of seconds....
        yield return new WaitForSeconds(battlePrepTime);

        //checking for no dancers on either team
        if(teamA.allDancers.Count == 0 && teamB.allDancers.Count == 0)
        {
            Debug.LogWarning("DoRound called, no dancers active. DanceTeamInit needs to be completed");
            // This will be called if there are 0 dancers on both teams.

        }
        else if (teamA.activeDancers.Count > 0 && teamB.activeDancers.Count > 0)
        {
            Debug.LogWarning("DoRound called, dancers active");
            //You need to select two random or engineered random characters to fight...so one from team a and one from team b....
            Character dancerA = teamA.activeDancers[Random.Range(0, teamA.activeDancers.Count)];
            Character dancerB = teamB.activeDancers[Random.Range(0, teamB.activeDancers.Count)];
            //We then need to pass in the two selected dancers into fightManager.Fight(CharacterA,CharacterB);
            
            fightManager.Fight(dancerA, dancerB);
            // we could also get fancy here by using the simulate battle first if we wanted to.
        }
        else
        {
            // IF we get to here...then we have a team has won...winner winner chicken dinner.
            DanceTeam winner = null; 

            // Checks both teams activeDancer count to see whether one team has eliminated the other
            if (teamB.activeDancers.Count <= 0)
            {
                winner = teamA;
            }
            if (teamA.activeDancers.Count <= 0)
            {
                winner = teamB;
            }
            Debug.Log(teamA.danceTeamName + " wins!");

            //Enables the win effects, and logs it out to the console.
            winner.EnableWinEffects();
            BattleLog.Log(winner.danceTeamName.ToString() + " wins!", winner.teamColor);

            Debug.Log("Game Over");
          
        }
        yield return null;
    }

    // This is where we can handle what happens when we win or lose.
    public void FightOver(Character winner, Character defeated, float outcome)
    {
        Debug.LogWarning("FightOver called");
        // assign damage...or if you want to restore health if they want that's up to you....might involve the character script.
        if (outcome != 0f)
        {
            winner.DealDamage(0.25f - (outcome % 0.50f));
            float damage = 0.25f - (outcome % 0.50f);
            defeated.DealDamage(0.25f);
            BattleLog.Log("Fight Over! \n" +
                      "Winner: " + winner.charName.GetFullCharacterName() + " takes " + damage + " damage! \n" +
                      "Loser: " + defeated.charName.GetFullCharacterName() + " takes 25 damage!", winner.myTeam.teamColor);
        }
        else
        {
            winner.DealDamage(0.10f);
            defeated.DealDamage(0.10f);
                        BattleLog.Log("Fight Over! \n No Winners! Both of y'all are taking 10 damage!", Color.grey);
        }

        //calling the coroutine so we can put waits in for anims to play
        StartCoroutine(HandleFightOver());
    }

    /// <summary>
    /// Used to Request A round.
    /// </summary>
    public void RequestRound()
    {
        //calling the coroutine so we can put waits in for anims to play
        StartCoroutine(DoRound());
    }

    /// <summary>
    /// Handles the end of a fight and waits to start the next round.
    /// </summary>
    /// <returns></returns>
    IEnumerator HandleFightOver()
    {
        yield return new WaitForSeconds(fightCompletedWaitTime);
        teamA.DisableWinEffects();
        teamB.DisableWinEffects();
        Debug.LogWarning("HandleFightOver called, may need to prepare or clean dancers or teams and checks before doing GameEvents.RequestFighters()");
        RequestRound();
    }
}
