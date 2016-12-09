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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedGameobject = collision.gameObject;
        bool hasCollidedWithPlayer = collidedGameobject.CompareTag(SRTags.Player);
        if (hasCollidedWithPlayer)
        {
            collidedGameobject.GetComponent<Player>().Damage();
            return;
        }
    }

    public Ball Shoot(Vector2 position, Vector2 direction, float magnitude, int lifeTime, Action<Transform> despawnBall)
    {
        this.gameObject.transform.position = position;
        this.ownRigidbody.AddForce(direction * magnitude, ForceMode2D.Impulse);
        this.timerComponent.StartTimer(lifeTime, value => { }, () => despawnBall(this.transform));
        return this;
    }

    private Rigidbody2D ownRigidbody;
    private TimerComponent timerComponent;
}
