using UnityEngine;

public class UserDirectionMessage
{
    public Vector2 Force { get; set; }
    public float Magnitude { get; set; }

    public UserDirectionMessage(Vector2 force, float magnitude)
    {
        this.Force = force;
        this.Magnitude = magnitude;
    }
}