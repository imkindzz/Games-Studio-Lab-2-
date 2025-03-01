using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopMusic();
            SceneManager.LoadScene("StartMenu");
        }
    }
    public void StartGame()
    {
        PlayMusic();
        SceneManager.LoadScene("Main"); // Make sure the scene name matches exactly
    }
    public void StartTutorial()
    {
        PlayMusic();
        SceneManager.LoadScene("Testing"); // Make sure the scene name matches exactly
    }
    private void StopMusic()
    {
        if (SoundManager.instance != null && SoundManager.instance.BGMusic.isPlaying)
        {
            SoundManager.instance.BGMusic.Stop();
        }
    }
    private void PlayMusic()
    {
        if (SoundManager.instance != null && !SoundManager.instance.BGMusic.isPlaying)
        {
            SoundManager.instance.BGMusic.Play();
        }
    }
}
