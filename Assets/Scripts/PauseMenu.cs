using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseOverlay;
    public GameObject menuOverlay;
    public GameObject turretShopOverlay;
    public GameObject gameOverOverlay;
    public GameObject loadingOverlay;
    public TMP_Text baseWealth;
    public Slider baseHealth;
    public GameObject[] turretsDisabledOverlay;
    public Button[] turretBuyButton;
    public float[] turretsCost;

 



    public void PauseGame(bool paused)
    {
        GameIsPaused = paused;
        pauseOverlay.SetActive(GameIsPaused);
        menuOverlay.SetActive(!GameIsPaused);
        if (GameIsPaused)
        {
            Time.timeScale = 0f;
            Camera.main.GetComponent<CameraController>().DisableCameraMove();
        }
        else
        {
            Camera.main.GetComponent<CameraController>().EnableCameraMove();
            Time.timeScale = 1f;
        }
    }

    public void UpdateHealth(float health)
    {
        if (health <= 0f)
        {
            GameOver();
        }
        baseHealth.value = health;
    }

    public void UpdateCoins(float coins)
    {
        baseWealth.text = coins.ToString();
        for (int i = 0; i < turretsDisabledOverlay.Length; i++) 
        {
            if (coins >= turretsCost[i])
            {
                turretsDisabledOverlay[i].SetActive(false);
                turretBuyButton[i].enabled = true;
            } else
            {
                turretsDisabledOverlay[i].SetActive(true);
                turretBuyButton[i].enabled = false;
            }
        }
    }

    private void GameOver()
    {
        gameOverOverlay.SetActive(true);
        pauseOverlay.SetActive(false);
        menuOverlay.SetActive(false);
        turretShopOverlay.SetActive(false);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowTurretShop(bool show)
    {
        if (show)
        {
            if (turretShopOverlay.active)
            {
            turretShopOverlay.SetActive(false);
            } else
            {
            turretShopOverlay.SetActive(true);
            }

        } else
        {
            turretShopOverlay.SetActive(false);
        }
    }

    public void InstantiateTurret(int turretCost)
    {
        if (turretCost <= float.Parse(baseWealth.text))
        {
        GameObject.FindObjectOfType<TurretPlacementController>().PlaceNewTurret(turretCost);
        }
    }

    public void RestartGame()
    {
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		//StartCoroutine(reloadScene());
		//GameManager.Instance.GoToScene("Game");
	}
	IEnumerator reloadScene() {
		yield return  new WaitForSeconds(0.0f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name,LoadSceneMode.Single);
	}

    public void MapLoaded()
    {
        loadingOverlay.SetActive(false);
        menuOverlay.SetActive(true);
    }

}
