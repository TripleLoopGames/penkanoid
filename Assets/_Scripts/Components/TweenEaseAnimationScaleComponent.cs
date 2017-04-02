using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DG.Tweening;
using RSG;
using UnityEngine;
using UnityEngine.Events;

public static class TweenEaseAnimationScaleComponent
{
    private static Tween AssignTween(GameObject targetObject, string tweenType)
    {
        if (tweenType == "scale")
        {
            return ScaleTween(targetObject);
        }
        return null;
    }

    private static Tween ScaleTween(GameObject targetObject){
        return targetObject.transform.DOScale(0, 1.2f)
        .From()
        .SetEase(Ease.OutElastic, 0.4f);
    }

    public static void CreateSequence(string sequenceId, GameObject[] targetObjects, Action afterCompleteAnimation = null, float timeBetweenAnimations = 0, float delay = 0)
    {
        if (DOTween.TweensById(sequenceId, true) != null)
        {
            return;
        }

        float currentTime = 0 + delay;

        Sequence mySequence = DOTween.Sequence();
        mySequence.SetId(sequenceId);
        foreach (GameObject targetObject in targetObjects)
        {
            mySequence.Insert(currentTime, AssignTween(targetObject, "scale"));
            currentTime += timeBetweenAnimations;
        }
        mySequence.OnComplete(() => afterCompleteAnimation());
    }

}
