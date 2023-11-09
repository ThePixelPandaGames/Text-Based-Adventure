

using UnityEngine;

[System.Serializable]
public class Exit {
    public Location locationToExit;
    [TextArea]
    public string exitDescription;
    public bool isVisible = true;
}

