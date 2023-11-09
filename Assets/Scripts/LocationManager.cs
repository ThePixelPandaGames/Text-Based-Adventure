using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public Location startLocation;
    [HideInInspector] public Location currentLocation;
    [HideInInspector] public Exit[] currentRoomsExits;
    [HideInInspector] public Enemy[] currentEnemies;

    private GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        currentLocation = startLocation;
        currentRoomsExits = currentLocation.exits;
        currentEnemies = currentLocation.enemies;

        gameController = GetComponent<GameController>();
    }

    public void TryToChangeLocationTo(string toExitTo)
    {
        Debug.Log("toExitTo = " + toExitTo);
        // check if toExitTo is reachable from this currentLocation
        foreach (Exit exit in currentRoomsExits)
        {
            Debug.Log("availableExit: " + exit.locationToExit.locationName);

            if (exit.locationToExit.locationName == toExitTo && exit.isVisible)
            {
                // is possible
                this.currentLocation = exit.locationToExit;
                this.currentRoomsExits = exit.locationToExit.exits;
                this.currentEnemies = exit.locationToExit.enemies;

                // Notify gameController
                gameController.EnterRoom(this.currentLocation);

                return;
            }
        }
        gameController.LogError("Could not find path to " + toExitTo);
    }

    public Enemy[] GetAllEnemies()
    {
        return currentLocation.enemies;
    }



}
