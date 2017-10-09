using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManagerEndless : MonoBehaviour
{
    /*
     Copyright Sigma Studios (c) 2017
     All Rights Reserved.
    */

    public GameObject pausedPanel;
    public Score score;
    public Text scoreText;
    public Text highscoreText;

    public AudioSource source;
    public AudioClip[] loops;

    void Start ()
    {
        if (PlayerPrefs.GetInt("selectedSong") != 0)
        {
            if (PlayerPrefs.GetInt("selectedSong") == -1)
            {
                //Add NG Code
            }
            else
            {
                int i = PlayerPrefs.GetInt("selectedSong");
                switch (i)
                {
                    case 1:
                        source.clip = loops[0];
                        source.Play();
                        break;
                    case 2:
                        source.clip = loops[1];
                        source.Play();
                        break;
                    case 3:
                        source.clip = loops[2];
                        source.Play();
                        break;
                    case 4:
                        source.clip = loops[3];
                        source.Play();
                        break;
                }
            }
        } else
        {
            Debug.Log("No song selected.");
        }
        
    }
    public void PauseGame ()
    {
        Time.timeScale = 0;
        score.kys = true;
        source.Pause();
        pausedPanel.SetActive(true);


        int bugs;
        bugs = score.scoreInt--;
        scoreText.text = bugs.ToString();
        int highScoreInt = PlayerPrefs.GetInt("highScore");
        highscoreText.text = highScoreInt.ToString();
    }
    public void UnPause ()
    {
        Time.timeScale = 1;
        score.kys = false;
        source.UnPause();
        pausedPanel.SetActive(false);
    }
    public void ToMenu ()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void AppearShop ()
    {
        SceneManager.LoadSceneAsync("ShopScene", LoadSceneMode.Additive);
    }
}