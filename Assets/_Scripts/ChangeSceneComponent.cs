using System;
using UnityEngine.SceneManagement;

public class ChangeSceneComponent : MonoBehaviourEx, IHandle<ChangeSceneMessage> {

    public ChangeSceneComponent setAction(Action<Action> beforeSceneChange)
    {
        this.beforeSceneChange = beforeSceneChange;
        return this;
    }

    public void Handle(ChangeSceneMessage message)
    {
        beforeSceneChange(() => SceneManager.LoadScene(message.SceneName));
    }

    private Action<Action> beforeSceneChange;
}
