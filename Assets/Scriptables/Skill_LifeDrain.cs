using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Skills/Life Drain")]
public class Skill_LifeDrain : Skill
{  public int power;

    public override void ActivateSkillEffect(PlayerCharacter player, Enemy enemy, GameController gameController)
    {
        if (!isPlayerAttack)
        {
            int toDrain = enemy.attack / (10 - power);
            Debug.Log("tDrain: " + toDrain);
            enemy.IncreaseHealth(toDrain);
        }else
        {
            int toDrain = player.Attack / (10 -power);
            player.IncreaseHealth(toDrain, gameController);
        }
    }
}
