﻿using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class playerCollision : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject DeadPanel;
    public GameObject Player;
    public Text scoreText;
    public GameObject count;
    public Text HSText;
    public Text balanceText;
    int balance;
    int tBalance;

    int highScoreNumber;


    public void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "canKill")
        {               
            KillPlayer();
        }       
    }
    public void KillPlayer ()
    {
        PlayerPrefs.SetInt("isDead", 1);
        CallAd();
        disEnableGO();
                   
        highScoreNumber = Convert.ToInt32(scoreText.text);


        if (highScoreNumber >= PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", highScoreNumber);
        }
        countEarnedScraps();
        SetText();
        
    }
    void countEarnedScraps ()
    {
        print("calling");
        balance = PlayerPrefs.GetInt("balance");
        int newBalance = balance + highScoreNumber;
        PlayerPrefs.SetInt("balance", newBalance);
        balanceText.text = "$" + PlayerPrefs.GetInt("balance");
    }
    public void ButtonToRestart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt("isDead", 0);
        SceneManager.LoadSceneAsync(currentSceneName);
    }
    public void ButtonToMain ()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    void CallAd ()
    {
        int randomGuess = UnityEngine.Random.Range(0, 4);
        if (randomGuess == 2)
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                //Advertisement.Show();
            }        
        }
    }
    void disEnableGO ()
    {
        DeadPanel.SetActive(true);
        Player.SetActive(false);
        count.SetActive(false);
    }
    void SetText ()
    {
        int _pPhighScore = PlayerPrefs.GetInt("highScore");
        HSText.text = _pPhighScore.ToString();
    }
    public void ToShop ()
    {
        SceneManager.LoadSceneAsync("ShopScene");
        
    }
}