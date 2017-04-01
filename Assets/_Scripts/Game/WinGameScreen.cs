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

        TweenEaseAnimationScaleComponent.CreateSequence("Win", new GameObject[] {this.playAgain.gameObject, this.info, this.title}, () => this.playAgain.interactable = true);

        return new Promise((resolve, rejection) =>
        {
            UnityAction resolvePromise = null;
            resolvePromise = () => {               
                SoundData playRestart = new SoundData(GetInstanceID(), SRResources.Audio.Effects.Confirm);
                Messenger.Publish(new PlayEffectMessage(playRestart));
                this.playAgain.onClick.RemoveListener(resolvePromise);
                resolve();
            };
            this.playAgain.onClick.AddListener(resolvePromise);
        });
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
        text.text = string.Format("You've proven yourself a <b>worthy volcano.</b> \n And you did it in only <b>{0}</b> seconds... \n After <b>{1}</b> tries, of course.", time, tries);
        return this;
    }

    private Button playAgain;
    private GameObject info;
    private GameObject title;
    private GameObject background;
}
