using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Attack")]
public class Attack : InputAction
{
    public override void RespondToInput(GameController gameController, string[] userInput)
    {

        if (gameController.combatManager.combatInProgress)
        {
            if (userInput.Length == 2 && userInput[0] == "attack")
            {
                string skillName = userInput[1];
                gameController.combatManager.AttackWithSkill(gameController,skillName);
            }
            else
            {
                gameController.LogError("That's not how you attack! Use 'attack [skill]'!");
            }
        }else
        {
            gameController.LogError("Who you want to attack???");
        }
    }
}
