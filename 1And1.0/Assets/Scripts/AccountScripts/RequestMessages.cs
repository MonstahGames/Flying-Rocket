using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class RequestMessages : MonoBehaviour
{
    public inboxManager manager;
    public GameObject messagePrefab;
    public string requestURL = "http://sigmastudios.tk/FlyingRocket/requestFRMessages13.php";

    public int messagecount;

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
    public void Start ()
    {
        manager = FindObjectOfType<inboxManager>();
        StartCoroutine(requestMessages());
    } 
    IEnumerator requestMessages ()
    {
        string key = PlayerPrefs.GetString("reqmsgkey");
        string kys = "'" + PlayerPrefs.GetString("username") + "'" + "," + "'" + key + "'";
        string hash = GetHashString(kys);

        WWWForm form = new WWWForm();
        form.AddField("usernamePost", PlayerPrefs.GetString("username"));
        form.AddField("hash", hash);
        WWW www = new WWW(requestURL, form);
        yield return www;
        ShowMessages(www.text);
    }
    void ShowMessages (string text)
    {
        string[] messages = text.Split(';');
        messagecount = messages.Length;
        messagecount--;
        Debug.Log(messagecount);
        foreach (string message in messages)
        {
            if (message == "")
                continue;

            string[] kys = message.Split(',');
        }

    }
}
