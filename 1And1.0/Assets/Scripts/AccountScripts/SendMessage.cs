using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SendMessage : MonoBehaviour
{
    string SendURL = "http://sigmastudios.tk/FlyingRocket/sendFRMessage12.php";
    public InputField usernameField;
    public InputField messageField;
    public Text resultText;
    public GameObject resultTextObj;

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
    IEnumerator SendMessageRequest (string username, string message)
    {
        string hashKey = PlayerPrefs.GetString("sendmsgkey");
        string hash = "'" + PlayerPrefs.GetString("username") + "'" + "," + "'" + username + "'" + "," + "'" + message + "'" + "," + "'" + hashKey + "'";
        WWWForm form = new WWWForm();
        form.AddField("fromPost", PlayerPrefs.GetString("username"));
        form.AddField("usernamePost", username);
        form.AddField("messagePost", message);
        form.AddField("hashPost", GetHashString(hash));
        WWW www = new WWW(SendURL, form);
        yield return www;
        resultTextObj.SetActive(true);
        resultText.text = www.text;
        Debug.Log(www.text);
    }
    public void SendButton ()
    {
        string usernameInput = usernameField.text;
        string messageInput = messageField.text;

        StartCoroutine(SendMessageRequest(usernameInput, messageInput));
    }
    public void ToInbox ()
    {
        SceneManager.LoadSceneAsync("PlayerInbox");
    }
}
