using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Take")]

public class Take : InputAction
{
    public override void RespondToInput(GameController gameController, string[] userInput)
    {
        foreach (InteractableObject interactable in gameController.locationManager.currentLocation.interactables)
        {
            if (interactable.name == userInput[1] && !interactable.taken && !interactable.used && interactable.takeable)
            {
                interactable.taken = true;
                gameController.inventoryManager.Take(interactable);
                gameController.Log("You added " + interactable.name + " to your inventory!");

                return;
            }
        }
            gameController.LogError("You can't take " + userInput[1]);
    }
}
