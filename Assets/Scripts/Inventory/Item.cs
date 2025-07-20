using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "F.L.O.W./Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int maxStackSize = 100;
}

