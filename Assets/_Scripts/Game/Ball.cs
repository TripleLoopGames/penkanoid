using System;
using UnityEngine;

public class Ball : MonoBehaviourEx
{
    public override void Awake()
    {
       base.Awake();
       this.ownRigidbody = GetComponent<Rigidbody2D>();
       this.timerComponent = GetComponent<TimerComponent>();
       
    }

    public Ball Initialize(Vector2 position, Action despawnBall, GameObject childParticleBall)
    {
        childParticleBall.transform.SetParent(this.transform);
        this.ballParticles = childParticleBall.GetComponent<ParticleSystem>();

        this.gameObject.transform.position = position;
        this.despawnOwn = () =>
        {
            ballParticles.Stop();
            ballParticles.gameObject.transform.SetParent(null);
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
            return;
        }
        if (hasCollidedWithLevel)
        {
            SoundData playBlockHit = new SoundData(GetInstanceID(), SRResources.Audio.Effects.tick);
            Messenger.Publish(new PlayEffectMessage(playBlockHit));
            return;
        }
    }

    private Action despawnOwn;
    private Rigidbody2D ownRigidbody;
    private TimerComponent timerComponent;
    private ParticleSystem ballParticles;
}
