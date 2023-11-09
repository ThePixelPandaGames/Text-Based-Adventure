using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Interactables/Fruit")]

public class Fruit : InteractableObject
{
    public override string Examine()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(examinationDescription);
        stringBuilder.AppendLine("Health recovery: " + boostNumber +" !");
        return stringBuilder.ToString();
    }
    public override void Use(GameController gameController)
    {
        Debug.Log("Use fruit");
    }
}
