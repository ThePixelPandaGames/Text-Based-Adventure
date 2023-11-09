using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TextAdventure/InputActions/Talk")]
public class Talk : InputAction
{
    public override void RespondToInput(GameController gameController, string[] userInput)
    {
        if (userInput.Length == 3)
        {
            string npcName = userInput[2];

            NPC[] currentLocationNPCs = gameController.locationManager.currentLocation.npcs;

            if (currentLocationNPCs.Length == 0)
            {
                gameController.LogError("There is no one around!");
            }

            NPC foundNPC = null;
            foreach (NPC npc in currentLocationNPCs)
            {
                if (npc.name == npcName)
                {
                    foundNPC = npc; break;
                }
            }

            if (foundNPC != null)
            {
               if(foundNPC.response.Length >= 1) gameController.LogDialogue(foundNPC.name, foundNPC.response);

                if (foundNPC.riddle.riddleQuestion.Length <= 0)
                {
                   

                    if (foundNPC.reward != null)
                    {
                        gameController.LogEvent("Reveiced " + foundNPC.reward.name + " from " + foundNPC.name);
                        gameController.inventoryManager.Take(foundNPC.reward);
                        //todo: uncomment this for release, only now for dev
                        //foundNPC.reward = null;
                    }
                }
                else if (foundNPC.riddle.riddleQuestion.Length > 0 && !gameController.combatManager.combatInProgress)
                {
                    gameController.riddleManager.StartRiddle(foundNPC.riddle, foundNPC);
                }else if (gameController.combatManager.combatInProgress)
                {
                    gameController.LogError("Seems like there is a riddle to solve but first defend your life!");
                }
            }
            else
            {
                gameController.LogError("Who you want to talk to? " + npcName + " isn't here!");
            }
        }else
        {
            Debug.Log("Wrong syntax");
        }
    }
}
