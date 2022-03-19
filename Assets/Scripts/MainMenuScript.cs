using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour {
	public void StartGame() {
	
		SceneManager.LoadSceneAsync("Game");
		//GameManager.Instance.GoToScene("Game");
	}

	public void GoToOptions() {
		SceneManager.LoadSceneAsync("OptionsScene");
		//GameManager.Instance.GoToScene("OptionsScene");

	}

	public void GoToAboutUs() {
		SceneManager.LoadSceneAsync("AboutUsScene");
		//GameManager.Instance.GoToScene("AboutUsScene");

	}

}
