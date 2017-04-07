using UnityEngine;
using UnityEngine.UI;
using WorldsConfig = Config.Worlds;
using Resources = SRResources.Menu.Ui;
using RSG;
using System.Collections.Generic;
using System;

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
                this.startingIndex--;
                this.startingIndex = this.makeInsideBoundaries(this.startingIndex, WorldsConfig.names.Length);
                AssingLevelDoorValues(this.startingIndex, this.levelDoors);
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
        worldNames.map((worldName, index) => levelDoors[index].SetType(worldName));
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

    private string GetWorldName(int currentIndex)
    {
        int normalized = Mathf.Abs(currentIndex);
        return WorldsConfig.names[normalized % WorldsConfig.names.Length];
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
    private int startingIndex = WorldsConfig.startingIndex;
    private Promise<string> waitForNextLevel;
    private readonly int[] goLeftOrder = new int[] { 2, 0, 1 };
    private readonly int[] goRightOrder = new int[] { 1, 2, 0 };
    private Button goLeft;
    private Button goRight;
    private Transform[] waypoints = new Transform[3];
    private List<LevelDoor> levelDoors;
}
