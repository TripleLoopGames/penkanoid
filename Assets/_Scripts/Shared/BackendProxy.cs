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
            StartCoroutine(DelayedExecution(resolve, 5));
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

    public BackendProxy Initialize(DataController dataController)
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
                    loginStatus.LoggedIn = true;
                    this.dataController.SetLoginStatus(loginStatus);
                    resolve();
                    return;
                }
                loginStatus.RefusedLogIn = true;
                this.dataController.SetLoginStatus(loginStatus);
                reject(new Exception(Exceptions.FailedLogin));
                return;
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
            if (!loginStatus.LoggedIn)
            {
                reject(new Exception(Exceptions.NotLoggedIn));
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

    public BackendProxy ShowLeaderboard(string leaderBoardId = GPGSIds.leaderboard_test_cool_leaderboard_d)
    {
        LoginStatus loginStatus = this.dataController.GetLoginStatus();
        if (!loginStatus.LoggedIn)
        {
            return this;
        }
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderBoardId);
        return this;
    }

    public BackendProxy Initialize(DataController dataController)
    {       
        this.dataController = dataController;
        LoginStatus loginStatus = this.dataController.GetLoginStatus();
        if (loginStatus.ServicesActivated)
        {
            Debug.Log("Nope only once");
            return this;
        }
        Debug.Log("Called Once");
        PlayGamesPlatform.Activate();
        loginStatus.ServicesActivated = true;       
        this.dataController.SetLoginStatus(loginStatus);
        return this;
    }
    
#endif
    private DataController dataController;
}