using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using RSG;

public class LevelDoor : MonoBehaviour {

    public LevelDoor Initialize()
    {
        this.rectTransform = GetComponent<RectTransform>();
        this.changeLevel = GetComponentInChildren<Button>();
        this.changeLevel.onClick.AddListener(() => this.promise.Resolve(this.levelName));
        return this;
    }

    public LevelDoor SetType(string type)
    {
        Text uiText = GetComponentInChildren<Text>();
        this.gameObject.name = type;
        this.levelName = type;
        uiText.text = type;
        return this;
    }

    public LevelDoor SetOnClickDoorPromise(Promise<string> promise)
    {
        this.promise = promise;
        return this;
    }

    public Promise MoveTo(Vector2 position)
    {
        return new Promise((resolve, reject)=>
        {
            Vector2 currentPosition = this.rectTransform.position;
            this.rectTransform.DOMove(position, 1f, true)
            .OnComplete(()=> resolve());
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
