using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Interactables/Med")]

public class Med : InteractableObject
{
    public override string Examine()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(examinationDescription);
        stringBuilder.AppendLine("Mana recovery: + " + boostNumber);

        return stringBuilder.ToString();
    }

    public override void Use(GameController gameController)
    {
        Debug.Log("Use Med");
        gameController.player.Mana += boostNumber;
    }
}
