using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using RSG;
using System;

public class LevelDoor : MonoBehaviour
{

    public LevelDoor Initialize()
    {
        this.rectTransform = GetComponent<RectTransform>();
        this.changeLevel = GetComponentInChildren<Button>();
        this.changeLevel.onClick.AddListener(() => this.promise.Resolve(this.levelName));
        RectTransform spiral = Array.Find(GetComponentsInChildren<RectTransform>(),
                        childTransform => childTransform.name == "Spiral");
        spiral.DORotate(new Vector3(0, 0, -360), 2f)
            .SetEase(Ease.Linear)
            .SetRelative()
            .SetLoops(-1);
        return this;
    }

    public LevelDoor SetWorldName(string type)
    {
        Text uiText = GetComponentInChildren<Text>();
        this.gameObject.name = type;
        this.levelName = type;
        uiText.text = type;
        return this;
    }

    public string GetWorldName()
    {
        return this.levelName;
    }

    public LevelDoor SetOnClickDoorPromise(Promise<string> promise)
    {
        this.promise = promise;
        return this;
    }

    public Promise MoveTo(Vector2 position, float time = 1f)
    {
        return new Promise((resolve, reject) =>
        {
            Vector2 currentPosition = this.rectTransform.position;
            this.rectTransform.DOMove(position, time, false)
            .OnComplete(() => resolve());
        });
    }

    public LevelDoor SetPosition(Vector2 position)
    {
        this.rectTransform.position = position;
        return this;
    }

    RectTransform rectTransform;
    Button changeLevel;
    string levelName;
    private Promise<string> promise;
}
