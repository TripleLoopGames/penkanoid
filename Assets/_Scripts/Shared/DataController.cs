using System;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public DataController Initialize()
    {
        this.LoadPlayerProgress();
        this.LoadLoginStatus();
        this.LoadWorldStatus();
        return this;
    }

    public LoginStatus GetLoginStatus()
    {
        return this.loginStatus.Copy();
    }

    public string GetCurrentWorldName()
    {
        return this.worldStatus.currentWorldName;
    }

    public DataController SetCurrentWorldName(string worldName)
    {
        this.worldStatus.currentWorldName = worldName;
        this.SaveWorldStatus();
        return this;
    }

    public DataController SetLoginStatus(LoginStatus loginStatus)
    {
        this.loginStatus.Merge(loginStatus);
        SaveLoginStatus(this.loginStatus);
        return this;
    }
    
    public WorldSave[] GetWorldSaves()
    {
        return this.playerProgress.worldSaves.map((worldSave)=>
        {
            return new WorldSave
            {
                name = worldSave.name,
                highScore = worldSave.highScore,
                unlocked = worldSave.unlocked,
                tries = worldSave.tries
            };
        }).ToArray();
    }

    public DataController SetWorldGameTries(string worldName, int tries)
    {
        this.playerProgress.worldSaves = this.playerProgress.worldSaves.map(worldSave =>
        {
            if (worldSave.name == worldName)
            {
                return new WorldSave
                {
                    name = worldSave.name,
                    highScore = worldSave.highScore,
                    unlocked = worldSave.unlocked,
                    tries = tries
                };
            }
            return worldSave;
        }).ToArray();
        SavePlayerProgress(this.playerProgress);
        return this;
    }

    public int GetWorldGameTries(string worldName)
    {

        return FindWorldSave(this.playerProgress.worldSaves, worldName).tries;
    }

    public DataController SetHighScore(string worldName, int highScore)
    {
        this.playerProgress.worldSaves = this.playerProgress.worldSaves.map(worldSave =>
        {
            if (worldSave.name == worldName)
            {               
                return new WorldSave
                {
                    name = worldSave.name,
                    highScore = highScore,
                    unlocked = worldSave.unlocked,
                    tries = worldSave.tries
                };
            }
            return worldSave;
        }).ToArray();
        SavePlayerProgress(this.playerProgress);
        return this;
    }

    public int GetHighScore(string worldName)
    {
        return FindWorldSave(this.playerProgress.worldSaves, worldName).highScore;
    }
    
    #region SaveAndLoadProperties
    private DataController LoadPlayerProgress()
    {
        string progressPath = Application.persistentDataPath + "/playerProgress.json";
        if (File.Exists(progressPath))
        {
            string dataAsJson = File.ReadAllText(progressPath);
            this.playerProgress = JsonUtility.FromJson<PlayerProgress>(dataAsJson);
            return this;
        }
        // generate default player progress
        WorldSave[] worldSaves = Config.Worlds.names.map((worldName, index) =>
        {
            bool isFirst = index == 0;
            return new WorldSave
            {
                name = worldName,
                highScore = 0,
                tries = 0,
                unlocked = isFirst
            };
        }).ToArray();
        this.playerProgress = new PlayerProgress
        {
            worldSaves = worldSaves
        };
        return this;
    }

    private DataController SavePlayerProgress(PlayerProgress playerProgress)
    {
        string dataAsJson = JsonUtility.ToJson(playerProgress);
        string progressPath = Application.persistentDataPath + "/playerProgress.json";
        File.WriteAllText(progressPath, dataAsJson);
        return this;
    }

    private DataController LoadLoginStatus()
    {
        this.loginStatus = new LoginStatus();
        this.loginStatus.ServicesActivated = servicesActivated;
        if (PlayerPrefs.HasKey("loggedIn"))
        {
            this.loginStatus.LoggedIn = PlayerPrefs.GetInt("loggedIn") != 0;
        }
        if (PlayerPrefs.HasKey("refusedLogIn"))
        {
            this.loginStatus.RefusedLogIn = PlayerPrefs.GetInt("loggedIn") != 0;
        }
        return this;
    }

    private DataController SaveLoginStatus(LoginStatus loginStatus)
    {
        Func<bool, int> boolToInt = boolean => boolean ? 1 : 0;

        servicesActivated = loginStatus.ServicesActivated;
        PlayerPrefs.SetInt("loggedIn", boolToInt(loginStatus.LoggedIn));
        PlayerPrefs.SetInt("refusedLogIn", boolToInt(loginStatus.RefusedLogIn));
        return this;
    }
    
    private DataController LoadWorldStatus()
    {
        this.worldStatus = new WorldStatus();
        if (PlayerPrefs.HasKey("currentWorldName"))
        {
            this.worldStatus.currentWorldName = PlayerPrefs.GetString("currentWorldName");
        }
        return this;
    }

    private DataController SaveWorldStatus()
    {
        PlayerPrefs.SetString("currentWorldName", this.worldStatus.currentWorldName);
        return this;
    }
    #endregion SaveAndLoadProperties

    private WorldSave FindWorldSave(WorldSave[] worldSaves, string worldName)
    {
        WorldSave foundworldSave = Array.Find(this.playerProgress.worldSaves,
                                             (worldSave) => worldSave.name == worldName);
        if (foundworldSave == null)
        {
            Debug.LogError("Can't find world");
            return null;
        }
        return foundworldSave;
    }


    private PlayerProgress playerProgress;
    private LoginStatus loginStatus;
    private WorldStatus worldStatus;
    private static bool servicesActivated;
}
