using UnityEngine;
using LocalConfig = Config.LevelFactory;
using BuildingResources = SRResources.Game.Building;

public class LevelFactory : MonoBehaviour
{
    public Level CreateLevel(WorldStage worldstage)
    {
        string levelPath = string.Format("Game/Worlds/{0}/{1}", worldstage.World, worldstage.Level);
        TextAsset jsonLevel = Resources.Load(levelPath) as TextAsset;
        if (jsonLevel == null)
        {
            Debug.LogError("Level Resource not Found!"+worldstage.World+"/"+worldstage.Level);
            return null;
        }
        LevelData levelData = JsonUtility.FromJson<LevelData>(jsonLevel.text);
        Level level = BuildingResources.Level.Instantiate(new Vector2(0.25f,-0.25f)).GetComponent<Level>();
        level.name = string.Format("Level_{0}_{1}", worldstage.World, worldstage.Level);
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
            string blockPath = string.Format("Game/Building/Blocks/{0}", blockData.type);
            GameObject blockResource = Resources.Load(blockPath) as GameObject;
            if (blockResource == null)
            {
                Debug.LogError("Block Resource not found!:"+blockData.type);
                return null;
            }
            Block block = Instantiate(blockResource, position, Quaternion.identity).GetComponent<Block>();
            string blockId = string.Format("{0}/{1}", blockData.row, blockData.column);
            block.Initialize(blockId, new Vector2(blockData.row, blockData.column),  blockData.type, (id, explosive) => level.OnBlockDestroyed(id, explosive));
            block.transform.SetParent(layout.transform, false);

            //create and set content (if there is any)
            if (blockData.content != null)
            {
                string pickupPath = string.Format("Game/Building/Pickups/{0}", blockData.content);
                GameObject pickUpResource = Resources.Load(pickupPath) as GameObject;
                if (pickUpResource == null)
                {
                    Debug.LogError("Block content Resource not found!"+blockData.content);
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
        string wallpath = string.Format("Game/Building/Walls/{0}", wallName);
        GameObject wallResource = Resources.Load(wallpath) as GameObject;
        if (wallResource == null)
        {
            Debug.LogError("Wall Resource not found!:"+wallName);
            return null;
        }
        GameObject wall = Instantiate(wallResource, new Vector2(0, 0), Quaternion.identity);
        wall.name = string.Format("Wall_{0}", wallName);

        string backgroundPath = string.Format("Game/Building/Backgrounds/{0}", backgroundName);
        GameObject backgroundResource = Resources.Load(backgroundPath) as GameObject;
        if (backgroundResource == null)
        {
            Debug.LogError("Background Resource not found!:"+backgroundName);
            return null;
        }
        GameObject background = Instantiate(backgroundResource, new Vector2(0, 0), Quaternion.identity);       
        background.name = string.Format("Background_{0}", backgroundName);
        background.transform.SetParent(wall.transform, true);
        return wall;
    }

}
