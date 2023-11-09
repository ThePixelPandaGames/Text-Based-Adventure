using System.Text;
using UnityEngine;

abstract public class InteractableObject : ScriptableObject
{
   public new string name;
   public string description;
   public string examinationDescription;
   public bool taken = false;
   public bool used = false;
    public bool takeable = true;
    public bool usable = true;
    public bool discardAfterUse = true;
    public int boostNumber;
    public Sprite image;
    // Input action[]


    public abstract string Examine();
 

    abstract public void Use(GameController gameController);


}
