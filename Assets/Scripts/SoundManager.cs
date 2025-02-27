using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // Singleton instance

    public AudioSource walkSound;
    public AudioSource jumpSound;
    public AudioSource pickupSound;
    public AudioSource deathSound;
    public AudioSource stompSound;

    void Awake()
    {
        // Ensure there's only one instance of SoundManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep SoundManager across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    public void PlayWalkSound()
    {
        if (!walkSound.isPlaying)
        {
            walkSound.Play();
        }
    }

    public void StopWalkSound()
    {
        if (walkSound.isPlaying)
        {
            walkSound.Stop();
        }
    }

    public void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            jumpSound.Play();
        }
    }

    public void PlayPickupSound()
    {
        if (pickupSound != null)
        {
            pickupSound.Play();
        }
    }

    public void PlayDeathSound()
    {
        if (deathSound != null)
        {
            deathSound.Play();
        }
    }

    public void PlayStompSound()
    {
        if (stompSound != null)
        {
            stompSound.Play();
        }
    }
}