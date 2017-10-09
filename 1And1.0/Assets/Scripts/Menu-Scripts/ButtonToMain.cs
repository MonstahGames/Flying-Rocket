using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonToMain : MonoBehaviour
{
    public void buttonToMain ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
