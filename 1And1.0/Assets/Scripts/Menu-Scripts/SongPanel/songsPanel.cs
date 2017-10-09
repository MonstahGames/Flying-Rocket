using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SongsPanel : MonoBehaviour
{
    public InputField songIDInput;

    public void ToDefault ()
    {
        SceneManager.LoadSceneAsync("DefaultSongs");
    }
    public void DownloadSong ()
    {
        //Add NG Download Code
    }
    public void Back ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
