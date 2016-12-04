using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Resources = SRResources.Menu.Ui;

public class MenuUi : MonoBehaviourEx
{
    public MenuUi Initialize()
    {
        InitializeBackground()
            .InitializeMainPanel();
        return this;
    }

    public MenuUi SetCamera(Camera camera)
    {
        GetComponent<Canvas>().worldCamera = camera;
        return this;
    }

    private MenuUi InitializeMainPanel()
    {
        GameObject mainPanel = Resources.MainPanel.Instantiate();
        Button[] buttons = mainPanel.GetComponentsInChildren<Button>();
        Button start = buttons.Where(button => button.name == "Start").First();
        start.onClick.AddListener(() => OnStart());
        mainPanel.name = "mainPanel";
        mainPanel.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private MenuUi InitializeBackground()
    {
        GameObject background = Resources.Background.Instantiate();
        background.transform.SetParent(this.gameObject.transform, false);
        background.name = "background";
        return this;
    }

    private void OnStart()
    {
        Messenger.Publish(new ChangeSceneMessage(SRScenes.Game));
    }

    private void OnOptions()
    {
        Debug.Log("options Click");
    }
}
