using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinLevel : MonoBehaviourEx {

    public WinLevel Initialize()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        bool[] activated = transforms.Select(currentTransform =>
        {
            if (currentTransform.name == "Image")
            {
                this.image = currentTransform.gameObject;
                return true;
            }
            return false;
        }).ToArray();
        int activatedAmount = activated.Where(element => element).Count();
        if (activatedAmount != 4)
        {
            Debug.LogWarning("Cound not find proper amount of elements");
        }
        return this;
    }

    public WinLevel Show()
    {
        this.gameObject.SetActive(true);

        Func<GameObject, Tween> tweenWinFactory = (GameObject targetObject) =>
        {
            return targetObject.transform.DOScale(0, 1.2f)
            .From()
            .SetEase(Ease.OutElastic, 0.4f);
        };
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(tweenWinFactory(this.image.gameObject));
        return this;
    }

    public WinLevel Hide()
    {
        this.gameObject.SetActive(false);
        return this;
    }

    private GameObject image;

}
