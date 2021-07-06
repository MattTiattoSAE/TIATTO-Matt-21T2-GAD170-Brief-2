using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Functions to complete:
/// - Initial Stats
/// - Return Battle Points
/// - Deal Damage
/// </summary>
public class Character : MonoBehaviour
{
    public CharacterName charName; // This is a reference to an instance of the characters name script.

    [Range(0.0f,1.0f)]
    public float mojoRemaining = 1; // This is the characters hp this is a float 0-100 but is normalised to 0.0 - 1.0;

    [Header("Stats")]
    /// <summary>
    /// Our current level.
    /// </summary>
    public int level;

    /// <summary>
    /// The current amount of xp we have accumulated.
    /// </summary>
    public int currentXp;

    /// <summary>
    /// The amount of xp required to level up.
    /// </summary>
    public int xpThreshold = 10;

    /// <summary>
    /// Our variables used to determine our fighting power.
    /// </summary>
    public int style;
    public int luck;
    public int rhythm;

    /// <summary>
    /// Our physical stats that determine our dancing stats.
    /// </summary>
    public int agility;
    public int intelligence;
    public int strength;

    /// <summary>
    /// Used to determine the conversion of 1 physical stat, to 1 dancing stat.
    /// </summary>
    public float agilityMultiplier = 0.5f;
    public float strengthMultiplier = 1f;
    public float inteligenceMultiplier = 2f;

    /// <summary>
    /// A float used to display what the chance of winning the current fight is.
    /// </summary>
    public float perecentageChanceToWin;


    [Header("Related objects")]
    public DanceTeam myTeam; // This holds a reference to the characters current dance team instance they are assigned to.

    public bool isSelected; // This is used for determining if this character is selected in a battle.

    [SerializeField]
    protected TMPro.TextMeshPro nickText; // This is just a piece of text in Unity,  to display the characters name.
 
    public AnimationController animController; // A reference to the animationController, is used to switch dancing states.

    // This is called once, this then calls Initial Stats function
    void Awake()
    {
        animController = GetComponent<AnimationController>();
        GeneratePhysicalStatsStats(); // we want to generate some physical stats.
        CalculateDancingStats();// using those physical stats we want to generate some dancing stats.
    }

    /// <summary>
    /// This function should set our starting stats of Agility, Strength and Intelligence
    /// to some default RANDOM values.
    /// </summary>
    public void GeneratePhysicalStatsStats()
    {
        Debug.LogWarning("Generate Physical Stats has been called");

        // Let's set up agility, intelligence and strength to some default Random values.
    }

    /// <summary>
    /// This function should set our style, luck and ryhtmn to values
    /// based on our currrent agility,intelligence and strength.
    /// </summary>
    public void CalculateDancingStats()
    {
        Debug.LogWarning("Generate Calculate Dancing Stats has been called");
        // what we want I want is for you to take our physical stats and translate them into our dancing stats,
        // based on the multiplier of that stat as follows:
        // our Style should be based on our Agility.
        // our Rhythm should be based on our Strength.
        // our Luck should be based on our intelligence.
        // hint...your going to need to convert our ints into floats, then back to ints.
    }


    /// <summary>
    /// This is takes in a normalised value i.e. 0.0f - 1.0f, and is used to display our % chance to win.
    /// </summary>
    /// <param name="normalisedValue"></param>
    public void SetPercentageValue(float normalisedValue)
    {
        // Essentially we want to set our percentage to win, to be a percentage using our normalised value (decimal value of a fraction)
        // How can we convert out normalised value into a whole number?

        Debug.LogWarning("SetPercentageValue has been called we probably want to convert our normalised value to a percentage");
    }

    /// <summary>
    /// We probably want to use this to remove some hp (mojo) from our character.....
    /// Then we probably want to check to see if they are dead or not from that damage and return true or false.
    /// </summary>
    public void DealDamage(float amount)
    {
        // we probably want to do a check in here to see if the character is dead or not...
        // if they are we probably want to remove them from their team's active dancer list...sure wish there was a function in their dance team  script we could use for this.
    }

    /// <summary>
    /// Used to generate a number of battle points that is used in combat.
    /// </summary>
    /// <returns></returns>
    public int ReturnDancePowerLevel()
    {
        // We want to design some algorithm that will generate a number of points based off of our luck,style and rythm, we probably want to add some randomness in our calculation too
        // to ensure that there is not always a draw, by default it just returns 0. 
        // If you right click this function and find all references you can see where it is called.
        // Let's also throw in a little randomness in here, so it's not a garunteed win
        Debug.LogWarning("ReturnBattlePoints has been called we probably want to create some battle points based on our stats");
        return 0;
    }

    /// <summary>
    /// A function called when the battle is completed and some xp is to be awarded.
    /// The amount of xp gained is coming into this function
    /// </summary>
    public void AddXP(int xpGained)
    {
        Debug.LogWarning("This character needs some xp to be given, the xpGained from the fight was: " + xpGained);
        // we probably want to do something with the xpGained.

        //We probably want to display the xp we just gained, by default it is 0

        // We probably also want to check to see if the player can level up and if so do something....what should we be checking?
    }

    /// <summary>
    /// A function used to handle actions associated with levelling up.
    /// </summary>
    private void LevelUp()
    {
        Debug.LogWarning("Level up has been called");
        // we probs want to increase our level....
        // As well as probably want to increase our threshold for when we should level up...based on our current new level
        // Last thing we probably want to do is increase our physical stats...if only we had a function to do that for us.       
    }

    /// <summary>
    /// A function used to assign a random amount of points ot each of our skills.
    /// </summary>
    public void DistributePhysicalStatsOnLevelUp(int PointsPool)
    {
        Debug.LogWarning("DistributePhysicalStatsOnLevelUp has been called " + PointsPool);
        // let's share these points somewhat evenly or based on some forumal to increase our current physical stats
        // then let's recalculate our dancing stats again to process and update the new values.
    }



    /// <summary>
    /// Is called inside of our DanceTeam.cs is used to set the characters name!
    /// </summary>
    /// <param name="characterName"></param>
    public void AssignName(CharacterName characterName)
    {
        charName = characterName;
        if(nickText != null)
        {
            nickText.text = charName.nickname;
            nickText.transform.LookAt(Camera.main.transform.position);
            //text faces the wrong way so
            nickText.transform.Rotate(0, 180, 0);
        }
    }
}
