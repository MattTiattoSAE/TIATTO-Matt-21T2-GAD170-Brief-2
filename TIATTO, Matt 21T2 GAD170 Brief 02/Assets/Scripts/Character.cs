using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Functions to complete:
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
    /// The amount of points a character has to determine their initial stats
    /// </summary>
    public int pool = 20;

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
    public float percentageChanceToWin;


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
        GeneratePhysicalStats(pool); // we want to generate some physical stats.
        CalculateDancingStats();// using those physical stats we want to generate some dancing stats.
    }

    /// <summary>
    /// Generates default stats for a character, using a stat pool
    /// </summary>
    /// <param name="pool"></param>
    public void GeneratePhysicalStats(int pool)
    {
        Debug.LogWarning(this.gameObject.name + " | GeneratePhysicalStats called | " + pool);

        while (pool > 0)
        {
            int statAssign = Random.Range(1, 4);
            {
                if (statAssign == 1)
                {
                    agility += 1;
                }
                else if (statAssign == 2)
                {
                    strength += 1;
                }
                else
                {
                    intelligence += 1;
                }
                pool -= 1;
            }
        }
        Debug.Log(this.gameObject.name + " | GeneratePhysicalStats | Agility: " + agility + " | Strength: " + strength + " | Intelligence: " + intelligence);
    }

    /// <summary>
    /// Generates a character's dancing stats based off their physical stats and the corresponding modifier
    /// </summary>
    public void CalculateDancingStats()
    {
        Debug.LogWarning(this.gameObject.name + " | CalculateDancingStats called");

        style = (int)((float)agility * agilityMultiplier);

        rhythm = (int)((float)strength * strengthMultiplier);

        luck = (int)((float)intelligence * inteligenceMultiplier);

        Debug.Log(this.gameObject.name + " | CalculateDancingStats | Style: " + style + " | Rhythm " + rhythm + " | Luck: " + luck);
    }


    /// <summary>
    /// Generates a percentage value for a character's chance to win
    /// </summary>
    /// <param name="normalisedValue"></param>
    public void SetPercentageValue(float normalisedValue)
    {
        Debug.LogWarning(this.gameObject.name + " | SetPercentageValue called | " + normalisedValue);

        percentageChanceToWin = normalisedValue * 100;

        Debug.Log(this.gameObject.name + " | SetPercentageValue | Normalised Value " + normalisedValue + " | Percentage " + percentageChanceToWin);
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
/// Generates a power level for a character based on their power level (sum of dancing stats)
/// </summary>
/// <returns></returns>
    public int ReturnDancePowerLevel()
    {
        Debug.LogWarning(this.gameObject.name + " | ReturnDancePowerLevel called");

        float score = style + rhythm + luck;

        Debug.Log(this.gameObject.name + " | ReturnDancePowerLevel | Power Level: " + score);

        return (int)score;
    }

    /// <summary>
    /// Adds xp to a character equal to xpGained
    /// </summary>
    /// <param name="xpGained"></param>
    public void AddXP(int xpGained)
    {
        Debug.LogWarning(this.gameObject.name +  " | AddXP called | " + xpGained);
        
        currentXp += xpGained;

        if (currentXp >= xpThreshold)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// A function used to handle actions associated with levelling up.
    /// </summary>
    private void LevelUp()
    {
        Debug.LogWarning(this.gameObject.name + " | LevelUp called");

        level += 1;
        
        currentXp -= xpThreshold;
        
        xpThreshold += xpThreshold + (level * 5);
        
        DistributePhysicalStatsOnLevelUp((int)((float)level + 1));

        Debug.Log(this.gameObject.name + " | LevelUp | Level: " + level + " | CurrentXP: " + currentXp + " | XPThreshold: " + xpThreshold);
        if (currentXp >= xpThreshold)
        {
            Debug.LogWarning(this.gameObject.name + " | LevelUp | CurrentXP exceeds XPThreshold! | CurrentXP: " + currentXp + " | XPThreshold: " + xpThreshold);
            LevelUp();
        }
    }

    /// <summary>
    /// Generates new physical stats for a character based on PointsPool
    /// </summary>
    /// <param name="PointsPool"></param>
    public void DistributePhysicalStatsOnLevelUp(int PointsPool)
    {
        Debug.LogWarning(this.gameObject.name + " | DistributePhysicalStatsOnLevelUp has been called | " + pool);
        while (pool > 0)
        {
            int statAssign = Random.Range(1, 4);
            {
                if (statAssign == 1)
                {
                    agility += 1;
                }
                else if (statAssign == 2)
                {
                    strength += 1;
                }
                else
                {
                    intelligence += 1;
                }
            }
        }
        Debug.Log(this.gameObject.name + " | DistributePhysicalStatsOnLevelUp | Agility: " + agility + " | Strength: " + strength + " | Intelligence: " + intelligence);
        CalculateDancingStats();
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
