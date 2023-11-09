using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
     Dictionary<string, int> items_amount = new Dictionary<string, int>();
    [HideInInspector] public List<InteractableObject> interactables = new List<InteractableObject>(); 

    GameController gameController;

    private void Start()
    {
        gameController = GetComponent<GameController>();
    }

    public void Take(InteractableObject item)
    {
        if (CheckIfItemExists(item.name))
        {
            IncreaseAmountOfItemByOne(item.name);
        }
        else
        {
            interactables.Add(item);
            items_amount.Add(item.name, 1);
        }
    }

    public void Discard(string item)
    {
        if (CheckIfItemExists(item))
        {

            int amount = GetAmountOfItem(item);
            if (amount > 0)
            {
                DecreaseAmountOfItemByOne(item);
            }
            if (items_amount[item] == 0)
            {
                items_amount.Remove(item);
                RemoveInteractableFromList(item);
                gameController.LogEvent("You threw away 1 " + item);
            }
            foreach (InteractableObject obj in gameController.locationManager.currentLocation.interactables)
            {
                if (obj.name == item && obj.taken)
                {
                    obj.taken = false;
                }
            }
        }
        else gameController.LogError(item + " is not in inventory");
    }

    private void RemoveInteractableFromList(string itemName)
    {
        foreach(InteractableObject io in interactables) { 
            if(io.name == itemName) {
                interactables.Remove(io);
                return;
            }
        }
    }

    public void ListAllItems()
    {

        string inventoryText = "<size=20><color=white>You look inside your backpack and you see:</color></size>\n";
        foreach (var item in items_amount)
        {
            string itemName = item.Key;
            int itemAmount = item.Value;
            inventoryText += $"<color=yellow>- <b>{itemName}</b> <i>x{itemAmount}</i></color>\n";
        }

        // Set the formatted inventoryPanel to your UI text component
        gameController.Log(inventoryText);
    }

    public bool CheckIfItemExists(string item)
    {
        return items_amount.ContainsKey(item);
    }

    private int GetAmountOfItem(string item)
    {
        if (CheckIfItemExists(item))
        {
            return items_amount[item];
        }
        else return 0;
    }

    private void SetAmountOfItem(String item, int newAmount)
    {
        if (CheckIfItemExists(item))
        {
            items_amount[item] = newAmount;
        }
    }

    private void IncreaseAmountOfItemByOne(String item)
    {
        if (CheckIfItemExists(item))
        {
            items_amount[item]++;
        }
    }

    private void DecreaseAmountOfItemByOne(String item)
    {
        if (CheckIfItemExists(item))
        {
            items_amount[item]--;
        }
    }
}
