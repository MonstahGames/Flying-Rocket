using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginAccPanel : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Text resultText;
    public GameObject textObj;
    public DataManager manager;
    string LoginURL = "http://sigmastudios.tk/FlyingRocket/loginFRAccount.php";

    public static byte[] GetHash(string inputString)
    {
        HashAlgorithm algorithm = SHA512.Create();
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }
    public static string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("x2"));

        return sb.ToString();
    }

    public void LoginButton ()
    {
        string usernameInput = usernameField.text;
        string passwordInput = passwordField.text;
        StartCoroutine(TryLogin(usernameInput, passwordInput));
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Start ()
    {
        manager = FindObjectOfType<DataManager>();
    }
    IEnumerator TryLogin (string usernameInput, string passwordInput)
    {
        string hashKey = PlayerPrefs.GetString("loginkey");
        string kys = "'" + usernameInput + "'" + "," + "'" + passwordInput + "'" + "," + "'" + hashKey + "'";
        string hash = GetHashString(kys);
        WWWForm form = new WWWForm();
        form.AddField("usernameInput", usernameInput);
        form.AddField("passwordInput", passwordInput);
        form.AddField("hashPost", hash);
        WWW www = new WWW(LoginURL, form);
        yield return www;
        if (www.text == "1")
        {
            PlayerPrefs.SetInt("isRegistered", 1);
            PlayerPrefs.SetString("username", usernameInput);
            PlayerPrefs.SetString("password", passwordInput);
            SetData();
            SceneManager.LoadScene("MainMenu");
        } else
        {
            textObj.SetActive(true);
            if (www.text.Contains("11"))
            {
                resultText.text = "Account Invalid.";
               
            } else
            {
                resultText.text = www.text;
            }
        }
       
    }
    void SetData()
    {
        manager.LoadButton();
    }
}
