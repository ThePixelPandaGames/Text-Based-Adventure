using System.Collections;
using UnityEngine;

public class RiddleManager : MonoBehaviour
{
    [HideInInspector] public bool isRiddleActive = false;
    [HideInInspector]public Riddle currentRiddle;
    GameController gameController;

    private void Start()
    {
        gameController = GetComponent<GameController>();    
    }

    // Start a riddle initiated by an NPC
    public void StartRiddle(Riddle riddle, NPC npc)
    {
        if (!isRiddleActive)
        {
            isRiddleActive = true;
            currentRiddle = riddle;

            // Display the riddle question to the player
            gameController.LogDialogue(npc.name, currentRiddle.riddleQuestion);

            // Start a coroutine to wait for player input
            StartCoroutine(WaitForPlayerInput(gameController));
        }
    }

    // Coroutine to wait for player input
    private IEnumerator WaitForPlayerInput(GameController gameController)
    {
        while (isRiddleActive)
        {
            gameController.Log("Wairing for answer.");
            yield return new WaitForSeconds(3);
        }
    }
    
    
}