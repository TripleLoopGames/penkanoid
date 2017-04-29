using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIDamageFeed : MonoBehaviourEx, IHandle<PlayerDamaged>
{

    public UIDamageFeed Initialize()
    {
        this.imageFeed = gameObject.GetComponent<Image>();
        return this;
    }

    private void AnimationDamageFeed()
    {
        imageFeed.DOColor(Color.red, 0.075f)
        .SetEase(Ease.Linear)
        .SetLoops(2, LoopType.Yoyo);
    }

    public void Handle(PlayerDamaged message)
    {
        AnimationDamageFeed();
    }

    private Image imageFeed;
    
}
