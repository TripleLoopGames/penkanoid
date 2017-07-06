using System.Collections.Generic;
using DG.Tweening;
using RSG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Resources = SRResources.Intro.Ui;

public class IntroUi : MonoBehaviourEx
{
    public IntroUi Initialize()
    {
        InitializeBackground();
        InitializeCopyright();
        InitializeUIElements();
        return this;
    }

    public IntroUi SetCamera(Camera camera)
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = camera;
        // set canvas layer, needs camera
        canvas.sortingLayerID = SRSortingLayers.UI;
        return this;
    }

    private IntroUi InitializeBackground()
    {
        background = Resources.Background.Instantiate();
        background.name = "Background";
        background.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private IntroUi InitializeCopyright()
    {
        copyright = Resources.Copyright.Instantiate();
        copyright.name = "Copyright";
        copyright.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private IntroUi InitializeUIElements()
    {
        InitializeTitle();
        InitializeVolkieImage();
        InitializeStartButton();
        return this;
    }

    private IntroUi InitializeTitle()
    {
        title = Resources.Title.Instantiate();
        title.name = "Title";
        title.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private IntroUi InitializeVolkieImage()
    {
        volkie = Resources.Volkie.Instantiate();
        volkie.name = "Volkie";
        volkie.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private IntroUi InitializeStartButton()
    {
        GameObject startInstance = Resources.StartButton.Instantiate();
        startInstance.name = "StartButton";
        startInstance.transform.SetParent(this.gameObject.transform, false);
        start = startInstance.GetComponent<Button>();
        return this;
    }

    public IPromise Show()
    {
        this.background.GetComponent<Image>().enabled = true;
        this.title.GetComponent<Image>().enabled = true;
        this.start.gameObject.SetActive(true);
        this.volkie.GetComponent<Image>().enabled = true;

        this.start.interactable = false;

        return EnterAnimation()
            .Then(() => new Promise((resolve, reject) =>
            {
                UnityAction onClick = null;
                onClick = () =>
                {
                    SoundData playRestart = new SoundData(GetInstanceID(), SRResources.Audio.Effects.Confirm);
                    Messenger.Publish(new PlayEffectMessage(playRestart));
                    this.start.onClick.RemoveListener(onClick);
                    resolve();
                };
                this.start.onClick.AddListener(onClick);
            }));
    }

    private IPromise EnterAnimation()
    {
        return Promise.All
            (
                new Promise((resolve, reject) =>
                {
                    Vector2 targetPosition = this.title.GetComponentInChildren<WaypointUi>().GetPosition();
                    RectTransform rectTransform = this.title.GetComponent<RectTransform>();
                    rectTransform.DOMove(targetPosition, 1f, false)
                        .From()
                        .OnComplete(() => resolve());
                }),
                new Promise((resolve, reject) =>
                {
                    Vector2 targetPosition = this.volkie.GetComponentInChildren<WaypointUi>().GetPosition();
                    RectTransform rectTransform = this.volkie.GetComponent<RectTransform>();
                    rectTransform.DOMove(targetPosition, 1f, false)
                        .From()
                        .OnComplete(() => resolve());
                }),
                new Promise((resolve, reject) =>
                {
                    Vector2 targetPosition = this.start.GetComponentInChildren<WaypointUi>().GetPosition();
                    RectTransform rectTransform = this.start.GetComponent<RectTransform>();
                    rectTransform.DOMove(targetPosition, 1f, false)
                        .From()
                        .OnComplete(() =>
                        {
                            this.start.interactable = true;
                            resolve();
                        });
                })
            );
    }

    private void OnOptions()
    {
        Debug.Log("options Click");
    }

    private GameObject background;
    private GameObject copyright;
    private GameObject title;
    private GameObject volkie;
    private Button start;

}
