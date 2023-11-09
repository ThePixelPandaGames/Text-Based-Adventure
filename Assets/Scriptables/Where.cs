using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Where")]

public class Where : InputAction
{
    public override void RespondToInput(GameController gameController, string[] userInput)
    {
        if(userInput.Length != 3)
        {
            gameController.LogError("what?");
        } else
        {
            if (userInput[1] == "am" && userInput[2] == "i")
            {
                gameController.Log(gameController.locationManager.currentLocation.name);
                gameController.Log(gameController.locationManager.currentLocation.locationDescription);

}
        }
    }
}
