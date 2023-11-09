using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Interactables/Meat")]

public class Meat : InteractableObject
{
    public override string Examine()
    {
        StringBuilder stringBuilder= new StringBuilder();
        stringBuilder.AppendLine(examinationDescription);
        stringBuilder.AppendLine("Attack & Defense permament boost: + " + boostNumber);

        return stringBuilder.ToString();
    }

    public override void Use(GameController gameController)
    {
        Debug.Log("Use Meat");
        gameController.player.Attack += boostNumber;
        gameController.player.Defense += boostNumber;  
    }
}
