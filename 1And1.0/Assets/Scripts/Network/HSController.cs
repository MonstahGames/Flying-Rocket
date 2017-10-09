using UnityEngine;
using System.Security.Cryptography;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.UI;

public class HSController : MonoBehaviour
{
    /*
    Copyright SigmaStudios (c) 2017
    All Rights Reserved.
    */

    private static HSController instance6;
    public GameObject errorObj;
    public Text errorTxt;
    public ProfileStats stats;


    public static HSController Instance
    {
        get { return instance6; }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance6 == null)
                instance6 = this;

        else if (instance6 != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        stats = FindObjectOfType<ProfileStats>();    

        if (PlayerPrefs.GetInt("IsBanned") == 0)
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                startPostScores();
                startGetScores();
                HSController.Instance.startGetScores();
            } else
            {
                errorObj.SetActive(true);
                errorTxt.text = "No Internet Connection!";
            }
        } else
        {
            errorObj.SetActive(true);
            Scrolllist.Instance.loading = false;
            errorTxt.text = "You have been banned from viewing the leaderboards.";
        }
    }
    public void Refresh ()
    {
        if (PlayerPrefs.GetInt("IsBanned") == 0)
        {
            startGetScores();
            startPostScores();
            HSController.Instance.startGetScores();
        } else
        {
            errorObj.SetActive(true);
            Scrolllist.Instance.loading = false;
        }
    }
    string highscoreURL = "http://sigmastudios.tk/FlyingRocket/display.php";
    string addScoreURL = "http://sigmastudios.tk/FlyingRocket/addscore.php";

    //for testing

     string uniqueID;
     string name3;
     int score;

    public string[] onlineHighscore;

    public void startGetScores()
    {
        StartCoroutine(GetScores());
    }
    public void ShowProfile ()
    {
        stats.ShowProfile();
    }
    public void startPostScores()
    {
        StartCoroutine(PostScores());
    }
    public void ToMenu ()
    {
        SceneManager.LoadSceneAsync("AccountPanel");
    }
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
    IEnumerator PostScores()
    {
        uniqueID = PlayerPrefs.GetString("userID");
        name3 = PlayerPrefs.GetString("username");

        score = PlayerPrefs.GetInt("highScore");
        string hasKey = PlayerPrefs.GetString("HSKey");
        string kys = "'" + name3 + "'" + "," + "'" + score + "'" + "," + "'" + hasKey + "'";
        string hash = GetHashString(kys);

        WWWForm form = new WWWForm();
        form.AddField("uniqueID", uniqueID);
        form.AddField("namePost", name3);
        form.AddField("scorePost", score);
        form.AddField("hashPost", hash);
        WWW hs_post = new WWW(addScoreURL, form);
        yield return hs_post;
    }
    IEnumerator GetScores()
    {
        Scrolllist.Instance.loading = true;

        WWW hs_get = new WWW(highscoreURL);

        yield return hs_get;
            string help = hs_get.text;

        onlineHighscore = help.Split(";"[0]);

        Scrolllist.Instance.loading = false;
        Scrolllist.Instance.getScrollEntrys();
    }
}
