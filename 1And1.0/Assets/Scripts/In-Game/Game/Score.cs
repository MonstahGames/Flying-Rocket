using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int scoreInt = 0;
    public Text scoreText;
    public float mediumSpeed = 1.5f;
    public GameObject meteor;
    public Meteor meteorScript;
    public bool kys = false;

    public GameObject[] one;
    public void Update ()
    {
        CountScore();
        if (kys)
        {
            scoreInt--;
        }
    }
    public void CountScore ()
    {
        scoreInt++;
        scoreText.text = scoreInt.ToString();
    }
}
