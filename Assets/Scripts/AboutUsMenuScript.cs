using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AboutUsMenuScript : MonoBehaviour
{
   public void BackToMainMenu()
    {
		SceneManager.LoadSceneAsync("MenuScene");
		//GameManager.Instance.GoToScene("MenuScene");
	}
}
