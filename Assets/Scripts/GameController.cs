using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    [HideInInspector] public LocationManager locationManager;
    public InputAction[] inputActions;

    public bool isGameOver = false;

    public bool displayInteractableImageOnExamine = false;

    private List<string> textToDisplay = new List<string>();

    [HideInInspector] public InventoryManager inventoryManager;

    [HideInInspector] public PlayerCharacter player;
    [HideInInspector] public CombatManager combatManager;
    [HideInInspector] public RiddleManager riddleManager;


    public Location[] introLocations;
    private int currentIntroLocationIndex;

    private bool torchIsActivated = false;

    [SerializeField]
    private Skill[] skillsForNewen;

    [SerializeField]
    private Skill[] skillsForMawun;

    [SerializeField]
    private Skill[] skillsForAliwen;


    public Image bg_ImageContainer;

    public GameObject priorityObject;
    [HideInInspector] public Image priorityImage;
    [HideInInspector] public TextMeshProUGUI priorityInfo;

    public GameObject playerObject;
    [HideInInspector] public Image playerImage;
    [HideInInspector] public TextMeshProUGUI playerInfo;

    public Image player_image;
    public Sprite mawün, aliwen, newen;

    public bool alreadyCreatedPlayer = false;



    void Start()
    {
        locationManager = GetComponent<LocationManager>();
        inventoryManager = GetComponent<InventoryManager>();
        combatManager = GetComponent<CombatManager>();
        riddleManager = GetComponent<RiddleManager>();

        currentIntroLocationIndex = 0;



        try
        {

            priorityImage = priorityObject.GetComponentInChildren<Image>();
            priorityInfo = priorityObject.GetComponentInChildren<TextMeshProUGUI>();

            playerImage = playerObject.GetComponentInChildren<Image>();
            playerInfo = playerObject.GetComponentInChildren<TextMeshProUGUI>();

            //CreatePlayerCharacter(Klasse.NEWEN, "toni");

            HidePriotityObject();
            HidePlayerObject();

            if (SceneManager.GetActiveScene().name == "Main")
            {
                EnterNextRoom();
            }

        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public void UpdatePlayerInfo()
    {
        playerInfo.text = player.DisplayInfo();
    }

    public void SetPlayerImage()
    {
        switch (player.ClassName)
        {
            case "MAWÜN":
                {
                    player_image.sprite = mawün;
                    Debug.Log("Here");
                    break;
                }
            case "ALIWEN":
                {
                    player_image.sprite = aliwen;
                    Debug.Log("Here");
                    break;
                }
            case "NEWEN":
                {
                    player_image.sprite = newen;
                    Debug.Log("Here");
                    break;
                }
        }
    }

    public void EnterNextRoom()
    {

        locationManager.currentLocation = introLocations[currentIntroLocationIndex];
        EnterRoom(locationManager.currentLocation);

        Debug.Log("Name: " + introLocations[currentIntroLocationIndex].name);

        currentIntroLocationIndex++;
    }

    public void CreatePlayerCharacter(Klasse klasse, string name)
    {
        if (klasse == Klasse.MAWÜN)
        {
            this.player = Classes.CreateMawun(name, skillsForMawun);
            player_image.sprite = mawün;
        }
        if (klasse == Klasse.ALIWEN)
        {
            this.player = Classes.CreateAliwen(name, skillsForAliwen);
            player_image.sprite = aliwen;

        }
        if (klasse == Klasse.NEWEN)
        {
            this.player = Classes.CreateNewen(name, skillsForNewen);
            player_image.sprite = newen;
        }

        ShowPlayerObject();
        UpdatePlayerInfo();

        LogAction("You created your player!");

        alreadyCreatedPlayer = true;
    }

    public void HidePriotityObject()
    {
        priorityObject.SetActive(false);
    }
    public void ShowPriotityObject()
    {
        priorityObject.SetActive(true);
    }

    public void HidePlayerObject()
    {
        playerObject.SetActive(false);
    }
    public void ShowPlayerObject()
    {
        playerObject.SetActive(true);
    }

    public IEnumerator DisplayBGImage()
    {

        Sprite bg_image = locationManager.currentLocation.bg;
        bg_ImageContainer.sprite = bg_image;


        bg_ImageContainer.CrossFadeAlpha(1, 2, false);

        yield return new WaitForSeconds(20);



        bg_ImageContainer.CrossFadeAlpha(0f, 10, false);

        yield return null;

        torchIsActivated = false;

    }

    public void BGCourutine()
    {
        torchIsActivated = true;
        StartCoroutine(DisplayBGImage());
    }

    public void DeleteBGImage()
    {
        bg_ImageContainer.sprite = null;
    }



    public void EnterRoom(Location location)
    {

        locationManager.currentLocation = location;
        locationManager.currentRoomsExits = location.exits;
        locationManager.currentEnemies = location.enemies;


        Debug.Log(torchIsActivated);

        Sprite bg_image = location.bg;
        bg_ImageContainer.sprite = bg_image;


        Debug.Log(bg_image);


        if (!torchIsActivated) bg_ImageContainer.CrossFadeAlpha(0f, 0, false);


        UnpackRoom();

        foreach (Enemy enemy in locationManager.currentEnemies)
        {
            // Check probability to start combat with enemies
            if (UnityEngine.Random.value <= enemy.combatProbability && !combatManager.combatInProgress && !enemy.defeated && !riddleManager.isRiddleActive)
            {
                LogEvent("You are being attacked from the back by " + enemy.name + "!");

                combatManager.StartCombat(enemy, false, this);
            }

            if (riddleManager.isRiddleActive)
            {
                LogError("You have to give an answer to the riddle first!");
            }
        }


        DisplayText();
    }

    public void Log(string input)
    {
        textToDisplay.Add(input);
        DisplayText();
    }

    public void LogItalic(string input)
    {
        textToDisplay.Add($"<i>{input}</i>");
        DisplayText();
    }

    // todo: fix this
    public void LogDialogue(string npcName, string dialogue)
    {
        textToDisplay.Add($"<i>{npcName}:  \"{dialogue}\"</i>");
        DisplayText();
    }

    public void LogError(string input)
    {
        textToDisplay.Add($"<color=#800000>{input}</color>");
        DisplayText();
    }

    public void LogAttack(string input)
    {
        textToDisplay.Add($"<color=purple><b>{input}</b></color>");
        DisplayText();
    }


    public void LogAction(string input)
    {
        textToDisplay.Add($"<b>{input}</b>");
        DisplayText();
    }

    public void LogEvent(string input)
    {
        textToDisplay.Add($"<color=blue>{input}</color>");
        DisplayText();
    }

    public void ExamineInteractable(string toExamine)
    {
        InteractableObject currentInteractable = null;

        foreach (InteractableObject io in locationManager.currentLocation.interactables)
        {
            if (io.name == toExamine)
            {
                currentInteractable = io;

            }
        }

        foreach (InteractableObject io in inventoryManager.interactables)
        {
            if (io.name == toExamine)
            {
                currentInteractable = io;

            }
        }

        if (currentInteractable != null)
        {
            // show interactable until next input
            DisplayExaminePicture(currentInteractable);
            //Log(currentInteractable.Examine());
        }
        else LogError("You can't examine " + toExamine);
    }

    private void DisplayExaminePicture(InteractableObject io)
    {
        displayInteractableImageOnExamine = true;
        ShowPriotityObject();
        priorityImage.sprite = io.image;
        priorityInfo.text = io.Examine();
    }

    public void HideExaminePicture()
    {
        displayInteractableImageOnExamine = false;
        HidePriotityObject();
        priorityImage.sprite = null;
        priorityInfo.text = "";
    }

    public void TryChangeLocation(string toExitTo)
    {
        locationManager.TryToChangeLocationTo(toExitTo);
    }


    public void DisplayHelp()
    {

        string text = "Help\n" +
    "Examine - Investigate your surroundings, objects, and characters.\n" +
    "Go - Move to a different location or area.\n" +
    "Take - Collect an item or object into your inventory.\n" +
    "Inventory - Check your current possessions and their details.\n" +
    "Stats - Display your character's status and attributes.\n" +
    "Create - Establish your character's name and race at the beginning of your journey.\n" +
    "Use - Utilize an item from your inventory or interact with objects in the environment.\n" +
    "Fight or Attack - Engage in combat with enemies or other characters.\n" +
    "Talk - Initiate a conversation with an NPC (Non-Playable Character).\n" +
    "Answer - Respond to dialogue options and engage with characters in conversation.\n" +
    "Start - Begin or initiate specific interactions or events.\n" +
    "Next - Proceed to the next part of the story or dialogues.\n" +
    "Where Am I - Receive information about your current location or environment.";
        Log(text);
    }

    private void DisplayText()
    {
        string textToDisplayAsString = "";

        foreach (string text in textToDisplay)
        {
            textToDisplayAsString += text + "\n";
        }
        displayText.text = textToDisplayAsString;
    }


    public Enemy FindEnemyInLocation(string enemyName)
    {
        Enemy enemy = null;

        Enemy[] allEnemiesInLocation = locationManager.GetAllEnemies();

        foreach (Enemy e in allEnemiesInLocation)
        {
            if (e.name == enemyName)
            {
                return e;
            }
        }
        return enemy;
    }

    private void UnpackRoom()
    {

        // for test only
        foreach (InteractableObject interactableObject in locationManager.currentLocation.interactables)
        {
            interactableObject.taken = false;
            interactableObject.used = false;
        }

        foreach (Enemy enemy in locationManager.currentEnemies)
        {
            enemy.defeated = false;
        }
        // ---
        GetAllCurrentRoomExits();
        GetAllCurrentRoomInteractables();
        GetAllRoomNPCS();
        GetAllCurrentRoomEnemies();
    }

    private void GetAllRoomNPCS()
    {
        Location currentLocation = locationManager.currentLocation;
        NPC[] currentRoomNPCS = currentLocation.npcs;
        foreach (NPC npc in currentRoomNPCS)
        {
            textToDisplay.Add(npc.descriptionInLocation);
        }
    }

    private void GetAllCurrentRoomExits()
    {
        Location currentLocation = locationManager.currentLocation;
        string currentLocationDesc = currentLocation.locationDescription;
        // exits
        foreach (Exit exit in currentLocation.exits)
        {
            if (exit.isVisible)
            {
                textToDisplay.Add(exit.exitDescription);
            }
        }

        textToDisplay.Add(currentLocationDesc);
    }

    private void GetAllCurrentRoomEnemies()
    {
        string log = "";
        Location currentLocation = locationManager.currentLocation;
        // exits
        foreach (Enemy enemy in currentLocation.enemies)
        {
            {
                //if(!enemy.defeated)
                log += enemy.descriptionInLocation;

                enemy.ResetToDefault();
                enemy.defeated = false;
            }

            if (log != null) textToDisplay.Add(log);
        }
    }

    private void GetAllCurrentRoomInteractables()
    {
        Location currentLocation = locationManager.currentLocation;
        foreach (InteractableObject io in currentLocation.interactables)
        {
            if (!io.taken && !io.used) textToDisplay.Add(io.description);
        }


    }


    // Update is called once per frame
    void Update()
    {

    }

}
