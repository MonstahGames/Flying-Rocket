﻿using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManagerLB : MonoBehaviour
{
    #region Variables
    public InputField usernameBox;
    public InputField passwordBox;
    public InputField emailBox;
    public Text rText;
    public GameObject rTextObj;
    public DataManager manager;

    
    string CreateUserURL = "http://sigmastudios.tk/FlyingRocket/createFRAccount.php";
    string userexistsURL = "http://sigmastudios.tk/FlyingRocket/nameExists13.php";
    string privacyPolicyURL = "http://sigmastudios.tk/FlyingRocket/privacypolicy.htm";
    #endregion

    #region CreatePrep
    public void createAccountButton ()
    {
        string usernameInput = usernameBox.text;
        string passwordInput = passwordBox.text;
        string emailInput = emailBox.text;

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            rTextObj.SetActive(true);
            rText.text = "No internet Connection. Is your WiFi on?";
            return;
        }
        if (usernameInput == "" || passwordInput == "" || emailInput == "")
        {
            rTextObj.SetActive(true);
            rText.text = "1 or more fields are blank.";
            return;
        }
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            rTextObj.SetActive(true);
            rText.text = "You cannot sign up with a Data Network. Try using WiFi.";
            return;
        } else
        {
            if (usernameInput.Contains(";"))
            {
                rTextObj.SetActive(true);
                rText.text = "You cannot use ; in your username.";
            }
            else
            {
                StartCoroutine(NameExists(usernameInput, passwordInput, emailInput));          
            }
        }
    }

    public void Start()
    {
        manager = FindObjectOfType<DataManager>();
    }
    #endregion

    #region Hash
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
    #endregion

    #region CheckIfExists
    IEnumerator NameExists(string username, string pass, string email)
    {
        string hashKey = PlayerPrefs.GetString("nameExistsKey");
        string hash = "'" + username + "'" + "," + "'" + hashKey + "'";
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("hashPost", GetHashString(hash));
        WWW www = new WWW(userexistsURL, form);
        yield return www;
        ProcessNameExists(www.text, username, pass, email);
    }
    void ProcessNameExists (string result, string username, string password, string email)
    {
        if (result == "1")
        {
            StartCoroutine(CreateAccount(username, password, email));
        }
        if (result == "-1")
        {
            rTextObj.SetActive(true);
            rText.text = "Username is already taken.";
        }
    }
    #endregion

    #region CreateAccount
    IEnumerator CreateAccount (string usernameInput, string passwordInput, string emailInput)
    {
        string hashKey = "createaccHashKey6501";
        string hashx = "'" + usernameInput + "'" + "," + "'" + passwordInput + "'" + "," + "'" + emailInput + "'" + "," + "'" + hashKey + "'";
        WWWForm requestCreateAccount = new WWWForm();
        requestCreateAccount.AddField("usernamePost", usernameInput);
        requestCreateAccount.AddField("passwordPost", passwordInput);
        requestCreateAccount.AddField("emailPost", emailInput);
        requestCreateAccount.AddField("hashPost", GetHashString(hashx));
        WWW WWW = new WWW(CreateUserURL, requestCreateAccount);
        yield return WWW;
        Debug.Log(WWW.text);
        if (WWW.text.Contains("This username is already taken."))
        {
            rTextObj.SetActive(true);
            rText.text = "This username has already been taken.";
        } else
        {
            InitializeLogin(usernameInput, passwordInput);
        }
             
    }
    public void InitializeLogin (string username, string password)
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password", password);
        SetData();
        SceneManager.LoadScene("AccountManagement");
        

        Debug.Log("Username: " + PlayerPrefs.GetString("username") + " Password: " + PlayerPrefs.GetString("password"));
    }
    #endregion

    #region Misc
    public void ToMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void SetData ()
    {
        //A
    }
    public void ToLogin ()
    {
        SceneManager.LoadScene("LoginPanel");
    }
    public void OpenPolicy ()
    {
        Application.OpenURL(privacyPolicyURL);
    }
    #endregion

}