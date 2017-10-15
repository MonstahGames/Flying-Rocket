using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject accRegButton;
    public GameObject accPanelButton;

    public void Start ()
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
    #region Scenes
    public void ButtonToGame ()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }
    public void ExitGame ()
    {
        Application.Quit();
    } 
    public void Credits ()
    {
        SceneManager.LoadSceneAsync("CreditsPanel");
    }
    public void Options ()
    {
        SceneManager.LoadSceneAsync("ControlsPanel");
    }
    public void ToEndless ()
    {
        SceneManager.LoadSceneAsync("EndlessScene");
    }
    public void ToLB ()
    {
        SceneManager.LoadSceneAsync("LBScene");
    }
    public void ToShop ()
    {
        SceneManager.LoadSceneAsync("ShopScene");
    }
    #endregion
    public void ToAccPanel ()
    {
        if (PlayerPrefs.GetString("username") != null)
        {
            if (PlayerPrefs.GetString("password") != null)
            {
                SceneManager.LoadSceneAsync("AccountPanel");
            }
                  
        } else
        {
            Debug.Log("Account Not Valid.");
        }
    }
    public void ToSongs ()
    {
        SceneManager.LoadSceneAsync("DefaultSongs");
        //VV To be added back in 1.4
        //SceneManager.LoadSceneAsync("SongsPanel");
    }
}