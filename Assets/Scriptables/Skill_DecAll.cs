using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Skills/Decrease All")]
public class Skill_DecAll: Skill
{

    public int power;
    public override void ActivateSkillEffect(PlayerCharacter player, Enemy enemy, GameController gameController)
    {
        if (!isPlayerAttack)
        {
            player.Attack -= power;
            player.Defense -= power;
            player.Mana -= power;
        }
        else
        {
            enemy.attack -= power;
            enemy.defense -= power;
        }
    }
}
