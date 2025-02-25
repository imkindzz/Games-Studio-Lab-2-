using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBlockEffect : MonoBehaviour
{
    [SerializeField] private List<GameObject> Items = new List<GameObject>();
    [SerializeField] private Transform ItemSpawnLocation;

    private bool itemSpawnLocationOccupied = false;

    void Start()
    {
        //colldiers = gameObject.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if the colliding object is the player/mario
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

        if (!itemSpawnLocationOccupied)
        {
            InstantiateItem();
        }
    }

    private void InstantiateItem()
    {
        int randomIndex = Random.Range(0, Items.Count);
        GameObject item = Items[randomIndex];

        Instantiate(item, ItemSpawnLocation);

        itemSpawnLocationOccupied = true;
    }
}
