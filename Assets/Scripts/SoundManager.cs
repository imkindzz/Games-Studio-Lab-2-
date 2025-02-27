using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // Singleton

    public AudioSource walkSound;
    public AudioSource jumpSound;
    public AudioSource pickupSound;
    public AudioSource deathSound;
    public AudioSource stompSound;
    public AudioSource stageClearSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // Keeps SoundManager across scenes
    }

    public void PlayWalkSound()
    {
        if (!walkSound.isPlaying)
            walkSound.Play();
    }

    public void StopWalkSound()
    {
        if (walkSound.isPlaying)
            walkSound.Stop();
    }

    public void PlayJumpSound()
    {
        jumpSound.Play();
    }

    public void PlayPickupSound()
    {
        pickupSound.Play();
    }

    public void PlayDeathSound()
    {
        deathSound.Play();
    }

    public void PlayStompSound()
    {
        stompSound.Play();
    }

    public void PlayStageClearSound()
    {
        stageClearSound.Play();
    }
}