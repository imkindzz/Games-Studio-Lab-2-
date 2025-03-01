using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitBlockEffect : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    [SerializeField] private Transform itemSpawnLocation;

    //if player can only get one item from the block
    [SerializeField] private int getItemAttempts = 5;
    [SerializeField] private Sprite endSprite;

    //whether the item temporarily appears on the scene
    public bool itemTemporaryAppearance = false;
    public float itemTemporaryTime = 3f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if the colliding object is the player) and that hits could be maade
        if (collision.CompareTag("Player") && !IsNoMoreGetItemAttempt())
        {
            switch (gameObject.tag)
            {
                case "Question Mark":
                    OnQuestionMark();
                    break;
                default:
                    Debug.LogWarning("Unidentified tag: " + gameObject.tag);
                    break;
            }
        }
    }

    private void OnQuestionMark()
    {
        //Debug.Log("Function: OnQuestionmark");

        if (!IsItemSpawnLocationOccupied())
        {
            InstantiateItem();
        }

        if (IsNoMoreGetItemAttempt())
        {
            EndHitBlock();
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

        getItemAttempts--;

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

    private bool IsNoMoreGetItemAttempt()
    {
        return getItemAttempts == 0;
    }

    private void EndHitBlock()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = endSprite;
    }
}
