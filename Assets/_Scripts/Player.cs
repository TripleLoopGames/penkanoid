using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviourEx, IHandle<UserDirectionMessage>
{

    public void Handle(UserDirectionMessage message)
    {
        this.ownRigidbody.AddForce(message.Force * message.Magnitude, ForceMode2D.Force);
    }

    void Start()
    {
        this.ownRigidbody = GetComponent<Rigidbody2D>();
    }

    private Rigidbody2D ownRigidbody;
}
