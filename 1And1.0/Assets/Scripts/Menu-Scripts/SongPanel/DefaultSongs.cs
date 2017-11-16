using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefaultSongs : MonoBehaviour
{
    /*
     Copyright Sigma Studios (c) 2017
     All Rights Reserved.
    */

    #region Variables
    public GameObject infoPanel;

    public Text songText;
    public Text authorText;
    public Text urlText;

    string songName;
    string authorName;
    string urllink;
    #endregion

    #region GetInfo
    public void GetName (string name)
    {
        songName = name;
    }
    public void GetAuthor(string author)
    {
        authorName = author;
    }
    public void GetURL(string url)
    {
        urllink = url;
    }
    #endregion

    #region Panels
    public void EnableInfoPanel ()
    {
        infoPanel.SetActive(true);
        songText.text = songName;
        authorText.text = authorName;
        urlText.text = urllink;
    }
    public void CloseInfoPanel ()
    {
        infoPanel.SetActive(false);
    }
    #endregion

    #region Song
    public void OpenSongURL ()
    {
        Application.OpenURL(urllink);
    }
    public void SelectSong(int index)
    {
        PlayerPrefs.SetInt("selectedSong", index);
    }
    #endregion

    public void Back ()
    {
        SceneManager.LoadScene("MainMenu");
        //SceneManager.LoadScene("SongsPanel");
    }
}
