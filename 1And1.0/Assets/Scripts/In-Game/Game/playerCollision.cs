using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
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
                   
        int highScoreNumber = Convert.ToInt32(scoreText.text);
        #region earned
        if (highScoreNumber < 1000)
        {
            countEarnedScraps(1);
        } else if (highScoreNumber < 2000)
        {
            countEarnedScraps(2);
        } else if (highScoreNumber < 3000)
        {
            countEarnedScraps(3);
        } else if (highScoreNumber < 4000)
        {
            countEarnedScraps(4);
        } else if (highScoreNumber < 5000)
        {
            countEarnedScraps(5);
        } else if (highScoreNumber < 6000)
        {
            countEarnedScraps(6);
        } else if (highScoreNumber < 7000)
        {
            countEarnedScraps(7);
        }
        #endregion

        if (highScoreNumber >= PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", highScoreNumber);
        }
        SetText();
    }
    void countEarnedScraps (int earned)
    {
        balance = PlayerPrefs.GetInt("balance");
        int newBalance;
        newBalance = balance * earned;
        PlayerPrefs.SetInt("balance", newBalance);
        balanceText.text = "$" + newBalance;
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