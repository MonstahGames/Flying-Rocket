using UnityEngine.SceneManagement;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    public void ButtonToHome ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
