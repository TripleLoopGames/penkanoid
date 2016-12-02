using UnityEngine;

using Random = UnityEngine.Random;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(LevelManager))]
public class BreakoutCool : MonoBehaviourEx, IHandle<PlayerDeadMessage>
{
    public void Handle(PlayerDeadMessage message)
    {
        this.inputManager.DisableInput();
    }

    private void Initialize()
    {
        InitializeLevelManager()
        .InitializeInputManager();
        this.inputManager.EnableInput();
    }

    private BreakoutCool InitializeInputManager()
    {
        this.inputManager = GetComponent<InputManager>();
        this.inputManager.Initialize();
        return this;
    }

    private BreakoutCool InitializeLevelManager()
    {
        this.levelManager = GetComponent<LevelManager>();
        this.levelManager.InitializeBlocks();
        return this;
    }

    private BreakoutCool InitializeCanvas()
    {
        this.levelManager = GetComponent<LevelManager>();
        this.levelManager.InitializeBlocks();
        return this;
    }

    private void Start()
    {
        Initialize();
    }

    private InputManager inputManager;
    private LevelManager levelManager;
}
