using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ChangeSceneComponent))]
public class MainMenu : MonoBehaviour
{
    private MainMenu Initialize()
    {
        InitializeCamera()
        .InitializeUI()
        .SetReferences();
        return this;
    }

    private MainMenu InitializeUI()
    {
        GameObject canvas = SRResources.Menu.Ui.Canvas.Instantiate();
        canvas.name = "Canvas";
        canvas.transform.SetParent(this.gameObject.transform, false);
        this.ui = canvas.GetComponent<MenuUi>();
        this.ui.Initialize();
        if (EventSystem.current == null)
        {
            GameObject eventSystem = SRResources.Menu.Ui.EventSystem.Instantiate();
            eventSystem.name = "EventSystemIn";
            eventSystem.transform.SetParent(this.transform, false);
        }
        return this;
    }

    private MainMenu InitializeCamera()
    {
        this.mainCamera = SRResources.Menu.Main_Camera.Instantiate().GetComponent<Camera>();
        this.mainCamera.name = "mainCamera";
        this.mainCamera.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private MainMenu SetReferences()
    {
        this.ui.SetCamera(this.mainCamera);
        return this;
    }

    private void Start()
    {
        Initialize();
    }

    private MenuUi ui;
    private Camera mainCamera;
}
