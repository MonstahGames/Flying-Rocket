using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public string SaveURL = "http://sigmastudios.tk/FlyingRocket/saveFRData13.php";
    public string LoadURL = "http://sigmastudios.tk/FlyingRocket/loadFRData13.php";

    public string key = PlayerPrefs.GetString("savedatakey");
    public string LoadKey = PlayerPrefs.GetString("loaddatakey");

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

    #region Saving
    public void SaveButton ()
    {
        string username = PlayerPrefs.GetString("username");
        string password = PlayerPrefs.GetString("password");
        int isBanned = PlayerPrefs.GetInt("IsBanned");
        StartCoroutine(RequestSaveData(username, password, isBanned));
    }
    IEnumerator RequestSaveData (string username, string password, int accStatus)
    {
        string score = PlayerPrefs.GetInt("highScore").ToString();
        string hash = "'" + username + "'" + "," + "'" + password + "'" + "," + "'" + key + "'";
        WWWForm form = new WWWForm();
        form.AddField("userID", PlayerPrefs.GetString("userID"));
        form.AddField("usernamePost", username);
        form.AddField("passwordPost", password);
        form.AddField("scorePost", score);
        form.AddField("accStatusPost", accStatus.ToString());
        form.AddField("hashPost", GetHashString(hash));
        WWW www = new WWW(SaveURL, form);
        yield return www;
        string savingResult = www.text;
        GetResultSave(savingResult);
    }
    void GetResultSave (string result)
    {
        if (result.Contains("1"))
        {
            //Replace this with text objects
            Debug.Log(result);
        } else
        {
            //replace with better shit like text objects lol
            Debug.Log(result);
        }
    }
    #endregion

    #region Loading

    public void LoadButton ()
    {
        string userID = PlayerPrefs.GetString("userID");
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //Text stuff here
        } else
        {
            StartCoroutine(RequestLoadData(userID));
        }
       
    }
    IEnumerator RequestLoadData (string uID)
    {
        string kys = "'" + uID + "'" + "," + "'" + LoadKey + "'";
        string hash = GetHashString(kys);
        WWWForm form = new WWWForm();
        form.AddField("userIDPost", uID);
        form.AddField("hashPost", hash);
        WWW www = new WWW(LoadURL, form);
        yield return www;
        string resultText = www.text;
        SetPrefs(resultText);
    }
    void SetPrefs(string resultText)
    {
        string[] data = resultText.Split(',');

        int hs = Convert.ToInt32(data[1]);
        int isBanned = Convert.ToInt32(data[3]);
        PlayerPrefs.SetString("username", data[0]);
        PlayerPrefs.SetInt("highScore", hs);
        PlayerPrefs.SetString("password", data[2]);
        PlayerPrefs.SetInt("isBanned", isBanned);


    }
    #endregion

}
