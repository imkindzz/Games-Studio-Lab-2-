using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBlockEffect : MonoBehaviour
{
    [SerializeField] private List<GameObject> Items = new List<GameObject>();

    private List<BoxCollider2D> colldiers = new List<BoxCollider2D>();

    void Start()
    {
        //colldiers = gameObject.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            switch (gameObject.tag)
            {
                case "QuestionMark":
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
        Debug.Log("Function: OnQuestionmark");
    }
}
