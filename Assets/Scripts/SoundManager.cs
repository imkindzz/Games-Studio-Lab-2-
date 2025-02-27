using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // Singleton

    public AudioSource walkSound;
    public AudioSource jumpSound;
    public AudioSource pickupSound;
    public AudioSource deathSound;
    public AudioSource stompSound;
    public AudioSource clearStageSound;
    public AudioSource BGMusic;

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

    public void PlayClearStageSound()
    {
        if (BGMusic.isPlaying)
        {
            BGMusic.Pause();
        }
        clearStageSound.Play();
        StartCoroutine(ResumeMusicAfterDelay(clearStageSound.clip.length));
    }

    private System.Collections.IEnumerator ResumeMusicAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        BGMusic.Play();
    }
}