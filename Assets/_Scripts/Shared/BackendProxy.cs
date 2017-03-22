using System;
using GooglePlayGames;
using UnityEngine;
using RSG;
using System.Collections;
using Exceptions = Config.Exceptions;

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
            LoginStatus loginStatus = this.dataController.GetLoginStatus();
            if (loginStatus.LoggedIn)
            {
                resolve();
                return;
            }
            if (loginStatus.RefusedLogIn)
            {
                reject(new Exception(Exceptions.RefusedLogin));
                return;
            }
            Social.localUser.Authenticate((sucess) =>
            {
                if (sucess)
                {
                    resolve();
                    return;
                }
                reject(new Exception(Exceptions.FailedLogin));
            });
        });
    }

    public IPromise PublishScore(int score, string leaderBoardId = GPGSIds.leaderboard_test_cool_leaderboard_d)
    {
        return new Promise((resolve, reject) =>
        {
            LoginStatus loginStatus = this.dataController.GetLoginStatus();
            if (loginStatus.RefusedLogIn)
            {
                reject(new Exception(Exceptions.RefusedLogin));
                return;
            }
            Social.ReportScore(score, leaderBoardId, (sucess) =>
            {
                if (sucess)
                {
                    resolve();
                    return;
                }
                reject(new Exception(Exceptions.FailedPublishScore));
            });
        });
    }

    public void ShowLeaderboard(string leaderBoardId = GPGSIds.leaderboard_test_cool_leaderboard_d)
    {
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderBoardId);
    }

    public BackendProxy Initialize()
    {       
        return this;
    }

    public BackendProxy SetDataController(DataController dataController)
    {
        this.dataController = dataController;
        LoginStatus loginStatus = this.dataController.GetLoginStatus();
        if (loginStatus.ServicesActivated)
        {
            return this;
        }
        PlayGamesPlatform.Activate();
        return this;
    }
#endif
    private DataController dataController;
}