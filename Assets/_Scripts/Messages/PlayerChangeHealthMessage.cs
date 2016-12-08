using UnityEngine;

public class PlayerChangeHealthMessage
{
    public int Health { get; set; }

    public PlayerChangeHealthMessage(int health)
    {
        this.Health = health;
    }
}