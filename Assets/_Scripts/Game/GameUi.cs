using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUi : MonoBehaviour {

    public GameUi Initialize()
    {
        return this;
    }

    public GameUi SetCamera(Camera camera)
    {
        GetComponent<Canvas>().worldCamera = camera;
        return this;
    }
}
