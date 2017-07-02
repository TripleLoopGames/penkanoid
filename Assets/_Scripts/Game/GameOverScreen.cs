using DG.Tweening;
using RSG;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviourEx
{

    public GameOverScreen Initialize()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        bool[] activated = transforms.Select(currentTransform =>
        {
            if (currentTransform.name == "Restart")
            {
                this.tryAgain = currentTransform.gameObject.GetComponent<Button>();               
                return true;
            }
            if (currentTransform.name == "Return")
            {
                this.backToMenu = currentTransform.gameObject.GetComponent<Button>();
                return true;
            }
            if (currentTransform.name == "Title")
            {
                this.title = currentTransform.gameObject;
                return true;
            }
            if (currentTransform.name == "Volkie")
            {
                this.volkie = currentTransform.gameObject;
                return true;
            }
            if (currentTransform.name == "GameOverScreen")
            {
                this.background = currentTransform.gameObject;
                return true;
            }
            return false;
        }).ToArray();
        int activatedAmount = activated.Where(element => element).Count();
        if (activatedAmount != 5)
        {
            Debug.LogError("Cound not find proper amount of elements");
        }
        return this;
    }

    public IPromise<bool> Show()
    {
        this.background.GetComponent<Image>().enabled = true;
        this.tryAgain.gameObject.SetActive(true);
        this.backToMenu.gameObject.SetActive(true);
        this.title.SetActive(true);
        this.volkie.SetActive(true);

        this.tryAgain.interactable = false;
        this.backToMenu.interactable = false;

        return EnterAnimation()
            .Then((value) => Promise<bool>.Race(new Promise<bool>((resolve, reject) =>
            {
                UnityAction onClick = null;
                onClick = () =>
                {
                    SoundData playRestart = new SoundData(GetInstanceID(), SRResources.Audio.Effects.Confirm);
                    Messenger.Publish(new PlayEffectMessage(playRestart));
                    this.tryAgain.onClick.RemoveListener(onClick);
                    resolve(true);
                };
                this.tryAgain.onClick.AddListener(onClick);
            }),
            new Promise<bool>((resolve, reject) =>
            {
                UnityAction onClick = null;
                onClick = () =>
                {
                    SoundData playRestart = new SoundData(GetInstanceID(), SRResources.Audio.Effects.Confirm);
                    Messenger.Publish(new PlayEffectMessage(playRestart));
                    this.backToMenu.onClick.RemoveListener(onClick);
                    resolve(false);
                };
                this.backToMenu.onClick.AddListener(onClick);
            }))
            );
    }

    public GameOverScreen Hide()
    {
        this.background.GetComponent<Image>().enabled = false;
        this.tryAgain.gameObject.SetActive(false);
        this.backToMenu.gameObject.SetActive(false);
        this.title.SetActive(false);
        this.volkie.SetActive(false);
        return this;
    }

    private IPromise<IEnumerable<bool>> EnterAnimation()
    {
        return Promise<bool>.All
        (
             new Promise<bool>((resolve, reject) =>
             {
                 Vector2 targetPosition = this.tryAgain.GetComponentInChildren<WaypointUi>().GetPosition();
                 RectTransform rectTransform = this.tryAgain.GetComponent<RectTransform>();
                 rectTransform.DOMove(targetPosition, 1f, false)
                 .From()
                 .OnComplete(() =>
                 {
                     this.tryAgain.interactable = true; 
                     resolve(false);
                 });
             }),
             new Promise<bool>((resolve, reject) =>
             {
                 Vector2 targetPosition = this.backToMenu.GetComponentInChildren<WaypointUi>().GetPosition();
                 RectTransform rectTransform = this.backToMenu.GetComponent<RectTransform>();
                 rectTransform.DOMove(targetPosition, 1f, false)
                 .From()
                 .OnComplete(() =>
                 {
                     this.backToMenu.interactable = true;
                     resolve(false);
                 });
             }),
             new Promise<bool>((resolve, reject) =>
             {
                 Vector2 targetPosition = this.title.GetComponentInChildren<WaypointUi>().GetPosition();
                 RectTransform rectTransform = this.title.GetComponent<RectTransform>();
                 rectTransform.DOMove(targetPosition, 1f, false)
                 .From()
                 .OnComplete(() => resolve(false));
             }),
             new Promise<bool>((resolve, reject) =>
             {
                 Vector2 targetPosition = this.volkie.GetComponentInChildren<WaypointUi>().GetPosition();
                 RectTransform rectTransform = this.volkie.GetComponent<RectTransform>();
                 rectTransform.DOMove(targetPosition, 1f, false)
                 .From()
                 .OnComplete(() => resolve(false));
             })
        );
    }

    private Button tryAgain;
    private Button backToMenu;
    private GameObject title;
    private GameObject volkie;
    private GameObject background;
}
