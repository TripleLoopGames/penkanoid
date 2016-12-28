using System;
using UnityEngine;
using UnityEngine.UI;

public class WinGameScreen : MonoBehaviourEx
{

    public WinGameScreen Initialize(Action restart)
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        bool[] activated = transforms.Select(currentTransform =>
        {
            if (currentTransform.name == "PlayAgain")
            {
                this.playAgain = currentTransform.gameObject.GetComponent<Button>();
                this.playAgain.onClick.AddListener(() =>
                {
                    SoundData playRestart = new SoundData(GetInstanceID(), SRResources.Audio.Effects.Confirm);
                    Messenger.Publish(new PlayEffectMessage(playRestart));
                    restart();
                });
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
            Debug.LogWarning("Cound not find proper amount of elements");
        }
        return this;
    }

    public WinGameScreen Show()
    {
        this.playAgain.gameObject.SetActive(true);
        this.info.gameObject.SetActive(true);
        this.title.gameObject.SetActive(true);
        this.background.GetComponent<Image>().enabled = true;
        return this;
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
        text.text = $"You've proven yourself a worthy volcano. \n And you did it in only {time} seconds... \n After {tries} tries, of course.";
        return this;
    }

    private Button playAgain;
    private GameObject info;
    private GameObject title;
    private GameObject background;

}
