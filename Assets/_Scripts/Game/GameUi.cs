using System;
using UnityEngine;
using UnityEngine.UI;
using Resources = SRResources.Game.Ui;

public class GameUi : MonoBehaviourEx, IHandle<PlayerChangeHealthMessage>, IHandle<ModifyTimeMessage>
{
    public GameUi Initialize(Action restart, Action nextLevel, int initialHealth)
    {
        this.initialHealth = initialHealth;
        InitializeEndGame(restart)
        .InitializeWinGame(restart, nextLevel)
        .InitializeHealth(initialHealth)
        .InitializeTimer();
        return this;
    }

    public void Handle(ModifyTimeMessage message)
    {
        ModifyTime(message.Time);
    }

    public void Handle(PlayerChangeHealthMessage message)
    {
        SetHearts(message.Health);
    }

    public GameUi Reset()
    {
        HideEnd();
        SetHearts(this.initialHealth);
        return this;
    }

    public GameUi StartCountDown(int time, Action onEnd)
    {
        Action<int> onTimerTick = value =>
        {         
            this.timerText.text = value.ToString();
        };
        this.timer.StartTimer(time, onTimerTick, onEnd);
        return this;
    }

    public GameUi StopCountDown()
    {
        this.timer.StopTimer();
        return this;
    }

    public GameUi ShowEnd()
    {
        this.endGame.SetActive(true);
        return this;
    }

    public GameUi HideEnd()
    {
        this.endGame.SetActive(false);
        return this;
    }

    public GameUi SetCamera(Camera camera)
    {
        GetComponent<Canvas>().worldCamera = camera;
        return this;
    }

    private GameUi InitializeEndGame(Action restart)
    {
        this.endGame = Resources.EndGame.Instantiate();
        this.endGame.name = "EndGame";
        this.endGame.transform.SetParent(this.gameObject.transform, false);

        Button[] buttons = this.endGame.GetComponentsInChildren<Button>();
        buttons = buttons.Select(button =>
        {
            if (button.name == "Restart")
            {
                button.onClick.AddListener(() => restart());
            }
            if (button.name == "Menu")
            {
                button.onClick.AddListener(() => OnMenu());
            }
            return button;
        }).ToArray();
        HideEnd();
        return this;
    }

    private GameUi InitializeWinGame(Action restart, Action nextLevel)
    {
        this.endGame = Resources.WinGame.Instantiate();
        this.endGame.name = "WinGame";
        this.endGame.transform.SetParent(this.gameObject.transform, false);

        Button[] buttons = this.endGame.GetComponentsInChildren<Button>();
        buttons = buttons.Select(button =>
        {
            if (button.name == "NextLevel")
            {
                button.onClick.AddListener(() => nextLevel());
            }
            if (button.name == "Menu")
            {
                button.onClick.AddListener(() => OnMenu());
            }
            return button;
        }).ToArray();
        HideEnd();
        return this;
    }

    private GameUi InitializeHealth(int initialHealth)
    {
        this.health = Resources.Health.Instantiate();
        this.health.name = "Health";
        this.health.transform.SetParent(this.gameObject.transform, false);
        this.hearts = this.health.GetComponentsInChildren<Transform>()
                            .Select((Transform heartTransform) => heartTransform.gameObject)
                            .Where(heart => heart.CompareTag(SRTags.UiHeart))
                            .ToArray();
        SetHearts(initialHealth);
        return this;
    }

    private GameUi InitializeTimer()
    {
        GameObject time = Resources.Time.Instantiate();
        this.timer = time.GetComponent<TimerComponent>();
        this.timer.name = "Time";
        this.timer.transform.SetParent(this.gameObject.transform, false);
        this.timerText = time.GetComponentInChildren<Text>();
        return this;
    }

    private void OnMenu()
    {
        Messenger.Publish(new ChangeSceneMessage(SRScenes.Menu));
    }

    private GameUi SetHearts(int health)
    {
        for (int index = 0; index < this.hearts.Length; index++)
        {
            this.hearts[index].SetActive(index < health);
        }
        return this;
    }

    private GameUi ModifyTime(int time)
    {
        int timeLeft = this.timer.GetTimeLeft();
        this.timer.SetTimeLeft(timeLeft + time);
        return this;
    }

    private GameObject endGame;
    private GameObject health;
    private TimerComponent timer;
    private Text timerText;
    private GameObject[] hearts;
    private int initialHealth;
}
