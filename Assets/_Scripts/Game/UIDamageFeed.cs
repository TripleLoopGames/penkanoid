using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIDamageFeed : MonoBehaviour {

    public UIDamageFeed Initialize()
    {
        this.imageFeed = gameObject.GetComponent<Image>();
        return this;
    }

    public void AnimationDamageFeed()
    {
        imageFeed.DOColor(Color.red, 1.2f)
        .SetEase(Ease.OutElastic, 0.4f)
        .SetLoops(1, LoopType.Yoyo);
    }

    private Image imageFeed;
}
