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
        .InitializeLevelSelector();
        return this;
    }

    public IPromise<string> WaitForNextLevel(){
        return this.levelSelector.WaitForNextLevel();
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

    LevelSelector levelSelector;
}
