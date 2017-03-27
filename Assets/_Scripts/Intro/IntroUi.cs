using UnityEngine;
using UnityEngine.UI;
using Resources = SRResources.Intro.Ui;

public class IntroUi : MonoBehaviourEx
{
    public IntroUi Initialize()
    {
        InitializeTitle();
        return this;
    }

    public IntroUi SetCamera(Camera camera)
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = camera;
        // set canvas layer, needs camera
        canvas.sortingLayerID = SRSortingLayers.UI;
        return this;
    }

    private IntroUi InitializeTitle()
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
