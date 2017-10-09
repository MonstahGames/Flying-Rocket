using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class inboxManager : MonoBehaviour
{
    public RequestMessages requestMessages;

    public void Start ()
    {
        requestMessages = FindObjectOfType<RequestMessages>();
    }
    public void ToAccPanel ()
    {
        SceneManager.LoadSceneAsync("AccountPanel");
    }
}
