using UnityEngine;

public class Ball : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedGameobject = collision.gameObject;
        bool hasCollidedWithBlock = collidedGameobject.CompareTag(SRTags.Block);
        if (!hasCollidedWithBlock)
        {
            return;
        }
        collidedGameobject.GetComponent<Block>().Die();
    }

    void Start()
    {
        Vector2 randomForce = new Vector2(0.7f, 0.7f);
        this.ownRigidbody = GetComponent<Rigidbody2D>();
        this.ownRigidbody.AddForce(randomForce * 1.5f, ForceMode2D.Impulse);
    }

    private Rigidbody2D ownRigidbody;
}
