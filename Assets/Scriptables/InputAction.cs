
using UnityEngine;

public abstract class InputAction : ScriptableObject
{
    public string actionKeyWord;
    public abstract void RespondToInput(GameController gameController, string[] userInput);
}
