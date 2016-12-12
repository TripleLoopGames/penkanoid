using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using LocalConfig = Config.LevelCreator;
using Resources = SRResources.Game.Levels;

public class LevelCreator : MonoBehaviour
{
    public GameObject GenerateLevel(int id)
    {
        //TODO: logic for level generation - load from json
        switch (id)
        {
            case 1:
                return Resources.Level_1.Instantiate();
            case 2:
                return Resources.Level_2.Instantiate();
            case 3:
                return Resources.Level_3.Instantiate();
            default :
                return Resources.Level_1.Instantiate();
        }     
    }
}
