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
        bool[] activated = GetComponentsInChildren<RectTransform>().map((childTransform) =>
        {

            if (childTransform.name == "Spiral")
            {
                childTransform.DORotate(new Vector3(0, 0, -360), 2f)
                              .SetEase(Ease.Linear)
                              .SetRelative()
                              .SetLoops(-1);
                return true;
            }
            if (childTransform.name == "volka")
            {
                this.volka = childTransform;
                AnimateEnterVolka();
                return true;
            }
            return false;
        }).ToArray();
        int activatedAmount = activated.Where(element => element).Count();
        if (activatedAmount != 2)
        {
            Debug.LogError("Cound not find proper amount of elements");
        }
        return this;
    }

    public LevelDoor ResetVolkaRotation()
    {
        this.volka.localEulerAngles = defaultRotation;
        return this;
    }

    public Promise AnimateEnterVolka()
    {
        return new Promise((resolve, reject) =>
        {
            this.volka.DORotate(new Vector3(0, 0, -80.7f), 1.5f)
                           .SetEase(Ease.OutBack)
                           .SetRelative()
                           .OnComplete(() => resolve());
        });
    }

    public LevelDoor SetWorldName(string type)
    {
        this.gameObject.name = type;
        this.levelName = type;
        return this;
    }

    public LevelDoor SetWorldDoorSprite(string type)
    {
        Image uiDoor = transform.Find("DoorFrame").GetComponentInChildren<Image>();
        if (type == "basic")
        {
            uiDoor.sprite = DoorSprites[0];
        }
        if (type == "rock")
        {
            uiDoor.sprite = DoorSprites[1];
        }
        if (type == "jungle")
        {
            uiDoor.sprite = DoorSprites[2];
        }
        if (type == "ice")
        {
            uiDoor.sprite = DoorSprites[3];
        }
        if (type == "lava")
        {
            uiDoor.sprite = DoorSprites[4];
        }
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

    private RectTransform rectTransform;
    private readonly Vector3 defaultRotation = new Vector3(0, 0, 18.7f);
    private Button changeLevel;
    string levelName;
    private Promise<string> promise;
    private RectTransform volka;
    [SerializeField]
    private Sprite[] DoorSprites;
}
