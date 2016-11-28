using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviourEx, IHandle<UserShootMessage>, IHandle<PlayerDeadMessage>
{

    void Start()
    {
        this.ownRigidbody = GetComponent<Rigidbody2D>();
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

    private Rigidbody2D ownRigidbody;
}
