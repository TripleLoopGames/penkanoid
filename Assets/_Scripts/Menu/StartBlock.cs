using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : MonoBehaviourEx
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedGameobject = collision.gameObject;
        bool hasCollidedWithBall = collidedGameobject.CompareTag(SRTags.Ball);
        if (!hasCollidedWithBall)
        {
            return;        
        }
        this.ballHits++;
        if(this.ballHits < 3)
        {
            return;
        }
        Messenger.Publish(new ChangeSceneMessage(SRScenes.Game));
    }

    private int ballHits = 0;
}
