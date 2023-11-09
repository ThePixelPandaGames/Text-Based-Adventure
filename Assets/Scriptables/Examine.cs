using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Examine")]
public class Examine : InputAction
{
    public override void RespondToInput(GameController gameController, string[] userInput)
    {
        if (!gameController.combatManager.combatInProgress)
        {
            gameController.ExamineInteractable(userInput[1]);
        } else
        {
            gameController.LogError("You can't right now, you are in a fight!");
        }
    }
}
