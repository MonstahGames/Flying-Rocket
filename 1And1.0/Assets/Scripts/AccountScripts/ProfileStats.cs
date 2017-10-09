using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ProfileStats : MonoBehaviour
{
    /*
     Copyright SigmaStudios (c) 2017
     All rights reserved.
    */

    public HSController controller;

    public GameObject profilePrefab;
    public Text username;
    public Text scoreText;

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

    public string requestURL = "http://sigmastudios.tk/FlyingRocket/getFRUserStats13.php";

    public void ShowProfile ()
    {
        string userID = PlayerPrefs.GetString("userID");
        StartCoroutine(GetStats(userID));
    }
    IEnumerator GetStats (string id)
    {
        string key = PlayerPrefs.GetString("getstatskey");
        string hash = id + key;
        WWWForm form = new WWWForm();
        form.AddField("userIDPost", id);
        form.AddField("hashPost", GetHashString(hash));
        WWW www = new WWW(requestURL, form);
        yield return www;
        Debug.Log(GetHashString(hash));
        Debug.Log(www.text);
        Debug.Log(id);
        //SetVariables(www.text);
    }
    void SetVariables (string result)
    {
        string[] results = result.Split(',');
        username.text = results[0];
        scoreText.text = results[1];
        profilePrefab.SetActive(true);
    }

}
