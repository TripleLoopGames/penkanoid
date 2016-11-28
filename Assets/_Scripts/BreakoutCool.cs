using UnityEngine;
using System.Collections;

public class BreakoutCool : MonoBehaviour {

	// Use this for initialization

    private void Initialize()
    {
        InitializeBackground()
        .InitializeScenario();
    }

    private BreakoutCool InitializeBackground()
    {
        // initialization of background
        return this;
    }

    private BreakoutCool InitializeScenario()
    {
        // initialization of scenario
        return this;
    }

    private void Start()
    {
        Initialize();
    }
}
