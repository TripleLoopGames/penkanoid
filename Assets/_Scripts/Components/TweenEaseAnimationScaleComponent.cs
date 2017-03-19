using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TweenEaseAnimationScaleComponent : MonoBehaviour
{

    public Tween createTweenEaseAnimation(GameObject targetObject){
        return targetObject.transform.DOScale(0, 1.2f)
        .From()
        .SetEase(Ease.OutElastic, 0.4f);
    }

}
