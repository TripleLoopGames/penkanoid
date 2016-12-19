using UnityEngine;
using UnityEngine.UI;
using Resources = SRResources.Menu.Ui;

public class MenuUi : MonoBehaviourEx
{
    public MenuUi Initialize()
    {
        InitializeTitle();
        return this;
    }

    public MenuUi SetCamera(Camera camera)
    {
        GetComponent<Canvas>().worldCamera = camera;
        return this;
    }

    private MenuUi InitializeTitle()
    {
        GameObject title = Resources.Title.Instantiate();
        title.name = "title";
        title.transform.SetParent(this.gameObject.transform, false);
        return this;
    }

    private void OnOptions()
    {
        Debug.Log("options Click");
    }

}
