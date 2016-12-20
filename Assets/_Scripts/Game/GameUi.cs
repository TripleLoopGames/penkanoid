
using System;
using UnityEngine;
using UnityEngine.UI;
using Resources = SRResources.Game.Ui;

public class GameUi : MonoBehaviourEx, IHandle<PlayerChangeHealthMessage>, IHandle<ModifyTimeMessage>
{
    public GameUi Initialize(Action restart, Action nextLevel, int initialHealth)
    {
        this.initialHealth = initialHealth;
        InitializeHealth(initialHealth)
        .InitializeTimer()
        .InitializeEndGame(restart)
        .InitializeWinLevel(nextLevel)
        .InitializeWinGame(restart);
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

    public GameUi FullReset()
    {
        SetHearts(this.initialHealth);
        HideEnd();
        HideWinLevel();
        HideWinGame();
        return this;
    }

    public GameUi NextLevelReset()
    {
        HideEnd();
        HideWinLevel();
        HideWinGame();
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

    public GameUi ReStartCountDown()
    {
        this.timer.RestartTimer();
        return this;
    }

    public int GetTimeSpent()
    {
        return this.timer.GetTimeSpent();
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

    public GameUi ShowWinLevel()
    {
        this.winLevel.SetActive(true);
        return this;
    }

    public GameUi HideWinLevel()
    {
        this.winLevel.SetActive(false);
        return this;
    }

    public GameUi ShowWinGame()
    {
        this.winGame.SetActive(true);
        return this;
    }

    public GameUi HideWinGame()
    {
        this.winGame.SetActive(false);
        return this;
    }

    public GameUi SetWinGameInfo(int time = 0, int tries = 0)
    {
        Text info = this.winGame.GetComponentInChildren<Text>();
        info.text = $"You've proven yourself a worthy volcano. \n And you did it in only {time} seconds... \n After {tries} tries, of course.";
        return this;
    }


    public GameUi SetCamera(Camera camera)
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = camera;
        // set canvas layer, needs camera
        canvas.sortingLayerID = SRSortingLayers.UI;
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
            return button;
        }).ToArray();
        HideEnd();
        return this;
    }

    private GameUi InitializeWinLevel(Action nextLevel)
    {
        this.winLevel = Resources.WinLevel.Instantiate();
        this.winLevel.name = "WinLevel";
        this.winLevel.transform.SetParent(this.gameObject.transform, false);

        Button[] buttons = this.winLevel.GetComponentsInChildren<Button>();
        buttons = buttons.Select(button =>
        {
            if (button.name == "NextLevel")
            {
                button.onClick.AddListener(() => nextLevel());
            }
            return button;
        }).ToArray();
        HideWinLevel();
        return this;
    }

    private GameUi InitializeWinGame(Action restart)
    {
        this.winGame = Resources.WinGame.Instantiate();
        this.winGame.name = "WinGame";
        this.winGame.transform.SetParent(this.gameObject.transform, false);

        Button[] buttons = this.winGame.GetComponentsInChildren<Button>();
        buttons = buttons.Select(button =>
        {
            if (button.name == "PlayAgain")
            {
                button.onClick.AddListener(() => restart());
            }
            return button;
        }).ToArray();
        HideWinGame();
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
    private GameObject winLevel;
    private GameObject winGame;
    private GameObject health;
    private TimerComponent timer;
    private Text timerText;
    private GameObject[] hearts;
    private int initialHealth;
}
