using UnityEngine;

public class displayRocket : MonoBehaviour
{
    public SpriteRenderer pRenderer;
    public int rocketID;
    public Sprite[] rocketSprites;

    public void Start ()
    {
        rocketID = PlayerPrefs.GetInt("currentRocketID");
        pRenderer.sprite = rocketSprites[rocketID];

        if (rocketID == 4) transform.localScale = new Vector3(0.15f, 0.25f, 0);
    }
}
