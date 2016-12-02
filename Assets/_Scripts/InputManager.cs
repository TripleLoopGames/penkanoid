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

    public InputManager Initialize()
    {
        if (SystemInfo.supportsGyroscope)
        {
            this.gyro = Input.gyro;
            this.gyro.enabled = true;
        }
        return this;
    }

    private void Update()
    {
        if (!this.inputEnabled)
        {
            return;
        }

#if UNITY_STANDALONE

        if (Input.GetKey(KeyCode.A))
        {
            if(this.direction > -0.7f)
            {
                this.direction += -0.5f*Time.deltaTime;
                Vector2 position = new Vector2(this.direction, 0);
                Messenger.Publish(new UserDirectionMessage(position));
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (this.direction < 0.7f)
            {
                this.direction += 0.5f*Time.deltaTime;
                Vector2 position = new Vector2(this.direction, 0);
                Messenger.Publish(new UserDirectionMessage(position));
            }           
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Messenger.Publish(new UserShootMessage());
        }

#elif UNITY_ANDROID
        if (this.gyro.enabled)
        {
            float currentDirection = Input.gyro.gravity.x; // theoretical limits (-1, 1) real limits(-0,7, 0.7)
            if (this.direction != currentDirection)
            {
                Messenger.Publish(new UserDirectionMessage(new Vector2(currentDirection, 0)));
                this.direction = currentDirection;
            }
        }
        if (Input.touchCount > 0)
        {
            if (!IsTouchPhaseBegan())
            {
                return;
            }
            Messenger.Publish(new UserShootMessage());
        }

#endif
    }

#if UNITY_ANDROID
    private bool IsTouchPhaseBegan()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }
#endif

    private float direction;
    private bool inputEnabled;
    private Gyroscope gyro;
}
