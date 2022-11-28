using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class inGameUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI AvailableAmmoText;
    public TextMeshProUGUI GunText;
    public TextMeshProUGUI scoreText;
    public GameObject GameUI;
    public GameObject Crosshair;
    public GameObject GameOverUI;
    public GameObject GameWonUI;

    public void Retry()
    {
        SaveSessionData();
        SceneManager.LoadScene(1);
    }

    public void ToScoreTable()
    {
        SaveSessionData();
        SceneManager.LoadScene(2);
    }

    void SaveSessionData()
    {
        DataHandler.Instance.BestScore = Player.Instance.Score;
        DataHandler.Instance.Save();
    }

    void UpdateUI()
    {
        healthText.text = $"{Player.Instance.health}/100";
        ammoText.text = $"{Player.Instance.firearm.currentAmmo}/{Player.Instance.firearm.maxAmmo}";
        GunText.text = $"{Player.Instance.firearm.GunType}";
        scoreText.text = $"Score: {Player.Instance.Score}";
        AvailableAmmoText.text = $"Ammo available: {Player.Instance.ammunition}";
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.isAlive)
        {
            UpdateUI();
        }
        else
        {
            GameUI.SetActive(false);
            Crosshair.SetActive(false);
            GameOverUI.SetActive(true);
        }

        if (SpawnManager.Instance.gameWon)
        {
            GameUI.SetActive(false);
            Crosshair.SetActive(false);
            GameWonUI.SetActive(true);
        }

    }
}
