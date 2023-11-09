using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Location")]
public class Location : ScriptableObject
{
    public string locationName;
    [TextArea]
    public string locationDescription;
    public Exit[] exits;
    public InteractableObject[] interactables;
    public Enemy[] enemies;
    public NPC[] npcs;
    public Sprite bg;
}
