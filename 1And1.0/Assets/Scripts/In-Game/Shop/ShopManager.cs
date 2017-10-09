using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public void Back ()
    {
        SceneManager.UnloadSceneAsync("ShopScene");
    }
}
