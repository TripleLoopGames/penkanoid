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

    public Ball Initialize(Vector2 position, Action despawnBall)
    {
        this.gameObject.transform.position = position;
        this.despawnOwn = despawnBall;
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
        if (hasCollidedWithPlayer)
        {
            collidedGameobject.GetComponent<Player>().Damage();
            this.despawnOwn();
            return;
        }
    }

    private Action despawnOwn;
    private Rigidbody2D ownRigidbody;
    private TimerComponent timerComponent;
}
