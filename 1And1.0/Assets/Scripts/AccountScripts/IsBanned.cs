using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class IsBanned : MonoBehaviour
{
    public string url = "http://sigmastudios.tk/FlyingRocket/IsBanned.php";

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
    void Start ()
    {
        if (PlayerPrefs.GetString("username") != null)
        {
            StartCoroutine(RequestCheck());
        }
    }
    IEnumerator RequestCheck ()
    {
        string hashKey = PlayerPrefs.GetString("frbankey");
        string kys = "'" + PlayerPrefs.GetString("username") + "'" + "," + "'" + PlayerPrefs.GetString("password") + "'" + "," + "'" + hashKey + "'";
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", PlayerPrefs.GetString("username"));
        form.AddField("passwordPost", PlayerPrefs.GetString("password"));
        form.AddField("hashPost", GetHashString(kys));
        WWW www = new WWW(url, form);
        yield return www;
        UpdateBanStatus(www.text);
    }
    void UpdateBanStatus (string result)
    {
        if (!result.Contains("-"))
        {
            if (result.Contains("1"))
            {
                PlayerPrefs.SetInt("IsBanned", 1);
            }     
        }
        if (result.Contains("Not Banned"))
        {
            PlayerPrefs.SetInt("IsBanned", 0);
        }
    }
}
