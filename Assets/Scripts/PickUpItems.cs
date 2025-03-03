using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    public MarioController marioController;
    //public int carryCapacity = 1;

    //collects the items that the player will be carrying
    private List<GameObject> items = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))// && !ReachedCarryCapacity())
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
                case ItemType.Mushroom:
                    marioController.ReceivePowerUp();
                    break;
                default:
                    Debug.LogWarning("Undetected Item Type: " + itemDetails.GetItemType());
                    break;
            }

            Debug.Log("items.Count: " + items.Count);
            //Destroy(collision.gameObject);
        }
    }

    //public bool ReachedCarryCapacity() { return items.Count == carryCapacity; }

    //public bool DropItem(GameObject item)
    //{
    //    return items.Remove(item);
    //}
}
