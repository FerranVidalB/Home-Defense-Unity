using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ChangeMusicVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public float GetMusicVolume()
    {
        return audioSource.volume;
    }

}
