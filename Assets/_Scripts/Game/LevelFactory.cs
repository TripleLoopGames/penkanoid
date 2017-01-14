using UnityEngine;
using System.Linq;
using LocalConfig = Config.LevelFactory;
using LevelResources = SRResources.Game.Levels;
using BuildingResources = SRResources.Game.Building;

public class LevelFactory : MonoBehaviour
{
    public Level CreateLevel(int levelId, int worldId)
    {
        TextAsset jsonLevel = Resources.Load($"Game/Levels/World_{worldId}/Level_{levelId}") as TextAsset;
        if (jsonLevel == null)
        {
            Debug.LogError("Level Resource not Found!");
            return null;
        }
        LevelData levelData = JsonUtility.FromJson<LevelData>(jsonLevel.text);
        Block[,] blockLayout = new Block[LocalConfig.Rows, LocalConfig.Columns];
        Level level = BuildingResources.Level.Instantiate(new Vector2(0.25f,-0.25f)).GetComponent<Level>();
        level.name = $"Level_{worldId}_{levelId}";

        levelData.layout.Select(blockData =>
        {
            Vector2 position = LocalConfig.InitialPosition;
            position.x += blockData.column * LocalConfig.OffsetColumns;
            position.y -= blockData.row * LocalConfig.OffsetRows;
            GameObject blockResource = Resources.Load($"Game/Building/Blocks/{blockData.type}") as GameObject;
            if (blockResource == null)
            {
                Debug.LogError("Block Resource not found!");
                return null;
            }
            Block block = Instantiate(blockResource, position, Quaternion.identity).GetComponent<Block>();
            block.transform.SetParent(level.transform, false);
            blockLayout[blockData.row, blockData.column] = block;
            return block;
        });

        level.Initialize(blockLayout);
        return level;
    }

}
