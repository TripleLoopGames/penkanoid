using UnityEngine;
using System.Linq;
using LocalConfig = Config.LevelFactory;
using LevelResources = SRResources.Game.Levels;
using BuildingResources = SRResources.Game.Building;

public class LevelFactory : MonoBehaviour
{
    public Level LoadLevel(int levelId, int worldId)
    {
        TextAsset jsonLevel = Resources.Load($"Game/Levels/World_{worldId}/Level_{levelId}") as TextAsset;
        if (jsonLevel == null)
        {
            Debug.LogError("Level not Found!");
            return null;
        }
        LevelData levelData = JsonUtility.FromJson<LevelData>(jsonLevel.text);
        Block[,] blockLayout = new Block[LocalConfig.Rows, LocalConfig.Columns];
        Level level = BuildingResources.Level.Instantiate().GetComponent<Level>();
        level.name = $"Level_{worldId}_{levelId}";

        levelData.layout.Select(blockData =>
        {
            Vector2 position = LocalConfig.InitialPosition;
            position.x += blockData.column * LocalConfig.OffsetColumns;
            position.y -= blockData.row * LocalConfig.OffsetRows;
            Block block = BuildingResources.Block.Instantiate(position).GetComponent<Block>();
            block.transform.SetParent(level.transform, false);
            blockLayout[blockData.row, blockData.column] = block;
            return block;
        });

        level.Initialize(blockLayout);
        return level;
    }

}
