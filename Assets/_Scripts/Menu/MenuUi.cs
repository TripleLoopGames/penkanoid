using System.Collections;
using System.Collections.Generic;
using Resources = SRResources.Menu.Ui;
using UnityEngine;
using RSG;

public class MenuUi : MonoBehaviour
{

    public MenuUi Initialize()
    {
        InitializeBackground()
        .InitializeCanvasGroup()
        .InitializeLevelSelector();
        MakeNonInteractable();
        return this;
    }  

    public IPromise<string> WaitForNextLevel(){
        return this.levelSelector.WaitForNextLevel();
    }
  
    public MenuUi MakeNonInteractable()
    {
        this.canvasGroup.interactable = false;
        return this;
    }

    public MenuUi MakeInteractable()
    {
        this.canvasGroup.interactable = true;
        return this;
    }

    private MenuUi InitializeBackground()
    {
        GameObject background = Resources.Background.Instantiate();
        background.name = "Background";
        background.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private MenuUi InitializeLevelSelector()
    {
        this.levelSelector = Resources.LevelSelector.Instantiate().GetComponent<LevelSelector>();
        this.levelSelector.Initialize();
        this.levelSelector.name = "LevelSelector";
        this.levelSelector.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    public MenuUi InitializeCanvasGroup()
    {
        this.canvasGroup = GetComponent<CanvasGroup>();
        return this;
    }

    LevelSelector levelSelector;
    private CanvasGroup canvasGroup;
}
