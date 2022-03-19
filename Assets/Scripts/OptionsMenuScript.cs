using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuScript : MonoBehaviour
{

    public Slider musicVolumeSlider;

    private void Start()
    {
        musicVolumeSlider.value = GameManager.Instance.GetMusicVolume();
    }

    public void BackToMainMenu()
    {
		//GameManager.Instance.GoToScene("MenuScene");
		SceneManager.LoadSceneAsync("MenuScene");
	}

    public void ChangeMusicVolume(float volume)
    {
        GameManager.Instance.ChangeMusicVolume(volume);
    }
}
