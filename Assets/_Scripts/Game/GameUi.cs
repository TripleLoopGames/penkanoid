using System;
using UnityEngine;
using UnityEngine.UI;
using Resources = SRResources.Game.Ui;

public class GameUi : MonoBehaviourEx {

    public GameUi Initialize(Action restart)
    {
        InitializeEndGame(restart)
        .InitializeHealth();
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

    private GameUi InitializeHealth()
    {
        this.health = Resources.Health.Instantiate();
        this.health.name = "InitialHealth";
        this.health.transform.SetParent(this.gameObject.transform, false);
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

    private void OnMenu()
    {
        Messenger.Publish(new ChangeSceneMessage(SRScenes.Menu));
    }

    public GameUi SetCamera(Camera camera)
    {
        GetComponent<Canvas>().worldCamera = camera;
        return this;
    }

    private GameObject endGame;
    private GameObject health;
    private GameObject[] hearts;
}
