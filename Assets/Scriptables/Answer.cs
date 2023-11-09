using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Answer")]
public class Answer : InputAction
{
    public override void RespondToInput(GameController gameController, string[] userInput)
    {
        if (gameController.riddleManager.isRiddleActive)
        {
             if(userInput.Length == 2) {
                if (userInput[1] == gameController.riddleManager.currentRiddle.riddleAnswer)
                {
                    // give reward
                    Riddle riddle = gameController.riddleManager.currentRiddle;
                    gameController.LogAction("correct answer!");


                    if (riddle.riddleReward != null)
                    {       
                        gameController.inventoryManager.Take(riddle.riddleReward);
                        gameController.LogAction("Received " + riddle.riddleReward.name + " !");
                    } 
                    if(riddle.locationToUnlock != null) {
                        Exit exit_ = null;
                        foreach(Exit exit in riddle.locationToUnlock.exits)
                        {
                            if (!exit.isVisible)
                            {
                                exit_ = exit;
                                exit_.isVisible = true;
                            }
                        }if (exit_ != null) {
                            gameController.LogAction("Unlocked new Location: " + exit_.exitDescription + " !");
                        } else
                        {
                            Debug.Log("Check this code, something went wrong");
                        }
                    }

                    gameController.riddleManager.isRiddleActive = false;


                }
            } 
             else
            {
                gameController.LogError("You answer is too long/ short!");
            }
        } else
        {
            gameController.LogError("What do you want to answer to ?");
        }
    }

}
