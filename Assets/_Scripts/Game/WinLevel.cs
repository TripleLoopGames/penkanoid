using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using RSG;
using UnityEngine.Events;

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
            if (currentTransform.name == "NextLevel")
            {
                this.nextLevel = currentTransform.GetComponent<Button>();
                return true;
            }
            return false;
        }).ToArray();
        int activatedAmount = activated.Where(element => element).Count();
        if (activatedAmount != 2)
        {
            Debug.LogError("Cound not find proper amount of elements");
        }
        return this;
    }

    public IPromise Show()
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

        return new Promise((resolve, rejection) =>
        {
            UnityAction resolvePromise = null;
            resolvePromise = () => {
                this.nextLevel.onClick.RemoveListener(resolvePromise);
                resolve();
            };          
            this.nextLevel.onClick.AddListener(resolvePromise);            
        });
        
    }

    public WinLevel Hide()
    {
        this.gameObject.SetActive(false);
        return this;
    }

    private GameObject image;
    private Button nextLevel;

}
