
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextInputManager : MonoBehaviour
{
    public TMP_InputField textInput;

    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        textInput.onEndEdit.AddListener(AcceptInput);
        gameController= GetComponent<GameController>();
        textInput.ActivateInputField();
    }

    private void AcceptInput(string input)
    {
        if (input.Length > 0)
        {

            gameController.Log("");
            input = input.ToLower();
            gameController.LogAction(input);

            if (gameController.isGameOver)
            {
                gameController.LogError("You have been defeated");
                gameController.LogError("Type 'exit' to return to the main menu or create a new player to start the story from the beginning! Hint: Create [race] [name]");
            }

                string[] seperatedWords = input.Split(' ');

                if (seperatedWords[0] == "stats")
                {
                    gameController.Log(gameController.player.DisplayStats());
                }

            if (seperatedWords[0] == "exit") {
                // go to previous scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }

            if (gameController.displayInteractableImageOnExamine)
            {
                gameController.HideExaminePicture();
            }


                if ((seperatedWords.Length > 0 || ((seperatedWords[0] == "create" && seperatedWords.Length == 3) || (seperatedWords[0] == "talk" && seperatedWords[1] == "to" && seperatedWords.Length == 3) || (seperatedWords[0] == "activate" && seperatedWords[1] == "class" && seperatedWords.Length == 3))))
                {

                    string action = seperatedWords[0];


                    foreach (InputAction inputAction in gameController.inputActions)
                    {
                        if (inputAction.actionKeyWord == action)
                        {
                            inputAction.RespondToInput(gameController, seperatedWords);


                            // handle class effects
                            if (gameController.player != null && gameController.player.ActivatedClassEffect)
                            {
                                if (gameController.player.ClassEffectCooldown > 0)
                                {
                                    gameController.player.ClassEffectCooldown--;
                                }
                                else
                                {
                                    gameController.player.ActivatedClassEffect = false;
                                    // reverse class effect if necessary
                                    gameController.player.Attack /= 2;
                                }
                            }

                            // --- //
                        }
                    }
                }

                foreach (Enemy enemy in gameController.locationManager.currentEnemies)
                {
                    // Check probability to start combat with enemies
                    if (Random.value <= enemy.combatProbability && !gameController.combatManager.combatInProgress && !enemy.defeated)
                    {
                        gameController.LogEvent("You are being attacked from the back by " + enemy.name + "!");
                        gameController.combatManager.StartCombat(enemy, false, gameController);
                    }
                }

                InputComplete(gameController);
            
        }

    }

    
      
private void InputComplete(GameController gameController)
    {
        textInput.ActivateInputField();
        textInput.text = null;
    }



    
}
