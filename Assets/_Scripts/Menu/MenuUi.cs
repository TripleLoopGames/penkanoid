using System.Collections;
using System.Collections.Generic;
using Resources = SRResources.Menu.Ui;
using System;
using DG.Tweening;
using RSG;
using UnityEngine;
using UnityEngine.UI;


public class MenuUi : MonoBehaviour {

  public MenuUi Initialize(WorldSave[] worldSaves, Action closeGame, Action openLeaderboard) {
    InitializeBackground()
      .InitializeCanvasGroup()
      .InitializeLevelSelector(worldSaves)
      .InitializeButtons(closeGame, openLeaderboard);
    return this;
  }

  public IPromise<string> WaitForNextLevel() {
    return this.levelSelector.WaitForNextLevel();
  }

  public IPromise ZoomIn(float time = 1) {
    //hack to zoom we have to get all first depth child into a gameobject and scale that
    GameObject zoomableObject = new GameObject("ZoomableObject");
    zoomableObject.AddComponent<CanvasGroup>();
    zoomableObject.transform.position = new Vector2(0, -300);
    zoomableObject.transform.SetParent(this.transform, false);
    Transform[] firstdepthChildren = GetComponentsInChildren<Transform>()
      .filter(childTransfrom => childTransfrom.parent == this.transform)
      .map(childTransform => {
        childTransform.SetParent(zoomableObject.transform, true);
        return childTransform;
      })
      .ToArray();
    return new Promise((resolve, reject) => {
      zoomableObject.transform.DOScale(new Vector2(6, 6), time)
        .OnComplete(() => resolve());
    });
  }

  public IPromise EnterAnimation() {
    return Promise.All(
      new Promise((resolve, reject) => {
        Vector2 targetPosition = this.closeGame.GetComponentInChildren<WaypointUi>().GetPosition();
        RectTransform rectTransform = this.closeGame.GetComponent<RectTransform>();
        rectTransform.DOMove(targetPosition, 1f, false)
          .From()
          .OnComplete(() => resolve());
      }),
      new Promise((resolve, reject) => {
        Vector2 targetPosition = this.openLeaderboard.GetComponentInChildren<WaypointUi>().GetPosition();
        RectTransform rectTransform = this.openLeaderboard.GetComponent<RectTransform>();
        rectTransform.DOMove(targetPosition, 1f, false)
          .From()
          .OnComplete(() => resolve());
      }),
      this.levelSelector.EnterAnimation()
    );
  }

  public MenuUi MakeNonInteractable() {
    this.canvasGroup.interactable = false;
    return this;
  }

  public MenuUi MakeInteractable() {
    this.canvasGroup.interactable = true;
    return this;
  }

  private MenuUi InitializeBackground() {
    GameObject background = Resources.Background.Instantiate();
    background.name = "Background";
    background.transform.SetParent(this.gameObject.transform, false);
    return this;
  }

  private MenuUi InitializeLevelSelector(WorldSave[] worldSaves) {
    this.levelSelector = Resources.LevelSelector.Instantiate().GetComponent<LevelSelector>();
    this.levelSelector.Initialize(worldSaves);
    this.levelSelector.name = "LevelSelector";
    this.levelSelector.transform.SetParent(this.gameObject.transform, false);
    return this;
  }

  private MenuUi InitializeCanvasGroup() {
    this.canvasGroup = GetComponent<CanvasGroup>();
    return this;
  }

  private MenuUi InitializeButtons(Action closeGame, Action openLeaderboard) {
    this.closeGame = Resources.CloseGame.Instantiate().GetComponent<Button>();
    this.closeGame.onClick.AddListener(() => closeGame());
    this.closeGame.transform.SetParent(this.gameObject.transform, false);
    this.openLeaderboard = Resources.OpenLeaderboard.Instantiate().GetComponent<Button>();;
    this.openLeaderboard.onClick.AddListener(() => openLeaderboard());
    this.openLeaderboard.transform.SetParent(this.gameObject.transform, false);
    return this;
  }

  LevelSelector levelSelector;
  private CanvasGroup canvasGroup;
  Button closeGame;
  Button openLeaderboard;
}