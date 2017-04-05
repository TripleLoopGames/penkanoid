using UnityEngine;
using UnityEngine.UI;
using WorldsConfig = Config.Worlds;
using Resources = SRResources.Menu.Ui;
using RSG;
using System.Collections.Generic;

public class LevelSelector : MonoBehaviour
{

    public LevelSelector Initialize()
    {
        InitializeReferences()
        .InitializeDoors()
        .SetDrawingOrder();
        return this;
    }

    public IPromise<string> WaitForNextLevel()
    {
        this.waitForNextLevel = new Promise<string>();
        this.levelDoors.Select(door => door.SetOnClickDoorPromise(this.waitForNextLevel));
        return this.waitForNextLevel;
    }

    private LevelSelector InitializeDoors()
    {
        int index = -1;
        this.levelDoors = this.waypoints.Select(waypoint =>
        {
            LevelDoor levelDoor = Resources.Door.Instantiate().GetComponent<LevelDoor>();
            levelDoor.Initialize(GetWorldName(index));
            levelDoor.SetPosition(waypoint.position);
            levelDoor.transform.SetParent(this.gameObject.transform, false);
            index++;
            return levelDoor;
        }).ToList();
        return this;
    }

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
                    TransitionLeft()
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
                    TransitionRight()
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
            return false;
        }).ToArray();
        int activatedAmount = activated.Where(element => element).Count();
        if (activatedAmount != 5)
        {
            Debug.LogError("Cound not find proper amount of elements");
        }
        return this;
    }

    private LevelSelector SetDrawingOrder()
    {
        this.goLeft.transform.SetAsLastSibling();
        this.goRight.transform.SetAsLastSibling();
        return this;
    }

    private IPromise TransitionRight()
    {
        Promise[] animations = this.levelDoors.map((door, index) =>
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
        return Promise.All(animations)
            .Then(() =>
            {
                // move last element to first position
                LevelDoor lastDoor = this.levelDoors.Last();
                this.levelDoors.Remove(lastDoor);
                this.levelDoors.Insert(0, lastDoor);
            });
    }

    private IPromise TransitionLeft()
    {
        Promise[] animations = this.levelDoors.map((door, index) =>
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
        return Promise.All(animations)
            .Then(() =>
            {
                // move first element to last position
                LevelDoor firstDoor = this.levelDoors.First();
                this.levelDoors.Remove(firstDoor);
                this.levelDoors.Add(firstDoor);
            });
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

    private string GetWorldName(int currentIndex)
    {
        int normalized = Mathf.Abs(currentIndex);
        return WorldsConfig.names[normalized % WorldsConfig.names.Length];
    }

    private Promise<string> waitForNextLevel;
    private readonly int[] goLeftOrder = new int[] { 2, 0, 1 };
    private readonly int[] goRightOrder = new int[] { 1, 2, 0 };
    private Button goLeft;
    private Button goRight;
    private Transform[] waypoints = new Transform[3];
    private List<LevelDoor> levelDoors;
}
