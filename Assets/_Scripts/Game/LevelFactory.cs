using UnityEngine;
using LocalConfig = Config.LevelFactory;
using BuildingResources = SRResources.Game.Building;

public class LevelFactory : MonoBehaviour
{
    public Level CreateLevel(WorldStage worldstage)
    {
        TextAsset jsonLevel = Resources.Load($"Game/Worlds/{worldstage.World}/{worldstage.Level}") as TextAsset;
        if (jsonLevel == null)
        {
            Debug.LogError($"Level Resource not Found!:{worldstage.World}/{worldstage.Level}");
            return null;
        }
        LevelData levelData = JsonUtility.FromJson<LevelData>(jsonLevel.text);
        Level level = BuildingResources.Level.Instantiate(new Vector2(0.25f,-0.25f)).GetComponent<Level>();
        level.name = $"Level_{worldstage.World}_{worldstage.Level}";
        Block[,] blockLayout = new Block[LocalConfig.Rows, LocalConfig.Columns];

        GameObject layout = new GameObject("Layout");
        layout.transform.SetParent(level.transform, false);

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
                Debug.LogError($"Block Resource not found!:{blockData.type}");
                return null;
            }
            Block block = Instantiate(blockResource, position, Quaternion.identity).GetComponent<Block>();
            string blockId = $"{blockData.row}_{ blockData.column}";
            block.Initialize(blockId, new Vector2(blockData.row, blockData.column),  blockData.type, (id, explosive) => level.OnBlockDestroyed(id, explosive));
            block.transform.SetParent(layout.transform, false);

            //create and set content (if there is any)
            if (blockData.content != null)
            {
                GameObject pickUpResource = Resources.Load($"Game/Building/Pickups/{blockData.content}") as GameObject;
                if (pickUpResource == null)
                {
                    Debug.LogError($"Block content Resource not found!:{blockData.content}");
                    return null;
                }
                GameObject pickUp = Instantiate(pickUpResource);
                block.SetItemOnHit(pickUp);
            }
            blockLayout[blockData.row, blockData.column] = block;
            blockCount++;
            return block;
        }).ToArray();

        GameObject scenario = CreateSceneario(levelData.wall, levelData.background);
        scenario.transform.SetParent(level.transform, true);
        level.Initialize(blockLayout, blocks);
        return level;
    }

    public GameObject CreateSceneario(string wallName, string backgroundName)
    {
        GameObject wallResource = Resources.Load($"Game/Building/Walls/{wallName}") as GameObject;
        if (wallResource == null)
        {
            Debug.LogError($"Wall Resource not found!:{wallName}");
            return null;
        }
        GameObject wall = Instantiate(wallResource, new Vector2(0, 0), Quaternion.identity);
        wall.name = $"Wall_{wallName}";
      
        GameObject backgroundResource = Resources.Load($"Game/Building/Backgrounds/{backgroundName}") as GameObject;
        if (backgroundResource == null)
        {
            Debug.LogError($"Background Resource not found!:{backgroundName}");
            return null;
        }
        GameObject background = Instantiate(backgroundResource, new Vector2(0, 0), Quaternion.identity);
        background.name = $"Background_{backgroundName}";
        background.transform.SetParent(wall.transform, true);
        return wall;
    }

}
