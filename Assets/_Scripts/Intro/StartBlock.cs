using DG.Tweening;
using System;
using UnityEngine;

public class StartBlock : MonoBehaviourEx
{
    public StartBlock Initialize(Action onDestroy)
    {
        this.ownCollider = GetComponent<Collider2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = this.BlockSprites[this.ballHits];
        this.onDestroy = onDestroy;
        return this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedGameobject = collision.gameObject;
        bool hasCollidedWithBall = collidedGameobject.CompareTag(SRTags.Ball);
        if (!hasCollidedWithBall)
        {
            return;
        }
        this.ballHits++;
        if (this.ballHits < 3)
        {
            this.spriteRenderer.sprite = this.BlockSprites[this.ballHits];
            return;
        }
        SoundData playIceBreak = new SoundData(GetInstanceID(), SRResources.Audio.Effects.Icebreak);
        Messenger.Publish(new PlayEffectMessage(playIceBreak));

        GameObject breakParticle = SRResources.Particles.IceBreak.Instantiate();
        breakParticle.name = "IceBreak_Particle";
        breakParticle.transform.SetParent(this.gameObject.transform.parent, false);
        breakParticle.transform.position = this.gameObject.transform.position;

        this.spriteRenderer.DOFade(0f, 0.3f);
        this.ownCollider.enabled = false;
        this.onDestroy();
    }

    private int ballHits = 0;
    public Sprite[] BlockSprites;
    private SpriteRenderer spriteRenderer;
    private Collider2D ownCollider;
    private Action onDestroy;
}
