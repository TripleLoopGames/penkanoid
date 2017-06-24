using RSG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinWorldScreen : MonoBehaviourEx
{

    public WinWorldScreen Initialize()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        bool[] activated = transforms.Select(currentTransform =>
        {
            if (currentTransform.name == "Continue")
            {
                this.continueButton = currentTransform.gameObject.GetComponent<Button>();
                return true;
            }
            if (currentTransform.name == "Background")
            {
                this.background = currentTransform.gameObject;
                return true;
            }
            if (currentTransform.name == "Time")
            {
                this.time = currentTransform.gameObject;
                return true;
            }
            if (currentTransform.name == "Life")
            {
                this.life = currentTransform.gameObject;
                return true;
            }
            if (currentTransform.name == "Score")
            {
                this.score = currentTransform.gameObject;
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

    public IPromise Show()
    {
        this.background.GetComponent<Image>().enabled = true;
        this.continueButton.gameObject.SetActive(true);
        this.time.SetActive(true);
        this.life.SetActive(true);
        this.score.SetActive(true);

        this.continueButton.interactable = false;

        TweenEaseAnimationScaleComponent.CreateSequence("WinWorld", new GameObject[] { this.continueButton.gameObject, this.background }, () => this.continueButton.interactable = true);

        return new Promise((resolve, reject) =>
        {
            UnityAction onClick = null;
            onClick = () =>
            {
                SoundData playRestart = new SoundData(GetInstanceID(), SRResources.Audio.Effects.Confirm);
                Messenger.Publish(new PlayEffectMessage(playRestart));
                this.continueButton.onClick.RemoveListener(onClick);
                resolve();
            };
            this.continueButton.onClick.AddListener(onClick);
        });
    }

    public WinWorldScreen Hide()
    {
        this.background.GetComponent<Image>().enabled = false;
        this.continueButton.gameObject.SetActive(false);
        this.time.SetActive(false);
        this.life.SetActive(false);
        this.score.SetActive(false);
        return this;
    }

    private Button continueButton;
    private GameObject background;
    private GameObject time;
    private GameObject life;
    private GameObject score;
}
