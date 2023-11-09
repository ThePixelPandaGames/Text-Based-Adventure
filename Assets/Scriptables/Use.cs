using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Use")]

public class Use : InputAction
{
    public override void RespondToInput(GameController gameController, string[] userInput)
    {
        Debug.Log(gameController.locationManager.currentLocation.interactables.Length);
        foreach (InteractableObject interactable in gameController.locationManager.currentLocation.interactables)
        {
            if (interactable.name == userInput[1] && interactable.taken && !interactable.used && interactable.usable)
            {
                interactable.taken = true;
                interactable.Use(gameController);
                gameController.Log("You used " + interactable.name + " !");
                interactable.used = true;
                if (interactable.discardAfterUse)
                {
                    gameController.inventoryManager.Discard(interactable.name);
                }

                return;
            }
        }
        // if item in inventory
        if (gameController.inventoryManager.CheckIfItemExists(userInput[1])) { 
            foreach(InteractableObject io in gameController.inventoryManager.interactables)
            {
                if(io.name == userInput[1])
                {
                    io.taken = true;
                    io.Use(gameController);
                    gameController.Log("You used " + io.name + " !");
                    io.used = true;
                    if (io.discardAfterUse)
                    {
                        gameController.inventoryManager.Discard(io.name);
                    }
                    return;
                }
                
            }
           
        }

        {
            gameController.LogError("You can't use " + userInput[1]);
        }
        // do the same
    }
}
