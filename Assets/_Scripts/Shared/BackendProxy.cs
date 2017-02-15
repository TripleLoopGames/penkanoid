using System;
using GooglePlayGames;
using UnityEngine;
using RSG;

public class BackendProxy : MonoBehaviour
{
#if UNITY_STANDALONE
    public void LogUser(Action<bool> callback)
    {
        callback(true);
    }

    public void PublishScore(int score, Action<bool> callback)
    {
        callback(true);
    }

    public void ShowLeaderboard()
    {
    }

    public BackendProxy Initialize()
    {
        return this;
    }
#endif

#if UNITY_ANDROID
    public IPromise Authenticate()
    {
        return new Promise((resolve, reject) =>
        {
            Social.localUser.Authenticate((sucess) =>
            {
                if (sucess)
                {
                    resolve();
                    return;
                }
                reject(new Exception($"Failed authenticating the user"));
            });
        });
    }

    public IPromise PublishScore(int score, string leaderBoardId = GPGSIds.leaderboard_test_cool_leaderboard_d)
    {
        return new Promise((resolve, reject) =>
        {
            Social.ReportScore(score, leaderBoardId, (sucess) =>
            {
                if (sucess)
                {
                    resolve();
                    return;
                }
                reject(new Exception($"Failed publishing score to leaderboard"));
            });
        });
    }

    public void ShowLeaderboard(string leaderBoardId = GPGSIds.leaderboard_test_cool_leaderboard_d)
    {
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderBoardId);
    }

    public BackendProxy Initialize()
    {
        PlayGamesPlatform.Activate();
        return this;
    }

#endif

}