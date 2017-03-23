using System;
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

    public int GetPlayerGameTries()
    {
        return this.playerProgress.gameTries;
    }

    public LoginStatus GetLoginStatus()
    {
        return this.loginStatus.Copy();
    }

    public string GetCurrentWorldName()
    {
        return this.worldStatus.currentWorldName;
    }

    public DataController SetLoginStatus(LoginStatus loginStatus)
    {
        this.loginStatus.Merge(loginStatus);
        SaveLoginStatus(this.loginStatus);
        return this;
    }

    public DataController AddPlayerGameTries(int tries)
    {
        this.playerProgress.gameTries += tries;
        SavePlayerProgress(this.playerProgress);
        return this;
    }

    public DataController ResetPlayerGameTries()
    {
        this.playerProgress.gameTries = 0;
        SavePlayerProgress(this.playerProgress);
        return this;
    }

    private DataController LoadPlayerProgress()
    {
        this.playerProgress = new PlayerProgress();
        if (PlayerPrefs.HasKey("gameTries"))
        {
            this.playerProgress.gameTries = PlayerPrefs.GetInt("gameTries");
        }
        return this;
    }

    private DataController LoadLoginStatus()
    {
        this.loginStatus = new LoginStatus();
        if (PlayerPrefs.HasKey("loginActivated"))
        {
            this.loginStatus.ServicesActivated = PlayerPrefs.GetInt("loginActivated") != 0;
        }
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

    private DataController LoadWorldStatus()
    {
        this.worldStatus = new WorldStatus();
        if (PlayerPrefs.HasKey("currentWorldName"))
        {
            this.worldStatus.currentWorldName = PlayerPrefs.GetString("currentWorldName");
        }      
        return this;
    }

    private DataController SavePlayerProgress(PlayerProgress playerProgress)
    {
        PlayerPrefs.SetInt("gameTries", playerProgress.gameTries);
        return this;
    }

    private DataController SaveLoginStatus(LoginStatus loginStatus)
    {
        Func<bool, int> boolToInt = boolean => boolean ? 1 : 0;

        PlayerPrefs.SetInt("loginActivated", boolToInt(loginStatus.ServicesActivated));
        PlayerPrefs.SetInt("loggedIn", boolToInt(loginStatus.LoggedIn));
        PlayerPrefs.SetInt("refusedLogIn", boolToInt(loginStatus.RefusedLogIn));
        return this;
    }

    private PlayerProgress playerProgress;
    private LoginStatus loginStatus;
    private WorldStatus worldStatus;
}
