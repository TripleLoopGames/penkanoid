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

    private void Update()
    {
        if (!this.inputEnabled)
        {
            return;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Messenger.Publish(new UserDirectionMessage(Vector2.left,25));
        }
        if (Input.GetKey(KeyCode.D))
        {
            Messenger.Publish(new UserDirectionMessage(Vector2.right,25));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Messenger.Publish(new UserDirectionMessage("space"));
        }
    }

    private bool inputEnabled;
}
