using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ControlsScript : MonoBehaviour
{
    public GameObject highScoreResultText;
    public GameObject logOutResultTextObj;
    public Text logOutResultText;
    public GameObject SureFrame;
    public GameObject InputFrame;
    public DataManager manager;

    public void ChangeInput ()
    {
        InputFrame.SetActive(true);
    }
    public void RegularInput ()
    {
        PlayerPrefs.SetInt("InputValue", 0);
        InputFrame.SetActive(false);
    }
    public void TiltMotor ()
    {
        PlayerPrefs.SetInt("InputValue", 1);
        InputFrame.SetActive(false);
    }
    public void DisableInput ()
    {
        InputFrame.SetActive(false);
    }
    public void ResetHighScore ()
    {
        SureFrame.SetActive(true);     
    }
    public void DeleteHS ()
    {
        PlayerPrefs.SetInt("highScore", 0);
        highScoreResultText.SetActive(true);
        SureFrame.SetActive(false);
    }
    public void DisableHS ()
    {
        SureFrame.SetActive(false);
    }
    public void LogOut ()
    {
        if (PlayerPrefs.GetInt("isRegistered") == 1)
        {
            SaveData();
            UnloadPrefs();     
        } else
        {
            logOutResultText.text = "You are not logged in to an Account!";
        }
        logOutResultTextObj.SetActive(true);
    }
    void SaveData ()
    {
        manager.SaveButton();
    }
    public void UnloadPrefs ()
    {
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
        PlayerPrefs.SetInt("isRegistered", 0);
        PlayerPrefs.SetInt("userID", 0);
        PlayerPrefs.SetInt("IsBanned", 0);
    }
    #region Scenes
    public void ToAdvanced()
    {
        SceneManager.LoadSceneAsync("AdvancedOptions");
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}
