using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : MonoBehaviourEx
{
    public StartBlock Initialize()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = this.BlockSprites[this.ballHits];
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
        Messenger.Publish(new ChangeSceneMessage(SRScenes.Game));
        this.gameObject.SetActive(false);
    }  

    private int ballHits = 0;
    public Sprite[] BlockSprites;
    private SpriteRenderer spriteRenderer;
}
