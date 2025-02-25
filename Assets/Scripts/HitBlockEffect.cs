using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBlockEffect : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    [SerializeField] private Transform itemSpawnLocation;

    //if player can only get one item from the block
    public bool onlyForOneItem = true;

    //whether the item temporarily appears on the scene
    public bool itemTemporaryAppearance = false;
    public float itemTemporaryTime = 3f;

    private bool oneItemChanceUsed = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if the colliding object is the player
        if (collision.tag.Equals("Player"))
        {
            switch (gameObject.tag)
            {
                case "Question Mark":
                    OnQuestionMark();
                    break;
                default:
                    Debug.Log("Unidentified tag: " + gameObject.tag);
                    break;
            }
        }
    }

    private void OnQuestionMark()
    {
        //Debug.Log("Function: OnQuestionmark");

        if (!IsOnlyForOneItem() && !IsItemSpawnLocationOccupied())
        {
            InstantiateItem();
        }
    }

    private void InstantiateItem()
    {
        int randomIndex = Random.Range(0, items.Count);
        GameObject item = items[randomIndex];

        if (itemTemporaryAppearance)
        {
            Destroy(Instantiate(item, itemSpawnLocation), 3f);
        }
        else
        {
            Instantiate(item, itemSpawnLocation);
        }


        Debug.Log("Item Spawned");
    }

    private bool IsItemSpawnLocationOccupied()
    {
        Vector2 raycastOrigin = new Vector2(itemSpawnLocation.position.x, itemSpawnLocation.position.y);
        if (Physics2D.Raycast(raycastOrigin, Vector2.up, 0.1f))
        {
            return true;
        }

        return false;
    }

    private bool IsOnlyForOneItem()
    {
        if (onlyForOneItem && !oneItemChanceUsed)
        {
            oneItemChanceUsed = true;
            
            return false; //oneItemChance is not yet used during this function
        }

        if (oneItemChanceUsed)
        {
            return true;
        }

        return false;
    }
}
