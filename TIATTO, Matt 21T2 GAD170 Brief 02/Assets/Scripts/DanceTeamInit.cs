using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DanceTeamInit : MonoBehaviour
{
    public DanceTeam teamA, teamB; // A reference to our teamA and teamB DanceTeam instances.

    public GameObject dancerPrefab; // This is the dancer that gets spawned in for each team.
    public int dancersPerSide = 3; // This is the number of dancers for each team, if you want more, you need to modify this in the inspector.
    public CharacterNameGenerator nameGenerator; // This is a reference to our CharacterNameGenerator instance.
    private CharacterName[] teamACharacterNames; // An array to hold all our character names of TeamA.
    private CharacterName[] teamBCharacterNames; // An array to hold all the character names of TeamB

    /// <summary>
    /// Called to iniatlise the dance teams with some dancers :D
    /// </summary>
    public void InitTeams()
    {
        Debug.LogWarning(this.gameObject + " | InitTeams called");
        // We need to set out team names using teamA.SetTroupeName.
        teamA.SetTroupeName("Team 1");
        teamB.SetTroupeName("Team 2");
        // We need to generate some character names for our teams to use from our CharacterNameGenerator.
        teamACharacterNames = nameGenerator.ReturnTeamCharacterNames(dancersPerSide);
        teamBCharacterNames = nameGenerator.ReturnTeamCharacterNames(dancersPerSide);
        // We need to spawn in some dancers using teamA.InitialiseTeamFromNames.
        teamA.InitaliseTeamFromNames(dancerPrefab, DanceTeam.Direction.Left, teamACharacterNames);
        teamB.InitaliseTeamFromNames(dancerPrefab, DanceTeam.Direction.Right, teamBCharacterNames);
        Debug.LogWarning(this.gameObject + " | InitTeams initialised " + dancersPerSide * 2 + " characters");
    }
}
