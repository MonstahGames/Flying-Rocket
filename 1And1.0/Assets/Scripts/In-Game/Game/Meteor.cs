using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Score score;
    public playerCollision playerState;
    public meteorSpawner spawner;

    void Start ()
    {
        score = FindObjectOfType<Score>();
        playerState = FindObjectOfType<playerCollision>();
        spawner = FindObjectOfType<meteorSpawner>();
    }
    void Update ()
    {
        #region SpeedBoosts
        if (PlayerPrefs.GetInt("isDead") != 1)
        {
            if (score.scoreInt == 2500)
            {

                spawner.spawnDelay -= 0.0025f;
            }
            if (score.scoreInt > 2500) speed += 0.05f;
            if (score.scoreInt > 5000)
            {
                speed += 0.05f;
            }
            if (score.scoreInt > 7500)
            {
                speed += 0.05f;

            }
            if (score.scoreInt > 10000) speed += 0.050f;
            if (score.scoreInt > 25000)
            {
                speed += 0.050f;
            }
            if (score.scoreInt > 50000) speed += 0.050f;
        }
        
        #endregion
    }                 
    void FixedUpdate ()
    {
        Vector2 forward = new Vector2(transform.up.x, transform.up.y);
        rb.MovePosition(rb.position - forward * Time.fixedDeltaTime * speed);
    }
}
