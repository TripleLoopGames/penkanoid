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
                this.health = currentTransform.gameObject;
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

    public IPromise Show(int timeValue, int healthValue, int scoreValue)
    {
        ResetValues();
        this.background.GetComponent<Image>().enabled = true;
        this.continueButton.gameObject.SetActive(true);
        this.time.SetActive(true);
        this.health.SetActive(true);
        this.score.SetActive(true);

        this.continueButton.interactable = false;

        TweenEaseAnimationScaleComponent.CreateSequence("WinWorld", new GameObject[] { this.continueButton.gameObject, this.background }, () => this.continueButton.interactable = true);

        StartCoroutine(ScoreUpAnimation(this.time.GetComponent<Text>(), timeValue));
        StartCoroutine(ScoreUpAnimation(this.health.GetComponent<Text>(), healthValue));
        StartCoroutine(ScoreUpAnimation(this.score.GetComponent<Text>(), scoreValue));

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
        this.health.SetActive(false);
        this.score.SetActive(false);
        return this;
    }

    private WinWorldScreen ResetValues()
    {
        this.time.GetComponent<Text>().text = "0";
        this.health.GetComponent<Text>().text = "0";
        this.score.GetComponent<Text>().text = "0";

        return this;
    }

    private IEnumerator ScoreUpAnimation(Text textCounter, int finalValue)
    {
        int currentValue = 0;
        while (currentValue < finalValue)
        {
            currentValue++;
            textCounter.text = "" + currentValue;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private Button continueButton;
    private GameObject background;
    private GameObject time;
    private GameObject health;
    private GameObject score;

    private int timeValue;
    private int healthValue;
}
