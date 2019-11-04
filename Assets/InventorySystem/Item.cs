using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventroy/Item ")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public string itemModernDescription = "Modern Description";
    public string itemAncientDescription = "Ancient Description";
    public Sprite ancientIcon;
    public Sprite modernIcon;
    public Sprite descriptionIcon;
    public ItemTypes itemType = ItemTypes.Basic;
    public int currentStack = 1;
    public int maxStack = 9;
    public bool isAncient = false;
    public string consumeAction = "Use";
}
