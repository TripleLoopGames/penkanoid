using DG.Tweening;
using RSG;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinGameScreen : MonoBehaviourEx
{

    public WinGameScreen Initialize()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        bool[] activated = transforms.Select(currentTransform =>
        {
            if (currentTransform.name == "PlayAgain")
            {
                this.playAgain = currentTransform.gameObject.GetComponent<Button>();              
                return true;
            }
            if (currentTransform.name == "Title")
            {
                this.title = currentTransform.gameObject;
                return true;
            }
            if (currentTransform.name == "Info")
            {
                this.info = currentTransform.gameObject;
                return true;
            }
            if (currentTransform.name == "WinGame")
            {
                this.background = currentTransform.gameObject;
                return true;
            }
            return false;
        }).ToArray();
        int activatedAmount = activated.Where(element => element).Count();
        if (activatedAmount != 4)
        {
            Debug.LogError("Cound not find proper amount of elements");
        }
        return this;
    }

    public IPromise Show()
    {
        this.playAgain.gameObject.SetActive(true);
        this.info.gameObject.SetActive(true);
        this.title.gameObject.SetActive(true);
        this.background.GetComponent<Image>().enabled = true;

        this.playAgain.interactable = false;

        Func<GameObject, Tween> tweenWinFactory = (GameObject targetObject) =>
       {
           return targetObject.transform.DOScale(0, 1.2f)
           .From()
           .SetEase(Ease.OutElastic, 0.4f);
       };
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(tweenWinFactory(this.playAgain.gameObject));
        mySequence.Insert(0, tweenWinFactory(this.info));
        mySequence.Insert(0, tweenWinFactory(this.title));
        mySequence.OnComplete(() => this.playAgain.interactable = true);
        Promise promise = new Promise();
        UnityAction onClick = () =>
        {
            SoundData playRestart = new SoundData(GetInstanceID(), SRResources.Audio.Effects.Confirm);
            Messenger.Publish(new PlayEffectMessage(playRestart));
            promise.Resolve();
        };
        promise.Then(() =>
        {
            Promise playAgainPromise = new Promise();
            this.playAgain.onClick.RemoveListener(onClick);
            return playAgainPromise;
        });

        this.playAgain.onClick.AddListener(onClick);
      
        return promise;
    }

    public WinGameScreen Hide()
    {
        this.playAgain.gameObject.SetActive(false);
        this.info.SetActive(false);
        this.title.SetActive(false);
        this.background.GetComponent<Image>().enabled = false;
        return this;
    }

    public WinGameScreen SetInfo(int time = 0, int tries = 0)
    {
        Text text = this.info.GetComponent<Text>();
        text.text = $"You've proven yourself a <b>worthy volcano.</b> \n And you did it in only <b>{time}</b> seconds... \n After <b>{tries}</b> tries, of course.";
        return this;
    }

    private Button playAgain;
    private GameObject info;
    private GameObject title;
    private GameObject background;

}
