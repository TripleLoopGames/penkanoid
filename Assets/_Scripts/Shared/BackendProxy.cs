using System;
using GooglePlayGames;
using UnityEngine;
using RSG;
using System.Collections;

public class BackendProxy : MonoBehaviour
{
#if UNITY_STANDALONE
    public IPromise Authenticate()
    {
        return new Promise((resolve, reject) =>
        {
            // fake wait to simulate mobile delay
            StartCoroutine(DelayedExecution(resolve, 10));
        });
    }

    public IPromise PublishScore(int score, string leaderBoardId = GPGSIds.leaderboard_test_cool_leaderboard_d)
    {
        return new Promise((resolve, reject) =>
        {
            // fake wait to simulate mobile delay
            StartCoroutine(DelayedExecution(resolve));
        });
    }

    public void ShowLeaderboard(string leaderBoardId = GPGSIds.leaderboard_test_cool_leaderboard_d)
    {
        return;
    }

    public BackendProxy Initialize()
    {        
        return this;
    }

    IEnumerator DelayedExecution(Action action, float time = 1f)
    {
        yield return new WaitForSeconds(time);
        action();
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