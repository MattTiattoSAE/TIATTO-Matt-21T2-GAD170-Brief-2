using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNameGenerator : MonoBehaviour
{
    [Header("Possible first names")]
    public List<string> firstNames = new List<string>(); // These appear in the inspector, you should be assigning names to these in the inspector.
    [Header("Possible last names")]
    public List<string> lastNames = new List<string>();
    [Header("Possible nicknames")]
    public List<string> nicknames = new List<string>();
    [Header("Possible adjectives to describe the character")]
    public List<string> descriptors = new List<string>();

 

    /// <summary>
    /// Creates a list of names for all our characters to potentiall use.
    /// This get's called in the battle stater, before both teams call initTeams().
    /// </summary>
    public void CreateNames()
    {
        Debug.LogWarning(this.gameObject + " | Create Names Called");

        // set list ranges to default values (string 1 to 6 (6 individual elements)
        firstNames.AddRange(new string[] { "John", "Barry", "James", "Barbara", "Martha", "Katie" });
        lastNames.AddRange(new string[] { "Jones", "Smith", "Williams", "Brown", "Miller", "Davis" });
        nicknames.AddRange(new string[] { "Left Feet", "Flex", "Strider", "Split", "Doll", "Stanky Legs" });
        descriptors.AddRange(new string[] { "Generic", "Angry", "Upset", "Joyful", "Depressed", "Tired" });

        // log whether ranges were created successfully
        Debug.Log(this.gameObject + " | firstNames " + firstNames.Count + " | lastNames " + lastNames.Count + " | nicknames " + nicknames.Count + " | descriptors " + descriptors.Count);
    }

    /// <summary>
    /// Returns an Array of Character Names based on the number of namesNeeded.
    /// </summary>
    /// <param name="namesNeeded"></param>
    /// <returns></returns>
    public CharacterName[] ReturnTeamCharacterNames(int namesNeeded)
    {
        Debug.LogWarning("CharacterNameGenerator called, it needs to fill out the names array with unique randomly constructed character names");
        CharacterName[] names = new CharacterName[namesNeeded]; 
        CharacterName emptyName = new CharacterName(string.Empty, string.Empty, string.Empty, string.Empty);

        for (int i = 0; i < names.Length; i++)
        {
            //For every name we need to generate, we need to assign a random first name, last name, nickname and descriptor to each.
            //Below is an example of setting the first name of the emptyName variable to the string "Blank".
            emptyName.firstName = firstNames[(Random.Range(0, firstNames.Count))];
            firstNames.Remove(emptyName.firstName);
            emptyName.lastName = lastNames[(Random.Range(0, lastNames.Count))];
            lastNames.Remove(emptyName.lastName);
            emptyName.nickname = nicknames[(Random.Range(0, nicknames.Count))];
            nicknames.Remove(emptyName.nickname);
            emptyName.descriptor = descriptors[(Random.Range(0, descriptors.Count))];
            descriptors.Remove(emptyName.descriptor);
            string fullName = emptyName.GetFullCharacterName();
            names[i] = emptyName;
            Debug.Log(fullName);
        } 

        //Returns an array of names that we just created.
        return names;
    }
}