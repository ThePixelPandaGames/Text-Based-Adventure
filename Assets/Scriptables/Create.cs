using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Create")]

public class Create : InputAction
{
    public override void RespondToInput(GameController gameController, string[] userInput)
    {
        if (!gameController.alreadyCreatedPlayer)
        {
            Debug.Log("player: " + gameController.player);
            if (userInput[1] == "mawun" || userInput[1] == "mawün") { gameController.CreatePlayerCharacter(Klasse.MAWÜN, userInput[2]); Debug.Log("startLocation: "+gameController.locationManager.startLocation); gameController.EnterRoom(gameController.locationManager.startLocation); }

            if (userInput[1] == "aliwen") { gameController.CreatePlayerCharacter(Klasse.ALIWEN, userInput[2]); gameController.EnterRoom(gameController.locationManager.startLocation); }

            if (userInput[1] == "newen") { gameController.CreatePlayerCharacter(Klasse.NEWEN, userInput[2]); gameController.EnterRoom(gameController.locationManager.startLocation); }
        }
        else gameController.Log("You have already created a Hero!");
    }


}
