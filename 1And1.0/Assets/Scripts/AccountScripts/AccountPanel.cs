using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountPanel : MonoBehaviour
{
    public Text username;
    public Text highscore;
    public Text accstatusText;
    public Text balanceText;
    public GameObject buttonObj;
    public GameObject inboxObj;
    
    void Start ()
    {
        username.text = PlayerPrefs.GetString("username");
        highscore.text = PlayerPrefs.GetInt("highScore").ToString();
        balanceText.text = "$" + PlayerPrefs.GetInt("balance").ToString();

        CheckBanned();     
    }
    void Update ()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    void CheckBanned ()
    {
        if (PlayerPrefs.GetInt("IsBanned") == 1)
        {
            buttonObj.SetActive(false);
            inboxObj.SetActive(false);
            accstatusText.text = "Banned";
        }
        else
        {
            accstatusText.text = "Normal";
        }
    }
    #region Navigation
    public void ToMenu ()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void ToLeaderboards ()
    {
        SceneManager.LoadSceneAsync("TestScene");
    }
    public void ToManage ()
    {
        SceneManager.LoadSceneAsync("AccountManage");
    }
    public void ToInbox ()
    {
        SceneManager.LoadSceneAsync("InboxPanel");
    }
    #endregion
}
