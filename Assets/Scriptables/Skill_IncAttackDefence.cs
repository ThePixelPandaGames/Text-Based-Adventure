using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Skills/Increase Attack and Defense")]
public class Skill_IncAttackDefense: Skill
{
    public int power;

    public override void ActivateSkillEffect(PlayerCharacter player, Enemy enemy, GameController gameController)
    {
        if (!isPlayerAttack)
        {
            enemy.attack += power;
            enemy.defense += power;
        }else
        {
            player.Attack += power;
            player.Defense+= power; 
        }
    }
}
