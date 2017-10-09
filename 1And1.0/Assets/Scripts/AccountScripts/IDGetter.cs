using System.Collections;
using UnityEngine;

public class IDGetter : MonoBehaviour
{
    string IDgetURL = "http://sigmastudios.tk/FlyingRocket/getID11.php";

    public void Start ()
    {
        if (PlayerPrefs.GetString("username") == null)
        {
            Debug.Log("Username is Null.");
        } else
        {
            StartCoroutine(IDGet());
        }
        
    }
    IEnumerator IDGet ()
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", PlayerPrefs.GetString("username"));
        WWW www = new WWW(IDgetURL, form);
        yield return www;
        
        string userIDtext = www.text;
        PlayerPrefs.SetString("userID", userIDtext);
    }
}
