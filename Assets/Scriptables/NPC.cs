using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TextAdventure/New NPC")]
public class NPC : ScriptableObject
{
    public new string name;
    [TextArea]
    public string descriptionInLocation;
    public InteractableObject reward;
    [TextArea] public string response;
    public Riddle riddle;
  
}
