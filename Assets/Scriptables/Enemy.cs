using System.Linq;
using System.Security.Cryptography;
using System.Xml;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/New Enemy")]
public class Enemy : ScriptableObject
{
    public new string name;
    public int maxHealth;
    [HideInInspector]public int Health;
    public int maxAttack;

    [HideInInspector] public int attack;
    [HideInInspector] public int defense;
    public int maxDefense;

    public bool defeated = false;
    [Range(0.0f, 1f)]
    public float combatProbability;

    public Skill[] skills; // An array of skills for the enemy
    public int expRewards;
    // EXP rewarded to the player upon defeat
    [TextArea]
    public string descriptionInLocation;
    public Sprite image;

    public string DisplayStats()
    {
        string stats = $"<size=20><b><color=blue>================</color></b></size>\n";
        stats += $"<color=green>Name:</color> {name}\n";

        // Check and add bold formatting to Health if it's higher than default
        if (Health > maxHealth)
            stats += $"<b><color=red>Health:</color> {Health}</b>\n";
        else
            stats += $"<color=red>Health:</color> {Health}\n";

        // Check and add bold formatting to Attack if it's higher than default
        if (attack > maxAttack)
            stats += $"<b><color=red>Attack:</color> {attack}</b>\n";
        else
            stats += $"<color=red>Attack:</color> {attack}\n";

        // Check and add bold formatting to Defense if it's higher than default
        if (defense > maxDefense)
            stats += $"<b><color=red>Defense:</color> {defense}</b>\n";
        else
            stats += $"<color=red>Defense:</color> {defense}\n";
        stats += $"<size=20><b><color=blue>================</color></b></size>";
        return stats;
    }








    public Skill GetRandomSkill()
    {
        if (skills != null && skills.Length > 0)
        {
            int randomIndex = Random.Range(0, skills.Length);
            return skills[randomIndex];
        }
        return null;
    }
    public int TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(damage - defense, 1);
        Health -= actualDamage;
        return actualDamage;
    }

    public void IncreaseHealth(int toIncrease)
    {
        int newHealthValue = Health + toIncrease;
        Health = Mathf.Min(newHealthValue, maxHealth);
    }

    public int GiveExpReward()
    {
        return expRewards;
    }

    public void ResetToDefault()
    {
        Health = maxHealth;
        attack = maxAttack;
        defense = maxDefense;
    }
}
