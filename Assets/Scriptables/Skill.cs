using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public new string name;
    public string description;
    [Range(1.0f, 2f)]
    public float attackMultiplier;
    public Klasse belongsToClass;
    public int ManaCost;

    public bool isPlayerAttack = false;

    public abstract void ActivateSkillEffect(PlayerCharacter player, Enemy enemy, GameController gameController);



    public override string ToString()
    {
        return $"\tSkill Name: {name}\n\tDescription: {description}\n\tAttack Multiplier: {attackMultiplier}\n\tBelongs to Class: {belongsToClass}\n\tMana Cost: {ManaCost}";
    }
}
