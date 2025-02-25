using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Item"))
        {
            ItemDetails itemDetails = collision.GetComponent<ItemDetails>();
            Debug.Log("Item Detected: " + itemDetails.GetItemType());

            switch (itemDetails.GetItemType())
            {
                case ItemType.Axe:
                    break;
                case ItemType.Cloud:
                    break;
                case ItemType.Coin:
                    break;
                default:
                    Debug.LogWarning("Undetected Item Type: " + itemDetails.GetItemType());
                    break;
            }

            Destroy(collision.gameObject);
        }
    }
}
