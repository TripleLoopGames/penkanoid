using UnityEngine;
using System.Collections;

public class InputDetector : MonoBehaviourEx
{

    public InputDetector EnableInput()
    {
        this.inputEnabled = true;
        return this;
    }

    public InputDetector DisableInput()
    {
        this.inputEnabled = false;
        return this;
    }

    public InputDetector Initialize()
    {
#if UNITY_ANDROID
        if (SystemInfo.supportsGyroscope)
        {
            this.gyro = Input.gyro;
            this.gyro.enabled = true;
        } else
        {
            this.gyroNotSupported = true;
            Debug.Log("The system doesn't support Gyroscope");
        }
#endif
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
        if (!this.gyroNotSupported && this.gyro.enabled)
        {
            float currentDirection = Input.gyro.gravity.x; // theoretical limits (-1, 1) real limits(-0,7, 0.7)
            if (this.direction != currentDirection)
            {
                Messenger.Publish(new UserDirectionMessage(new Vector2(currentDirection, 0)));
                this.direction = currentDirection;
            }
        }
        else
        {
            float currentDirection = Input.acceleration.x; // theoretical limits (-1, 1) real limits(-0,7, 0.7)
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

    private Gyroscope gyro;
    private bool gyroNotSupported;
#endif

    private float direction;
    private bool inputEnabled;
}
