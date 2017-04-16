using UnityEngine;
using UnityEngine.UI;
using WorldsConfig = Config.Worlds;
using Resources = SRResources.Menu.Ui;
using RSG;
using System.Collections.Generic;
using System;
using DG.Tweening;

public class LevelSelector : MonoBehaviour
{

    public LevelSelector Initialize(WorldSave[] worldSaves)
    {
        InitializeReferences()
        .InitializeDoors(worldSaves)
        .SetDrawingOrder();
        return this;
    }

    public IPromise<string> WaitForNextLevel()
    {
        this.waitForNextLevel = new Promise<string>();
        this.levelDoors.Select(door => door.SetOnClickDoorPromise(this.waitForNextLevel));
        return this.waitForNextLevel;
    }

    public IPromise EnterAnimation()
    {
        return Promise.All
        (
             new Promise((resolve, reject) =>
             {
                 RectTransform rectTransform = this.goLeft.GetComponent<RectTransform>();
                 Sequence mySequence = DOTween.Sequence();
                 rectTransform.DOMoveX(-300, 1f, false)
                 .From()
                 .OnComplete(() => resolve());
             }),
             new Promise((resolve, reject) =>
             {
                 RectTransform rectTransform = this.goRight.GetComponent<RectTransform>();
                 Sequence mySequence = DOTween.Sequence();
                 rectTransform.DOMoveX(1200, 1f, false)
                 .From()
                 .OnComplete(() => resolve());
             }),
             new Promise((resolve, reject) =>
             {
                 this.worldTitle.DOMoveY(600, 1f, false)
                 .From()
                 .OnComplete(() => resolve());
             }),
             new Promise((resolve, reject) =>
             {
                 this.highScore.DOMoveY(550, 1f, false)
                 .From()
                 .OnComplete(() => resolve());
             }),
             new Promise((resolve, reject) =>
             {
                 RectTransform rectTransform =  this.levelDoors[1].GetComponent<RectTransform>();
                 rectTransform.DOMoveY(-550, 1f, false)
                 .From()
                 .OnComplete(() => resolve());
             }));
    }

    private LevelSelector SetDrawingOrder()
    {
        this.goLeft.transform.SetAsLastSibling();
        this.goRight.transform.SetAsLastSibling();
        return this;
    }

    private IPromise TransitionRight()
    {
        // animate the doors
        Promise[] doorAnimations = this.levelDoors.map((door, index) =>
        {
            int indexWaypoint = this.goRightOrder[index];
            Vector2 position = this.waypoints[indexWaypoint].position;
            if (index == 2)
            {
                var promise = new Promise();
                door.SetPosition(position);
                promise.Resolve();
                return promise;
            }
            return door.MoveTo(position);
        }).ToArray();
        //animation world up change when not visible and then go down
        Promise[] extraAnimations = new Promise[]
        {
            new Promise((resolve, reject)=>
            {
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(this.highScore.DOMoveY(250, 0.5f, false).SetRelative());
                mySequence.AppendCallback(() => {
                   string worldName = this.levelDoors.First().GetWorldName();
                   WorldSave foundWorldSave = Array.Find(this.worldSaves, (worldSave) => worldSave.name == worldName);
                   this.highScoreText.text = foundWorldSave.highScore.ToString();
                });
                mySequence.Append(this.highScore.DOMoveY(-250, 0.5f, false).SetRelative());
                mySequence.AppendCallback(() => resolve());
            }),
            new Promise((resolve, reject)=>
            {
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(this.worldTitle.DOMoveY(250, 0.5f, false).SetRelative());
                mySequence.Append(this.worldTitle.DOMoveY(-250, 0.5f, false).SetRelative());
                mySequence.AppendCallback(() => resolve());
            }),
            new Promise((resolve, reject)=>
            {
                this.levelDoors.Last()
                .ResetVolkaRotation()
                .AnimateEnterVolka()
                .Then(()=> resolve());
            }),
        };
        Promise[] animations = doorAnimations.Union(extraAnimations).ToArray();
        // set highScore world     
        return Promise.All(animations)
            .Then(() =>
            {
                // move last element to first position
                LevelDoor lastDoor = this.levelDoors.First();
                this.levelDoors.Remove(lastDoor);
                this.levelDoors.Insert(0, lastDoor);
                this.startingIndex--;
                this.startingIndex = this.makeInsideBoundaries(this.startingIndex, WorldsConfig.names.Length);
                AssingLevelDoorValues(this.startingIndex, this.levelDoors);
            });
    }

