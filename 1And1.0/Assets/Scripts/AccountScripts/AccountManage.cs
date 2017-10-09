using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountManage : MonoBehaviour
{
    
    //Frames
    public GameObject username;
    public GameObject password;
    public GameObject email;

    //Username Variables
    public InputField usernameInputField;
    public GameObject userResultObj;
    public Text userResultText;

    //Password Variables
    public InputField passwordInputOldField;
    public InputField passwordInputNewField;
    public GameObject passwordResultObj;
    public Text passwordResultText;

    //Email Variables
    public InputField emailInputField;
    public GameObject emailResultObj;
    public Text emailResultText;

    string changeusernameurl = "http://sigmastudios.tk/FlyingRocket/changeFRUsername12.php";
    string changepassurl = "http://sigmastudios.tk/FlyingRocket/changeFRPassword12.php";
    string changeemailurl = "http://sigmastudios.tk/FlyingRocket/changeFREmail12.php";

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
    public void ToAcc ()
    {
        SceneManager.LoadSceneAsync("AccountPanel");
    }
    public void AppearEnableUsername ()
    {
        password.SetActive(false);
        email.SetActive(false);
        username.SetActive(true);
    }
    public void AppearEnablePassword()
    {
        username.SetActive(false);
        email.SetActive(false);
        password.SetActive(true);
    }
    public void AppearEnableEmail()
    {
        email.SetActive(true);
        password.SetActive(false);
        username.SetActive(false);
    }

    public void ChangeUsername ()
    {
        string usernameInput = usernameInputField.text;
        StartCoroutine(ChangeNameRequest(usernameInput));
    }
    public void ChangePassword ()
    {
        string passwordInputOld = passwordInputOldField.text;
        string passwordInputNew = passwordInputNewField.text;
        StartCoroutine(ChangePasswordRequest(passwordInputOld, passwordInputNew));
    }
    public void ChangeEmail ()
    {
        string EmailInput = emailInputField.text;
        StartCoroutine(ChangeEmailRequest(EmailInput));
    }

    IEnumerator ChangeNameRequest (string usernamePost)
    {
        string hashKey = PlayerPrefs.GetString("changenamekey");
        string kys = "'" + PlayerPrefs.GetString("username") + "'" + "," + "'" + PlayerPrefs.GetString("password") + "'" + "," + "'" + hashKey + "'";
        string hash = GetHashString(kys);
        
        WWWForm form = new WWWForm();
        form.AddField("userID", PlayerPrefs.GetString("userID"));
        form.AddField("username", PlayerPrefs.GetString("username"));
        form.AddField("password", PlayerPrefs.GetString("password"));
        form.AddField("usernameToPost", usernamePost);
        form.AddField("hashPost", hash);
        WWW www = new WWW(changeusernameurl, form);
        yield return www;
        Debug.Log(www.text);
        if (www.text.Contains("1"))
        {
            PlayerPrefs.SetString("username", usernamePost);
            userResultObj.SetActive(true);
            userResultText.text = "Username has been changed.";
            //manager.SaveButton();
        }
        if (www.text.Contains("-1"))
        {
            userResultText.text = "Error when changing username.";
        }

    }
    IEnumerator ChangePasswordRequest (string passwordOld, string passwordNew)
    {
        string hashKey = PlayerPrefs.GetString("changepasskey");
        string kys = "'" + PlayerPrefs.GetString("userID") + "'" + "," + "'" + passwordOld + "'" + "," + "'" + passwordNew + "'" + "," + "'" + hashKey + "'";
        WWWForm form = new WWWForm();
        form.AddField("userID", PlayerPrefs.GetString("userID"));
        form.AddField("passwordOldPost", passwordOld);
        form.AddField("passwordNewPost", passwordNew);
        form.AddField("hash", GetHashString(kys));
        WWW www = new WWW(changepassurl, form);
        yield return www;
        if (www.text.Contains("1"))
        {
            PlayerPrefs.SetString("password", passwordNew);
            passwordResultObj.SetActive(true);
            passwordResultText.text = "Password has been changed.";
            //manager.SaveButton();
        } else
        {
            passwordResultObj.SetActive(true);
            passwordResultText.text = "Error.";
        }
    }
    IEnumerator ChangeEmailRequest (string email)
    {
        string hashKey = PlayerPrefs.GetString("changeemailkey");
        string kys = "'" + PlayerPrefs.GetString("userID") + "'" + "," + "'" + email + "'" + "," + "'" + hashKey + "'";
        string hash = GetHashString(kys);
        WWWForm form = new WWWForm();
        form.AddField("userID", PlayerPrefs.GetString("userID"));
        form.AddField("emailPost", email);
        form.AddField("hashPost", hash);
        WWW www = new WWW(changeemailurl, form);
        yield return www;
        Debug.Log(www.text);
        if (!www.text.Contains("1"))
        {
            emailResultObj.SetActive(true);
            emailResultText.text = "Error when changing email.";
        } else
        {
            emailResultObj.SetActive(true);
            emailResultText.text = "Email changed.";
        }
    }
}