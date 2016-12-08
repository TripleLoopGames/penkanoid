using System;
using UnityEngine;
using UnityEngine.UI;
using Resources = SRResources.Game.Ui;

public class GameUi : MonoBehaviourEx, IHandle<PlayerChangeHealthMessage> {



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

    public GameUi Initialize(Action restart,int initialHealth)
    {
        this.initialHealth = initialHealth;
        InitializeEndGame(restart)
        .InitializeHealth(initialHealth);
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
        buttons = buttons.Select(button => {
            if(button.name == "Restart")
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

    private GameUi InitializeHealth(int initialHealth)
    {
        this.health = Resources.Health.Instantiate();
        this.health.name = "InitialHealth";
        this.health.transform.SetParent(this.gameObject.transform, false);
        this.hearts = this.health.GetComponentsInChildren<Transform>()
                            .Select((Transform heartTransform) => heartTransform.gameObject)
                            .Where(heart => heart.CompareTag(SRTags.UiHeart))
                            .ToArray();
        SetHearts(initialHealth);
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



    private GameObject endGame;
    private GameObject health;
    private GameObject[] hearts;
    private int initialHealth;
}