    private IPromise TransitionLeft()
    {
        Promise[] doorAnimations = this.levelDoors.map((door, index) =>
        {
            int indexWaypoint = this.goLeftOrder[index];
            Vector2 position = this.waypoints[indexWaypoint].position;
            if (index == 0)
            {
                var promise = new Promise();
                door.SetPosition(position);
                promise.Resolve();
                return promise;
            }
            return door.MoveTo(position);
        }).ToArray();
        //animation world up change when not visible and then go down
        Promise[] worldInfoAnimations = new Promise[]
        {
            new Promise((resolve, reject)=>
            {
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(this.highScore.DOMoveY(250, 0.5f, false).SetRelative());
                mySequence.AppendCallback(() => {
                   string worldName = this.levelDoors.First().GetWorldName();
                   WorldSave foundWorldSave = Array.Find(this.worldSaves, (worldSave) => worldSave.name == worldName);
                   this.highScoreText.text = foundWorldSave.highScore.ToString();
                });
                mySequence.Append(this.highScore.DOMoveY(-250, 0.5f, false).SetRelative());
                mySequence.AppendCallback(() => resolve());
            }),
            new Promise((resolve, reject)=>
            {
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(this.worldTitle.DOMoveY(250, 0.5f, false).SetRelative());
                mySequence.Append(this.worldTitle.DOMoveY(-250, 0.5f, false).SetRelative());
                mySequence.AppendCallback(() => resolve());
            }),
            new Promise((resolve, reject)=>
            {
                this.levelDoors.Last()
                .ResetVolkaRotation()
                .AnimateEnterVolka()
                .Then(()=> resolve());
            }),
        };
        Promise[] animations = doorAnimations.Union(worldInfoAnimations).ToArray();
        return Promise.All(animations)
            .Then(() =>
            {
                // move first element to last position
                LevelDoor firstDoor = this.levelDoors.First();
                this.levelDoors.Remove(firstDoor);
                this.levelDoors.Add(firstDoor);
                this.startingIndex++;
                this.startingIndex = this.makeInsideBoundaries(this.startingIndex, WorldsConfig.names.Length);
                AssingLevelDoorValues(this.startingIndex, this.levelDoors);
            });
    }

    private LevelSelector AssingLevelDoorValues(int indexLevelDoor, List<LevelDoor> levelDoors)
    {
        Func<int, string[], string[]> getSelectedAndAdjecent = (index, array) =>
        {
            int upper = index + 1;
            int lower = index - 1;
            if (upper >= array.Length)
            {
                upper = 0;
            }
            if (lower < 0)
            {
                lower = array.Length - 1;
            }
            return new string[] { array[lower], array[index], array[upper] };
        };
        string[] worldNames = getSelectedAndAdjecent(indexLevelDoor, WorldsConfig.names);
        worldNames.map((worldName, index) => levelDoors[index].SetWorldName(worldName));
        return this;
    }

    private LevelSelector DisableButtons()
    {
        this.goLeft.interactable = false;
        this.goRight.interactable = false;
        return this;
    }

    private LevelSelector EnableButtons()
    {
        this.goLeft.interactable = true;
        this.goRight.interactable = true;
        return this;
    }

    Func<int, int, int> makeInsideBoundaries = (number, limit) =>
    {
        if (number >= limit)
        {
            return 0;
        }
        if (number < 0)
        {
            return limit - 1;
        }
        return number;
    };

    private LevelSelector InitializeReferences()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        bool[] activated = transforms.Select(currentTransform =>
        {
            if (currentTransform.name == "GoLeft")
            {
                this.goLeft = currentTransform.gameObject.GetComponent<Button>();
                this.goLeft.onClick.AddListener(() =>
                {
                    DisableButtons();
                    TransitionRight()
                    .Then(() => EnableButtons());
                });
                return true;
            }
            if (currentTransform.name == "GoRight")
            {
                this.goRight = currentTransform.gameObject.GetComponent<Button>();
                this.goRight.onClick.AddListener(() =>
                {
                    DisableButtons();
                    TransitionLeft()
                    .Then(() => EnableButtons());
                });
                return true;
            }

            if (currentTransform.name == "WaypointRight")
            {
                this.waypoints[2] = currentTransform;
                return true;
            }
            if (currentTransform.name == "WaypointCenter")
            {
                this.waypoints[1] = currentTransform;
                return true;
            }
            if (currentTransform.name == "WaypointLeft")
            {
                this.waypoints[0] = currentTransform;
                return true;
            }
            if (currentTransform.name == "Score")
            {
                this.highScoreText = currentTransform.GetComponentInChildren<Text>();
                this.highScore = currentTransform.GetComponent<RectTransform>();
                return true;
            }
            if (currentTransform.name == "WorldTitle")
            {
                this.worldTitle = currentTransform.GetComponent<RectTransform>();
                return true;
            }
            return false;
        }).ToArray();
        int activatedAmount = activated.Where(element => element).Count();
        if (activatedAmount != 7)
        {
            Debug.LogError("Cound not find proper amount of elements");
        }
        return this;
    }

    private LevelSelector InitializeDoors(WorldSave[] worldSaves)
    {
        this.worldSaves = worldSaves;
        this.levelDoors = this.waypoints.map((waypoint, index) =>
        {
            LevelDoor levelDoor = Resources.Door.Instantiate().GetComponent<LevelDoor>();
            levelDoor.Initialize();
            levelDoor.SetPosition(waypoint.position);
            levelDoor.transform.SetParent(this.gameObject.transform, false);
            return levelDoor;
        }).ToList();
        AssingLevelDoorValues(this.startingIndex, this.levelDoors);
        return this;
    }

    private int startingIndex = WorldsConfig.startingIndex;
    private Promise<string> waitForNextLevel;
    private readonly int[] goLeftOrder = new int[] { 2, 0, 1 };
    private readonly int[] goRightOrder = new int[] { 1, 2, 0 };
    private Button goLeft;
    private Button goRight;
    private Transform[] waypoints = new Transform[3];
    private List<LevelDoor> levelDoors;
    private Text highScoreText;
    private RectTransform highScore;
    private RectTransform worldTitle;
    private WorldSave[] worldSaves;
}
