using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// maybe dont add discard since player has no inventory limit
[CreateAssetMenu(menuName = "TextAdventure/InputActions/Discard")]
public class Discard : InputAction
{

    public override void RespondToInput(GameController gameController, string[] userInput)
    {
        gameController.inventoryManager.Discard(userInput[1]);
    }


}
