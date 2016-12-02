using UnityEngine;

public class UserDirectionMessage
{
    public Vector2 Position { get; set; }

    public UserDirectionMessage(Vector2 position)
    {
        this.Position = position;
    }
}