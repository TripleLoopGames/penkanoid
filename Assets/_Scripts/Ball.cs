using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody2D ownRigidbody;
    //private float lastSpeed = 0;

    // Use this for initialization
    void Start()
    {
        Vector2 randomForce = new Vector2(0.7f, 0.7f);
        ownRigidbody = GetComponent<Rigidbody2D>();
        ownRigidbody.AddForce(randomForce * 5, ForceMode2D.Impulse);
    }

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

}
