using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    /*
     Copyright SigmaStudios (c) 2017
     All Rights Reserved.
     */

    public GameObject accRegButton;
    public GameObject accPanelButton;
    public GameObject rTextObj;

    public Text rText;

    void Start ()
    {
        if (PlayerPrefs.GetInt("highScore") != 0)
        {
                if (PlayerPrefs.GetInt("isRegistered") == 1)
                {
                    accRegButton.SetActive(false);
                    accPanelButton.SetActive(true);

                }
                if (PlayerPrefs.GetInt("isRegistered") == 0)
                {
                    accRegButton.SetActive(true);
                    accPanelButton.SetActive(false);
                }
        } else
        {
            accPanelButton.SetActive(false);
            accRegButton.SetActive(false);
        }     
    }
    
    public void ToAccPanel ()
    {
        if (PlayerPrefs.GetString("username") != null)
        {
            if (PlayerPrefs.GetString("password") != null)
            {
                if (Application.internetReachability != NetworkReachability.NotReachable)
                {
                    SceneManager.LoadSceneAsync("AccountPanel");
                    Debug.Log("Loaded Account Panel.");
                } else
                {
                    if (!rTextObj.activeInHierarchy)
                    {
                        rTextObj.SetActive(true);
                    }
                    rText.text = "No Internet Connection!";
                }
                
            } else
            {
                Debug.Log("Password is Null.");
            }
                  
        } else
        {
            Debug.Log("Account Not Valid.");
        }
    }
    #region Scenes
    public void ToSongs ()
    {
        SceneManager.LoadSceneAsync("DefaultSongs");
        //SceneManager.LoadSceneAsync("SongsPanel"); 1.4
    }
    public void ButtonToGame()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Credits()
    {
        SceneManager.LoadSceneAsync("CreditsPanel");
    }
    public void Options()
    {
        SceneManager.LoadSceneAsync("ControlsPanel");
    }
    public void ToEndless()
    {
        SceneManager.LoadSceneAsync("EndlessScene");
    }
    public void ToLB()
    {
        SceneManager.LoadSceneAsync("LBScene");
    }
    public void ToShop()
    {
        SceneManager.LoadSceneAsync("ShopScene");
    }
    #endregion
}