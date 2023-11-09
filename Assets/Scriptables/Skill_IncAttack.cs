using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Skills/Increase Attack")]
public class Skill_IncAttack: Skill
{
    public override void ActivateSkillEffect(PlayerCharacter player, Enemy enemy, GameController gameController)
    {
        if(!isPlayerAttack)
        enemy.attack += 3;
        else player.Attack+= 3; 
    }
}
