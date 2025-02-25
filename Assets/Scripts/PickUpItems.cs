using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    //collects th eitems that the player will be carrying
    private List<GameObject> items = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Item"))
        {
            ItemDetails itemDetails = collision.GetComponent<ItemDetails>();
            Debug.Log("Item Detected: " + itemDetails.GetItemType());

            switch (itemDetails.GetItemType())
            {
                case ItemType.Axe:
                    items.Add(collision.gameObject);
                    break;
                case ItemType.Cloud:
                    items.Add(collision.gameObject);
                    break;
                case ItemType.Coin:
                    items.Add(collision.gameObject);
                    break;
                default:
                    Debug.LogWarning("Undetected Item Type: " + itemDetails.GetItemType());
                    break;
            }

            Destroy(collision.gameObject);
        }
    }
}
