using System;
using UnityEngine;
using Random = UnityEngine.Random;
using PlayerConfig = Config.Player;
using BallConfig = Config.Ball;
using PathologicalGames;

public class Player : MonoBehaviourEx, IHandle<UserShootMessage>, IHandle<PlayerDeadMessage>, IHandle<UserDirectionMessage>
{
    public Player Damage()
    {
        this.health -= 1;
        if(this.health <= 0)
        {
            Messenger.Publish(new PlayerChangeHealthMessage(0));
            Messenger.Publish(new PlayerDeadMessage());
            return this;
        }
        Messenger.Publish(new PlayerChangeHealthMessage(this.health));
        return this;
    }

    public void Handle(UserDirectionMessage message)
    {
        // magic number to transform position :(
        float xPosition = message.Position.x;
        if (xPosition > 0.7)
        {
            xPosition = 0.7f;
        }
        if (xPosition < -0.7)
        {
            xPosition = -0.7f;
        }
        Vector2 targetPosition = new Vector2(xPosition* 13.07f, this.ownRigidbody.position.y);
        this.ownRigidbody.position = targetPosition;
    }

    public void Handle(UserShootMessage message)
    {
        Vector2 randomDirection = new Vector2(Random.Range(-0.2f, 0.2f), 1);
        Vector2 spawnPosition = this.gameObject.transform.position;
        spawnPosition.y += 1.3f;
        Ball ball = this.ballPool.Spawn(SRResources.Game.Ball).GetComponent<Ball>();
        ball.Shoot(spawnPosition, randomDirection, 2, BallConfig.lifetime, (transfrom) => this.ballPool.Despawn(transfrom));        
    }

    public void Handle(PlayerDeadMessage message)
    {
        GetComponent<Animator>().SetBool("isAlive",false);
    }

    public Player Initialize()
    {
        this.ownRigidbody = GetComponent<Rigidbody2D>();
        this.gameObject.transform.position = PlayerConfig.InitialPosition;
        this.health = PlayerConfig.InitialHealth;
        return this;
    }


    public Player SetBallPool(SpawnPool ballPool)
    {
        this.ballPool = ballPool;
        return this;
    }

    public Player Reset()
    {
        this.gameObject.transform.position = PlayerConfig.InitialPosition;
        GetComponent<Animator>().SetBool("isAlive", true);
        this.health = PlayerConfig.InitialHealth;
        return this;
    }

    private Rigidbody2D ownRigidbody;
    private SpawnPool ballPool;
    private int health;
}
