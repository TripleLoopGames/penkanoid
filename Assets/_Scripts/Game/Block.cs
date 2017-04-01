using System;
using UnityEngine;
using System.Collections;
using System.Security.AccessControl;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Block : MonoBehaviourEx
{
    public Block Initialize(string ownId, Vector2 matrixPosition, string name, Action<string, bool> onBlockDeactivation)
    {
        this.name = String.Format("Block_{0}_{1}", name, ownId);
        this.ownId = ownId;
        this.onBlockDeactivation = onBlockDeactivation;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = this.LifeSprites[this.LifeAmount - 1];
        this.matrixPosition = matrixPosition;
        return this;
    }

    public Block EnableIgnoreCollisionResult()
    {
        this.ignoreCollisionResult = true;
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

    public string GetId()
    {
        return ownId;
    }

    public Vector2 GetMatrixPosition()
    {
        return matrixPosition;
    }

    public Block Kill()
    {
        if (destroyed)
        {
            return this;
        }
        if (this.itemOnHit != null)
        {
            SpawnItem();
        }
        destroyed = true;

        SoundData playIceBreak = new SoundData(GetInstanceID(), SRResources.Audio.Effects.Icebreak);
        Messenger.Publish(new PlayEffectMessage(playIceBreak));

        GameObject breakParticle = SRResources.Particles.IceBreak.Instantiate();
        breakParticle.name = "IceBreak_Particle";
        breakParticle.transform.SetParent(this.gameObject.transform.parent, false);
        breakParticle.transform.position = this.gameObject.transform.position;

        // custom behaviour for Explosive blocks
        this.onBlockDeactivation(this.ownId, this.Explosive);
        this.gameObject.SetActive(false);
        return this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.Indestructible)
        {
            return;
        }
        if (this.ignoreCollisionResult)
        {
            return;
        }
        GameObject collidedGameobject = collision.gameObject;
        bool hasCollidedWithBall = collidedGameobject.CompareTag(SRTags.Ball);
        if (hasCollidedWithBall)
        {
            LifeAmount--;
            if (LifeAmount > 0)
            {
                this.spriteRenderer.sprite = LifeSprites[LifeAmount - 1];
                // custom behaviour for cloak blocks
                if (this.Invisible)
                {
                    this.spriteRenderer.DOFade(0f, 1f);
                }
                return;
            }
            Kill();
        }
    }

    private Block SpawnItem()
    {
        this.itemOnHit.transform.SetParent(this.transform.parent);
        this.itemOnHit.SetActive(true);
        return this;
    }

    private bool ignoreCollisionResult;
    private string ownId;
    private Action<string, bool> onBlockDeactivation;
    private GameObject itemOnHit;
    private SpriteRenderer spriteRenderer;
    private Vector2 matrixPosition;
    private bool destroyed;

    public bool Indestructible;
    public bool Explosive;
    public bool Invisible;
    public int LifeAmount;
    public Sprite[] LifeSprites;

}



