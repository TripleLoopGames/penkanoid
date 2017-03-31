using System.Collections;
using System.Collections.Generic;
using Resources = SRResources.Menu.Ui;
using UnityEngine;

public class MenuUi : MonoBehaviour
{

    public MenuUi Initialize()
    {
        InitializeBackground()
        .InitializeLevelSelector();
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
        LevelSelector levelSelector = Resources.LevelSelector.Instantiate().GetComponent<LevelSelector>();
        levelSelector.Initialize();
        levelSelector.name = "LevelSelector";
        levelSelector.transform.SetParent(this.gameObject.transform, false);
        return this;
    }
}
