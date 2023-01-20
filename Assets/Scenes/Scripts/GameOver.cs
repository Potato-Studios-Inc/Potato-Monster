using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AudioSource audioSource;
    
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.6f;
        audioSource.Play();
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}