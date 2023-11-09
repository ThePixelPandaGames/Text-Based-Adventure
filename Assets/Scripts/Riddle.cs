using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Riddle
{
    [TextArea] public string riddleQuestion;
    public string riddleAnswer;
    public InteractableObject riddleReward;
    public Location locationToUnlock;
}
