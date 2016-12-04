using UnityEngine;

public class ChangeSceneMessage
{
    public TypeSafe.Scene SceneName { get; set; }

    public ChangeSceneMessage(TypeSafe.Scene sceneName)
    {
        this.SceneName = sceneName;
    }
}