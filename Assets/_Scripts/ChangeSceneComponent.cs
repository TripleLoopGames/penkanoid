using System;
using UnityEngine.SceneManagement;

public class ChangeSceneComponent : MonoBehaviourEx, IHandle<ChangeSceneMessage> {

    public void Handle(ChangeSceneMessage message)
    {
        SceneManager.LoadScene(message.SceneName);
    }
}
