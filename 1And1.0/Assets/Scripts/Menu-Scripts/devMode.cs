using UnityEngine;
using UnityEngine.UI;

public class devMode : MonoBehaviour
{
    public InputField inputField;
    public GameObject resultObj;
    public Text resultTxt;
    public GameObject devFrame;

    string key = "verlemionismydad123";
    public void EnterDev ()
    {
        
        if (inputField.text == key)
        {
            if (resultObj.activeInHierarchy)
            {
                resultObj.SetActive(false);
            }
            devFrame.SetActive(true);
        } else
        {
            resultObj.SetActive(true);
            resultTxt.text = "Password Incorrect.";
        }
    }
    public void Back ()
    {
        devFrame.SetActive(false);
    }
}
