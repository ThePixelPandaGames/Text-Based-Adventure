using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/StartGame")]
public class StartGame : InputAction
{
    public override void RespondToInput(GameController gameController, string[] userInput)
    {

        if (userInput.Length
            != 2)
        {
            Debug.Log("start Game, not longer than 2 words");

        }
        else
        {
            if (userInput[1] == "game")
            {
                //gameController.EnterRoom(gameController.IntroLocation);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }else
            {
                Debug.Log("Type 'start game'");
            }
        }

    }
}
