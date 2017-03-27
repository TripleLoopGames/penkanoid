using DG.Tweening;
using RSG;
using System;
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
            if (currentTransform.name == "Title")
            {
                this.title = currentTransform.gameObject;
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
        if (activatedAmount != 3)
        {
            Debug.LogError("Cound not find proper amount of elements");
        }
        return this;
    }

    public IPromise Show()
    {
        this.background.GetComponent<Image>().enabled = true;
        this.tryAgain.gameObject.SetActive(true);
        this.title.SetActive(true);

        this.tryAgain.interactable = false;

        TweenEaseAnimationScaleComponent.CreateSequence("gameOver", new GameObject[] { this.tryAgain.gameObject, this.title }, () => this.tryAgain.interactable = true);

        return new Promise((resolve, reject) =>
        {
            UnityAction onClick = null;
            onClick = () =>
            {
                SoundData playRestart = new SoundData(GetInstanceID(), SRResources.Audio.Effects.Confirm);
                Messenger.Publish(new PlayEffectMessage(playRestart));
                this.tryAgain.onClick.RemoveListener(onClick);
                resolve();
            };
            this.tryAgain.onClick.AddListener(onClick);

        });
    }

    public GameOverScreen Hide()
    {
        this.background.GetComponent<Image>().enabled = false;
        this.tryAgain.gameObject.SetActive(false);
        this.title.SetActive(false);
        return this;
    }

    private Button tryAgain;
    private GameObject title;
    private GameObject background;
}
