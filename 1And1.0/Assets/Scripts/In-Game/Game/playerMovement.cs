using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 left;
    public Vector2 right;

    public GameObject leftButton;
    public GameObject rightButton;

    public void Start ()
    {
        if (PlayerPrefs.GetInt("InputValue") == 0)
        {
            return;
        }
        if (PlayerPrefs.GetInt("InputValue") == 1)
        {
            leftButton.SetActive(false);
            rightButton.SetActive(false);
        }
    }
    public void Update ()
    {
        if (PlayerPrefs.GetInt("InputValue") == 1)
        {
            return;
            //^ Removed Tilt
            if (PlayerPrefs.GetInt("paused") == 0)
            {
                transform.Translate(Input.acceleration.x, 0, 0);
            }
            
        }
    }

    public void ButtonLeft ()
    {     //animSource.Play("clipLeft");
        rb.MovePosition(rb.position + left);
    }
    public void ButtonRight ()
    {
        rb.MovePosition(rb.position + right);
    }

    
}
