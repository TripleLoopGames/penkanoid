using UnityEngine;

public class Ball : MonoBehaviourEx
{

    public override void Awake()
    {
       base.Awake();
       this.ownRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedGameobject = collision.gameObject;
        bool hasCollidedWithBlock = collidedGameobject.CompareTag(SRTags.Block);
        bool hasCollidedWithPlayer = collidedGameobject.CompareTag(SRTags.Player);
        if (hasCollidedWithBlock)
        {
            collidedGameobject.GetComponent<Block>().Die();
            return;
        }
        if (hasCollidedWithPlayer)
        {
            Messenger.Publish(new PlayerDeadMessage());
            return;
        }
    }

    public Ball Inititalize(Vector2 position, Vector2 direction, float magnitude)
    {
        this.gameObject.transform.position = position;
        this.ownRigidbody.AddForce(direction * magnitude, ForceMode2D.Impulse);
        return this;
    }

    private Rigidbody2D ownRigidbody;
}
