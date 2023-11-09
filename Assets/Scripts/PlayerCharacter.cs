using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum Klasse
{
    NEWEN,
    MAWÜN,
    ALIWEN
}

[Serializable]
public class PlayerCharacter : MonoBehaviour
{
    public string PlayerName { get; set; }
    public string ClassName { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }

    public string Description { get; set; }
    public int _Health { get => _health; set => _health = value; }

    public int Mana { get; set; }
    public int MaxMana { get; set; }

    private int _health;

    public int ClassEffectCooldown { get; set; }

    public bool ActivatedClassEffect { get; set; }



    [SerializeField]
    private Skill[] skillsArray;


    private List<Skill> skills = new List<Skill>();

    // Constructor for creating a new player character
    public PlayerCharacter(string className, string playerName, Skill[] skills)
    {
        PlayerName = playerName;
        ClassName = className;
        Health = 100;
        Attack = 10;
        Defense = 5;
        Level = 1;
        Experience = 0;
        Description = "";
        Mana = MaxMana = 100;
        ClassEffectCooldown = 10;
        ActivatedClassEffect = false;



        _Health = Health;


        skillsArray = skills;
        this.skills.Add(skillsArray[0]);
    }



    public bool UseMana(int cost, GameController gameController)
    {
        if (Mana >= cost)
        {
            Mana -= cost;
            gameController.UpdatePlayerInfo();

            return true;
        }
        else
        {
            Mana = 0;
            gameController.UpdatePlayerInfo();

            return false;
        }

    }



    public void GainExperience(int amount, GameController gameController)
    {
        Experience += amount;
        if (Experience >= CalculateRequiredExperienceForNextLevel())
        {
            LevelUp(gameController);
        }

        gameController.UpdatePlayerInfo();
    }

    public void IncreaseHealth(int amount, GameController gameController)
    {
        _Health = Health + amount;
        if (_Health >= Health)
        {
            _Health = Health;
        }
        gameController.UpdatePlayerInfo();
    }

    public Skill GetSkillByName(string skillName)
    {
        foreach (Skill skill in skills)
        {
            if (skill.name == skillName) return skill;
        }
        return null;
    }

    private int CalculateRequiredExperienceForNextLevel()
    {
        return Level * 100;
    }

    private void LevelUp(GameController gameController)
    {
        gameController.LogAction("=== Level Up ! ===");
        Level++;
        Health += 20;
        Attack += 5;
        Defense += 2;
        MaxMana += 10;
        Experience = 0;


        _Health = Health;
        Mana = MaxMana;


        // Add skills based on the player's class and level
        if (Level == 4)
        {
            gameController.LogAction("Learned new Skill!");
            gameController.LogAction(skillsArray[1].ToString());
            skills.Add(skillsArray[1]);
        }
        if (Level == 7)
        {
            skills.Add(skillsArray[2]);
            gameController.LogAction("Learned new Skill!");
            gameController.LogAction(skillsArray[2].ToString());
        }

        if (Level == 10)
        {
            skills.Add(skillsArray[3]);
            gameController.LogAction("Learned new Skill!");
            gameController.LogAction(skillsArray[3].ToString());
        }

        gameController.UpdatePlayerInfo();

    }

    public void TakeDamage(int damage, GameController gameController)
    {
        _Health -= damage;

        // Ensure that health doesn't go below 0
        if (_Health < 0)
        {
            _Health = 0;
        }

        // Check if the player has been defeated
        if (_Health == 0)
        {
            HandleDefeat(); // You can implement this method to handle player defeat
        }

        gameController.UpdatePlayerInfo();

    }

    private void HandleDefeat()
    {
        // do sth here
        Debug.Log("Lost the Game");
    }

    public string DisplayStats()
    {
        string stats = $"<size=20><b><color=blue>===== Player Stats =====</color></b></size>\n";
        stats += $"<color=green>Name:</color> {PlayerName}\n";
        stats += $"<color=green>Class:</color> {ClassName}\n";
        stats += $"<color=green>Level:</color> {Level}\n";
        stats += $"<color=green>Experience:</color> {Experience} / {CalculateRequiredExperienceForNextLevel()}\n";
        stats += $"<color=red>Health:</color> {_Health}\n";
        stats += $"<color=red>Attack:</color> {Attack}\n";
        stats += $"<color=red>Defense:</color> {Defense}\n";
        stats += $"<color=blue>Mana:</color> {Mana}\n";

        stats += $"<color=green>Skills:</color>\n";

        if (skills.Count > 0)
        {
            foreach (var skill in skills)
            {
                stats += $"<i><color=orange>{skill.ToString()}</color></i>\n";
            }
        }

        stats += $"<size=20><b><color=blue>=====================</color></b></size>";

        return stats;
    }

    public string DisplayInfo()
    {
        string stats = $"<size=20><b><color=blue>================</color></b></size>\n";
        stats += $"<color=green>Name:</color> {PlayerName}\n";
        stats += $"<color=green>Class:</color> {ClassName}\n";
        stats += $"<color=green>Level:</color> {Level}\n";
        stats += $"<color=green>Experience:</color> {Experience} / {CalculateRequiredExperienceForNextLevel()}\n";
        stats += $"<color=red>Health:</color> {_Health}\n";
        stats += $"<color=red>Attack:</color> {Attack}\n";
        stats += $"<color=red>Defense:</color> {Defense}\n";
        stats += $"<color=blue>Mana:</color> {Mana}\n";

        stats += $"<size=20><b><color=blue>================</color></b></size>";

        return stats;
    }


}


// Define the three classes
public static class Classes
{


    public static PlayerCharacter CreateNewen(string name, Skill[] skills)
    {
        return new PlayerCharacter("Newen", name, skills)
        {
            Health = 120,
            Attack = 50,
            Defense = 25,
            MaxMana = 100,
            Description = "Descendant of ancient dragons, Dragonshifters are gifted with the ability to harness the power of fire and transform into fierce dragon forms. As a Dragonshifter, you command the flames of destruction and protective scales. Unleash devastating fire spells, and become an unstoppable force on the battlefield."
        };
    }

    public static PlayerCharacter CreateAliwen(string name, Skill[] skills)
    {

        return new PlayerCharacter("Aliwen", name, skills)
        {
            Health = 100,
            Attack = 30,
            Defense = 20,
            MaxMana = 150,
            Description = "Masters of stealth and precision, Shadowblades are the unseen predators of the night. As a Shadowblade, you walk in the shadows, striking from the darkness. You possess unparalleled agility and can backstab enemies with deadly accuracy. Use your abilities to infiltrate, assassinate, and vanish without a trace."
        };
    }

    public static PlayerCharacter CreateMawun(string name, Skill[] skills)
    {

        return new PlayerCharacter("Mawün", name, skills)
        {
            Health = 80,
            Attack = 25,
            Defense = 15,
            MaxMana = 200,
            Description = "Connected to the elemental forces of nature, Elementalists wield the powers of earth, air, fire, and water. As an Elementalist, you are the embodiment of nature's fury. Harness the elements to protect and attack, summon storms, and command the very earth beneath your feet. Your connection to the elements makes you a versatile and formidable mage."
        };
    }
}
