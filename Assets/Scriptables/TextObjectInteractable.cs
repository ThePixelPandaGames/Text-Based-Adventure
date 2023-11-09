using System.Text;
using UnityEngine;

[CreateAssetMenu (menuName = "TextAdventure/TestObject")]
public class TextObjectInteractable : InteractableObject
{
    public override string Examine()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(examinationDescription);

        return stringBuilder.ToString();
    }
    public override void Use(GameController gameController)
    {
        gameController.Log("Increased Player Health by 50!");
        gameController.player.IncreaseHealth(50, gameController);
    }
}
