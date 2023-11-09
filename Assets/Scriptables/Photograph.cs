using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Interactables/Photo")]

public class Photo : InteractableObject
{
    public override string Examine()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(examinationDescription);

        return stringBuilder.ToString();
    }
    public override void Use(GameController gameController)
    {
        Debug.Log("Use Photo");
    }
}
