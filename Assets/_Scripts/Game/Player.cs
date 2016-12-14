using System;
using UnityEngine;
using Random = UnityEngine.Random;
using PlayerConfig = Config.Player;
using BallConfig = Config.Ball;
using PathologicalGames;

public class Player : MonoBehaviourEx, IHandle<UserShootMessage>, IHandle<PlayerDeadMessage>, IHandle<UserDirectionMessage>
{
    public Player Initialize()
    {
        this.ownRigidbody = GetComponent<Rigidbody2D>();
        this.timer = GetComponent<TimerComponent>();
        this.gameObject.transform.position = PlayerConfig.InitialPosition;
        this.health = PlayerConfig.InitialHealth;
        return this;
    }

    public Player Damage()
    {
        if (this.invulnerable)
        {
            return this;
        }
        ChangeHealth(-1);
        GetComponent<Animator>().SetTrigger("Damage");
        if (this.health <= 0)
        {
            this.timer.StopTimer();
            Messenger.Publish(new PlayerDeadMessage());
            return this;
        }
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
        Vector2 targetPosition = new Vector2(xPosition * 13.07f, this.ownRigidbody.position.y);
        this.ownRigidbody.position = targetPosition;
    }

    public void Handle(UserShootMessage message)
    {
        GetComponent<Animator>().SetTrigger("Shoot");
        Vector2 randomDirection = new Vector2(Random.Range(-0.2f, 0.2f), 1);
        Vector2 spawnPosition = this.gameObject.transform.position;
        spawnPosition.y += 1.3f;
        Ball ball = this.ballPool.Spawn(SRResources.Game.Ball).GetComponent<Ball>();
        ball.Initialize(spawnPosition, () => this.ballPool.Despawn(ball.transform));
        ball.Shoot(randomDirection, 2, BallConfig.lifetime);        
    }

    public void Handle(PlayerDeadMessage message)
    {
        GetComponent<Animator>().SetBool("isAlive",false);
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
        this.invulnerable = false;
        return this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool pickupTrigger = collision.gameObject.CompareTag(SRTags.Pickup);
        if (!pickupTrigger)
        {
            return;
        }
        Pickup pickup = collision.gameObject.GetComponent<Pickup>();
        ItemStats pickupStats = pickup.GetStats();
        pickup.Destroy();
        ProcessStats(pickupStats);
    }

    private Player ProcessStats(ItemStats stats)
    {
        if (stats.HealthAmount != 0)
        {
            ChangeHealth(stats.HealthAmount);
        }
        if (stats.GameTime != 0)
        {
            AddTime(stats.GameTime);
        }
        if (stats.InvincibilityTime != 0)
        {
            AddInvencibility(stats.InvincibilityTime);
        }
        return this;
    }

    private Player ChangeHealth(int variance)
    {
        this.health += variance;
        Messenger.Publish(new PlayerChangeHealthMessage(this.health));
        return this;
    }

    private Player AddTime(int time)
    {
        Messenger.Publish(new ModifyTimeMessage(time));
        return this;
    }

    private Player AddInvencibility(int time)
    {
        this.invulnerable = true;
        this.timer.StartTimer(time, () => {
            this.invulnerable = false;
        });
        return this;
    }

    private bool invulnerable = false;
    private Rigidbody2D ownRigidbody;
    private TimerComponent timer;
    private SpawnPool ballPool;
    private int health;
}
