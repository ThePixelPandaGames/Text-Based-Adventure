using UnityEngine;

[CreateAssetMenu(menuName ="TextAdventure/InputActions/Inventory")]

public class Inventory : InputAction
{
    public override void RespondToInput(GameController gameController, string[] userInput)
    {
        gameController.inventoryManager.ListAllItems();
    }
}
