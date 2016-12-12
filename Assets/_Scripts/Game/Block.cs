using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    public Block Initialize(int id, Action<int> onBlockDeactivation)
    {
        this.name = "Block_" + id;
        this.id = id;
        this.parent = GetComponentInParent<Level>();
        this.onBlockDeactivation = onBlockDeactivation;
        return this;
    }

    public Block SetItemOnHit(GameObject item)
    {
        this.itemOnHit = item;
        this.itemOnHit.transform.position = this.gameObject.transform.position;
        this.itemOnHit.transform.SetParent(this.gameObject.transform);
        this.itemOnHit.SetActive(false);
        return this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedGameobject = collision.gameObject;
        bool hasCollidedWithBall = collidedGameobject.CompareTag(SRTags.Ball);
        if (hasCollidedWithBall)
        {
            if (this.itemOnHit != null)
            {
                SpawnItem();
            }
            this.onBlockDeactivation(this.id);
            this.gameObject.SetActive(false);
        }
    }

    private Block SpawnItem()
    {
        this.itemOnHit.transform.SetParent(this.parent.transform);
        this.itemOnHit.SetActive(true);
        return this;
    }

    private int id;
    private Action<int> onBlockDeactivation;
    private GameObject itemOnHit;
    private Level parent;
}



