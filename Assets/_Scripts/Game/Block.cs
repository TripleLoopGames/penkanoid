using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    public Block Initialize(int id)
    {
        this.name = "Block_" + id;
        this.parent = GetComponentInParent<Level>();
        return this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedGameobject = collision.gameObject;
        bool hasCollidedWithBall = collidedGameobject.CompareTag(SRTags.Ball);
        if (hasCollidedWithBall)
        {
            this.gameObject.SetActive(false);
        }
    }

    private Level parent;
}



