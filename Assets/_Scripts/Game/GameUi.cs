using System;
using UnityEngine;
using UnityEngine.UI;
using Resources = SRResources.Game.Ui;

public class GameUi : MonoBehaviourEx, IHandle<PlayerChangeHealthMessage>, IHandle<ModifyTimeMessage>
{
    public GameUi Initialize(Action restart, Action nextLevel, int initialHealth, int startTime)
    {
        this.initialHealth = initialHealth;
        this.canvasGroup = GetComponent<CanvasGroup>();
        InitializeHealth(initialHealth)
        .InitializeTimer(startTime)
        .InitializeGameOverScreen(restart)
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

    public GameUi MakeInteractable()
    {
        this.canvasGroup.interactable = true;
        return this;
    }

    public GameUi MakeNonInteractable()
    {
        this.canvasGroup.interactable = false;
        return this;
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
            this.timeText.text = value.ToString();
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
        this.gameOverScreen.Show();
        return this;
    }

    public GameUi HideEnd()
    {
        this.gameOverScreen.Hide();
        return this;
    }

    public GameUi ShowWinLevel()
    {
        this.winLevel.Show();
        return this;
    }

    public GameUi HideWinLevel()
    {
        this.winLevel.Hide();
        return this;
    }

    public GameUi ShowWinGame()
    {
        this.winGameScreen.Show();
        return this;
    }

    public GameUi HideWinGame()
    {
        this.winGameScreen.Hide();
        return this;
    }

    public GameUi SetWinGameInfo(int time, int tries)
    {
        this.winGameScreen.SetInfo(time, tries);
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

    private GameUi InitializeGameOverScreen(Action restart)
    {
        this.gameOverScreen = Resources.GameOverScreen.Instantiate().GetComponent<GameOverScreen>();
        Utils.SetNameAndParent("GameOverScreen", this.gameOverScreen.gameObject, this.gameObject);
        this.gameOverScreen.Initialize(restart);
        this.gameOverScreen.Hide();
        return this;
    }

    private GameUi InitializeWinLevel(Action nextLevel)
    {
        this.winLevel = Resources.WinLevel.Instantiate().GetComponent<WinLevel>();
        Utils.SetNameAndParent("WinLevel", this.winLevel.gameObject, this.gameObject);
        this.winLevel.Initialize();
        this.winLevel.Hide();

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
        this.winGameScreen = Resources.WinGameScreen.Instantiate().GetComponent<WinGameScreen>();
        this.winGameScreen.name = "WinGame";
        this.winGameScreen.transform.SetParent(this.gameObject.transform, false);
        this.winGameScreen.Initialize(restart);
        this.winGameScreen.Hide();
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

    private GameUi InitializeTimer(int startTime)
    {
        GameObject time = Resources.Time.Instantiate();
        this.timer = time.GetComponent<TimerComponent>();
        this.timer.name = "Time";
        this.timer.transform.SetParent(this.gameObject.transform, false);
        this.timeText = time.GetComponentInChildren<Text>();
        this.timeText.text = startTime.ToString();
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

    private CanvasGroup canvasGroup;
    private GameOverScreen gameOverScreen;
    private WinLevel winLevel;
    private WinGameScreen winGameScreen;
    private GameObject health;
    private TimerComponent timer;
    private Text timeText;
    private GameObject[] hearts;
    private int initialHealth;
}
