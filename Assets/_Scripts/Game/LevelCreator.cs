using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using LocalConfig = Config.LevelCreator;

public class LevelCreator : MonoBehaviour
{
    public GameObject GenerateLevel()
    {
        //TODO: logic for level generation - load from json
        GameObject level = SRResources.Game.Levels.Level_1.Instantiate();
        return level;
    }
}
