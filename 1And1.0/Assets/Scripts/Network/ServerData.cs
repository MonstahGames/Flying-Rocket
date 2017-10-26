using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;

public class ServerData : MonoBehaviour 
{
    /*
	 Copyright Sigma Studios (c) 2017
	 All Rights Reserved.
	*/

    public GameObject serverStatusFrame;
    public Text serverStatusText;

    string datalink = "http://sigmastudios.tk/FlyingRocket/getFRServerStatus13.php";

    void Start ()
    {
        StartCoroutine(ServerStatus());
    }
    IEnumerator ServerStatus ()
    {
        WWW www = new WWW(datalink);
        yield return www;

        if (www.text == "1")
        {
            ServerDown();
        } 
    }
    void ServerDown ()
    {
        serverStatusText.text = "We are currently doing maintenance on our server. Please check back later!";
        serverStatusFrame.SetActive(true);
    }
    public void Back ()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
