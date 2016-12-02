using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviourEx, IHandle<UserShootMessage>, IHandle<PlayerDeadMessage>, IHandle<UserDirectionMessage>
{
    public void Handle(UserDirectionMessage message)
    {
        // magic number to transform position :(
        float xPosition = message.Position.x;
        if (message.Position.x > 0.7)
        {
            xPosition = 0.7f;
        }
        if (message.Position.x < -0.7)
        {
            xPosition = -0.7f;
        }
        Vector2 targetPosition = new Vector2(message.Position.x*13.07f, this.ownRigidbody.position.y);
        this.ownRigidbody.position = targetPosition;
    }

    public void Handle(UserShootMessage message)
    {
        Ball ball = SRResources.Game.Ball.Instantiate().GetComponent<Ball>();
        Vector2 randomDirection = new Vector2(Random.Range(-0.2f, 0.2f), 1);
        Vector2 spawnPosition = this.gameObject.transform.position;
        spawnPosition.y += 1.3f;
        ball.Inititalize(spawnPosition, randomDirection, 2);
    }

    public void Handle(PlayerDeadMessage message)
    {
        GetComponent<Animator>().SetBool("isAlive",false);
    }

    //void FixedUpdate()
    //{
    //    if(this.gravityDirection == null)
    //    {
    //        return;
    //    }
    //    this.ownRigidbody.AddForce(this.gravityDirection * 50 * this.ownRigidbody.mass);
    //}

    private void Start()
    {
        this.ownRigidbody = GetComponent<Rigidbody2D>();
    }

    private Rigidbody2D ownRigidbody;
    // private Vector2 gravityDirection;
}
