using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Next")]
public class Next : InputAction
{
    public override void RespondToInput(GameController gameController, string[] userInput)
    {

        if (userInput.Length
            != 1)
        {
            Debug.Log("Check for Error");
        }else
        {
            try
            {
                gameController.EnterNextRoom();
                    }catch(IndexOutOfRangeException e) {
                Debug.LogError("You cant type next, you have to create a player");
            }
        }

    }
}
