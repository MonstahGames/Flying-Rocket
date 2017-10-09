using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public Text username;

    public void Start ()
    {
        username.text = PlayerPrefs.GetString("username");
    }
    public void ToMain ()
    {
        SceneManager.LoadScene("MainMenu");
        PlayerPrefs.SetInt("isRegistered", 1);
    }
}
