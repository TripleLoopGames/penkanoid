using RSG;
using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviourEx
{
    public override void Awake()
    {
        base.Awake();
        this.ownRigidbody = GetComponent<Rigidbody2D>();
        this.timerComponent = GetComponent<TimerComponent>();

    }

    public Ball Initialize(Vector2 position, Action despawnBall, GameObject childParticleBall, Action despawnParticleBall)
    {
        childParticleBall.transform.SetParent(this.transform);
        this.ballParticles = childParticleBall.GetComponent<ParticleSystem>();
        this.autodestroyParticlesComponent = childParticleBall.GetComponent<AutodestroyParticlesComponent>();

        this.gameObject.transform.position = position;
        this.despawnOwn = () =>
        {
            this.ballParticles.Stop();
            this.ballParticles.gameObject.transform.SetParent(null);
            this.autodestroyParticlesComponent.SetDeactivateMethod(despawnParticleBall);
            despawnBall();
        };

        this.ballParticles.Play();
        return this;
    }

    public Ball Shoot(Vector2 direction, float magnitude, int lifeTime)
    {
        this.ownRigidbody.AddForce(direction * magnitude, ForceMode2D.Impulse);
        this.timerComponent.StartTimer(lifeTime, this.despawnOwn);
        return this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedGameobject = collision.gameObject;
        bool hasCollidedWithPlayer = collidedGameobject.CompareTag(SRTags.Player);
        bool hasCollidedWithScenario = collidedGameobject.CompareTag(SRTags.Scenario);
        bool hasCollidedWithLevel = collidedGameobject.CompareTag(SRTags.Level);
        if (hasCollidedWithPlayer)
        {
            collidedGameobject.GetComponent<Player>().Damage();
            this.despawnOwn();
            return;
        }
        if (hasCollidedWithScenario)
        {
            SoundData playWallHit = new SoundData(GetInstanceID(), SRResources.Audio.Effects.ReboteProyectil);
            Messenger.Publish(new PlayEffectMessage(playWallHit));
            DissappearIfNecessary();
            return;
        }
        if (hasCollidedWithLevel)
        {
            SoundData playBlockHit = new SoundData(GetInstanceID(), SRResources.Audio.Effects.tick);
            Messenger.Publish(new PlayEffectMessage(playBlockHit));
            DissappearIfNecessary();
            return;
        }
    }

    // We want to avoid horizontal bounces that are too low
    // because the player has no way to avoid them
    private Ball DissappearIfNecessary()
    {
        if (IsHorizontalBounce())
        {
            Debug.Log("boom");
            this.despawnOwn();
        }
        return this;
    }

    private bool IsHorizontalBounce()
    {
        // ignore bounces that happen too high
        if (this.transform.position.y > -2f)
        {
            return false;
        }
        Vector2 direction = this.ownRigidbody.velocity;
        direction.Normalize();
        // we filter out the balls that go up
        if(direction.y > 0f)
        {
            return false;
        }  
        float angle = Vector2.Angle(direction, Vector2.right);
        // we take only the balls that have an horizontal bounce
        if(angle < 170 && angle > 10)
        {
            return false;
        }
        return true;
    }

    private Action despawnOwn;
    private Rigidbody2D ownRigidbody;
    private TimerComponent timerComponent;
    private ParticleSystem ballParticles;
    private AutodestroyParticlesComponent autodestroyParticlesComponent;
}
