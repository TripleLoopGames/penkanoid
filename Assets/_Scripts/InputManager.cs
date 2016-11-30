using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviourEx
{

    public InputManager EnableInput()
    {
        this.inputEnabled = true;
        return this;
    }

    public InputManager DisableInput()
    {
        this.inputEnabled = false;
        return this;
    }

    private void Start()
    {
        //if (SystemInfo.supportsGyroscope)
        //{
        //    this.gyro = Input.gyro;
        //    this.gyro.enabled = true;
        //}
    }

    private void Update()
    {
        if (!this.inputEnabled)
        {
            return;
        }
        //if (Input.gyro.enabled)
        //{
        //    float currentDirection = -Input.gyro.rotationRateUnbiased.z;
        //    if (this.direction != currentDirection)
        //    {
        //        Physics2D.gravity = new Vector2(currentDirection, 0);
        //        this.direction = currentDirection;
        //    }           
        //}
        if (Input.GetKey(KeyCode.A))
        {
            Physics2D.gravity = new Vector2(-1, 0);
            // Messenger.Publish(new UserDirectionMessage(Vector2.left,25));
        }
        if (Input.GetKey(KeyCode.D))
        {
            Physics2D.gravity = new Vector2(1, 0);
            // Messenger.Publish(new UserDirectionMessage(Vector2.right,25));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // Debug.Log("ho");
            Messenger.Publish(new UserShootMessage());
        }
    }


    private float direction;
    private bool inputEnabled;
    private Gyroscope gyro;
}
