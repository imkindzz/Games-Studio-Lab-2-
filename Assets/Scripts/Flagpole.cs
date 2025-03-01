using UnityEngine;
using UnityEngine.UI; // For UI Text
using UnityEngine.SceneManagement; // For Restarting Scene

public class Flagpole : MonoBehaviour
{
    public Animator animator;
    public GameObject restartText; // UI Text GameObject

    private bool canRestart = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("ReachFlag", true);
            animator.SetTrigger("ReachFlag");
            restartText.SetActive(true); // Show Restart Text
            canRestart = true;

        }
    }

    private void Update()
    {
        if (canRestart && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart Level
        }
    }
}
