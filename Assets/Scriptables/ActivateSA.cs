using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/ActivateSA")]
public class ActivateSA : InputAction
{
    public override void RespondToInput(GameController gameController, string[] userInput)
    {
        if (userInput.Length > 3)
        {
            // handle error
        }
        else
        {
            if (userInput[1] == "class" && userInput[2] == "effect")
            {
                if (gameController.player.ActivatedClassEffect == false)
                {
                    gameController.LogAction("Activate class Effect!!!");
                    string className = gameController.player.ClassName;

                    gameController.player.ActivatedClassEffect = true;

                    switch (className)
                    {
                        case "Newen":
                            {
                                gameController.player.ClassEffectCooldown = 10;

                                gameController.player.Attack *= 2;
                            }
                            break;
                        case "Aliwen":
                            {
                                gameController.player.ClassEffectCooldown = 10;

                                if (gameController.combatManager.combatInProgress)
                                {
                                    gameController.combatManager.combatInProgress = false;
                                }
                            }
                            break;
                        case "Mawün":
                            {
                                gameController.player.ClassEffectCooldown = 10;

                                gameController.player._Health = gameController.player.Health;
                            }
                            break;

                    }
                }
                else
                {
                    gameController.LogError("Already activated Class Effect");
                }
            }
            else
            {
                gameController.LogError("Invalid Command!");
            }
        }
    }


}
