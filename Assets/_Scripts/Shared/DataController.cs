using UnityEngine;

public class DataController : MonoBehaviour
{
    private PlayerProgress playerProgress;

    public DataController Initialize()
    {
        this.LoadPlayerProgress();
        return this;
    }

    public int GetPlayerGameTries()
    {
        return this.playerProgress.gameTries;
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

    private DataController SavePlayerProgress(PlayerProgress playerProgress)
    {
        PlayerPrefs.SetInt("gameTries", playerProgress.gameTries);
        return this;
    }
}
