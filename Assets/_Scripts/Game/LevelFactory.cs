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

        int blockCount = 0;
        // it's easier to operate trough normal array
        Block[] blocks = levelData.layout.Select(blockData =>
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
            block.Initialize(blockCount, blockData.type, (id, explosive) => level.OnBlockDestroyed(id, explosive));
            block.transform.SetParent(level.transform, false);

            //create and set content (if there is any)
            if (blockData.content != null)
            {
                GameObject pickUpResource = Resources.Load($"Game/Building/Pickups/{blockData.content}") as GameObject;
                if (pickUpResource == null)
                {
                    Debug.LogError("Block content Resource not found!");
                    return null;
                }
                GameObject pickUp = Instantiate(pickUpResource);
                block.SetItemOnHit(pickUp);
            }
            blockLayout[blockData.row, blockData.column] = block;
            blockCount++;
            return block;
        }).ToArray();

        level.Initialize(blockLayout, blocks);
        return level;
    }

}
