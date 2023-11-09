using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Fight")]
public class Fight : InputAction
{
    public override void RespondToInput(GameController gameController, string[] userInput)
    {
        if(userInput.Length > 2) { return; }
            string enemyName = userInput[1];
            Enemy enemyToFight = gameController.FindEnemyInLocation(enemyName);
            if (enemyToFight != null && !enemyToFight.defeated)
            {
                gameController.combatManager.StartCombat(enemyToFight, true, gameController);
            }else
            {
                gameController.Log("You can't fight " + enemyName);
            }
    }
}
