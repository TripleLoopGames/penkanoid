using System;
using UnityEngine;
using Random = UnityEngine.Random;
using PlayerConfig = Config.Player;
using BallConfig = Config.Ball;
using PathologicalGames;
using System.Collections;

public class Player : MonoBehaviourEx, IHandle<UserShootMessage>, IHandle<UserDirectionMessage>
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
        if (this.invulnerable || this.damageInvulnerable ||  this.interactionsBlocked)
        {
            return this;
        }
        ChangeHealth(-1);
        GetComponent<Animator>().SetTrigger("Damage");
        SoundData playDamage = new SoundData(GetInstanceID(), SRResources.Audio.Effects.Damage);
        Messenger.Publish(new PlayEffectMessage(playDamage));
        StartCoroutine(DamageInvulnerability());        
        if (this.health <= 0)
        {
            this.timer.StopTimer();
            GetComponent<Animator>().SetBool("isAlive", false);
            StopInvulerability();
            Messenger.Publish(new PlayerDeadMessage());
            return this;
        }
        return this;
    }

    public Player StopInvulerability()
    {
        SoundData stopInvulnerability = new SoundData(GetInstanceID(), SRResources.Audio.Effects.Invincibility);
        Messenger.Publish(new StopEffectMessage(stopInvulnerability));
        this.timer.StopTimer();
        this.invulnerable = false;
        return this;
    }

    public Player BlockInteractions()
    {
        this.interactionsBlocked = true;
        return this;
    }

    public Player FullReset()
    {
        this.gameObject.transform.position = PlayerConfig.InitialPosition;
        GetComponent<Animator>().SetBool("isAlive", true);
        this.health = PlayerConfig.InitialHealth;
        this.timer.StopTimer();
        this.invulnerable = false;
        this.interactionsBlocked = false;
        this.damageInvulnerable = false;
        return this;
    }

    public Player NextLevelReset()
    {
        this.gameObject.transform.position = PlayerConfig.InitialPosition;
        this.timer.StopTimer();
        this.invulnerable = false;
        this.interactionsBlocked = false;
        this.damageInvulnerable = false;
        return this;
    }

    public void Handle(UserDirectionMessage message)
    {
        // magic number to transform position :(
        float xPosition = message.Position.x;
        if (xPosition > 0.25)
        {
            xPosition = 0.25f;
        }
        if (xPosition < -0.25)
        {
            xPosition = -0.25f;
        }
        float targetX = xPosition * 38.4f;
        this.direction = targetX - this.transform.position.x;
        float difference = Math.Abs(this.transform.position.x - (xPosition * 38.4f));
        this.speed = this.speedCurve.Evaluate(difference);
    }

    public void Handle(UserShootMessage message)
    {
        GetComponent<Animator>().SetTrigger("Shoot");
        Vector2 randomDirection = new Vector2(Random.Range(-0.2f, 0.2f), 1);
        Vector2 spawnPosition = this.gameObject.transform.position;
        spawnPosition.y += 1.3f;
        Ball ball = this.ballPool.Spawn(SRResources.Game.Ball).GetComponent<Ball>();
        ball.Initialize(spawnPosition, () => this.ballPool.Despawn(ball.transform));
        SoundData playShoot = new SoundData(GetInstanceID(), SRResources.Audio.Effects.VolcanoShot);
        Messenger.Publish(new PlayEffectMessage(playShoot));
        ball.Shoot(randomDirection, 2, BallConfig.lifetime);
    }

    public Player SetBallPool(SpawnPool ballPool)
    {
        this.ballPool = ballPool;
        return this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.interactionsBlocked)
        {
            return;
        }
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

    private void FixedUpdate()
    {
       this.ownRigidbody.MovePosition(new Vector2 (this.transform.position.x + this.direction * this.speed * Time.fixedDeltaTime, this.transform.position.y));
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
        SoundData playInvencibility = new SoundData(GetInstanceID(), SRResources.Audio.Effects.Invincibility, true);
        Messenger.Publish(new PlayEffectMessage(playInvencibility));
        this.invulnerable = true;
        this.timer.StartTimer(time, () =>
        {
            StopInvulerability();
        });
        return this;
    }

    private IEnumerator DamageInvulnerability()
    {
        this.damageInvulnerable = true;
        yield return new WaitForSeconds(0.5f);
        this.damageInvulnerable = false;
    }

    private float speed;
    private float direction;
    private bool damageInvulnerable = false;
    private bool invulnerable = false;
    private bool interactionsBlocked = false;
    private Rigidbody2D ownRigidbody;
    private TimerComponent timer;
    private SpawnPool ballPool;
    private int health;
    #pragma warning disable 0649 // variable assinged in the inspector.
    [SerializeField]
    private AnimationCurve speedCurve;
}
