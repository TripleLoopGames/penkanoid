using System;
using UnityEngine;
using UnityEngine.UI;
using Resources = SRResources.Game.Ui;

public class GameUi : MonoBehaviourEx {

    public GameUi Initialize()
    {
        InitializeEndGame();
        return this;
    }

    private GameUi InitializeEndGame()
    {
        this.endGame = Resources.EndGame.Instantiate();
        Button[] buttons = this.endGame.GetComponentsInChildren<Button>();
        buttons = buttons.Select(button => {
            if(button.name == "Restart")
            {
               button.onClick.AddListener(() => OnReStart());
            }
            if (button.name == "Menu")
            {
               button.onClick.AddListener(() => OnMenu());
            }
            return button;
        }).ToArray();
        this.endGame.name = "EndGame";
        this.endGame.transform.SetParent(this.gameObject.transform, false);
        HideEnd();
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


    private void OnReStart()
    {
        Debug.Log("should call a delegate or send a message to re-start the level");
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
}
