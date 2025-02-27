using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetails : MonoBehaviour
{
    [SerializeField] private ItemType itemType;

    public ItemType GetItemType() { return itemType; }
}
