using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using LocalConfig = Config.LevelCreator;

public class LevelCreator : MonoBehaviour
{
    public LevelCreator Initialize()
    {
        this.blockParent = new GameObject("BlockParent");
        return this;
    }

    public GameObject GenerateLevel()
    {
        this.avoidSpawnInLayers.value = LocalConfig.layerMask;
        int[] preArray = new int[30];
        this.blocksArray = preArray.Select((int _, int index) =>
        {
            Vector2 position = GetAvaliablePosition();
            GameObject blockObject = SRResources.Game.Block.Instantiate(position);
            blockObject.GetComponent<Block>().Initialize(index);
            blockObject.transform.SetParent(this.blockParent.transform);
            return blockObject;
        }).ToArray();
        return this.blockParent;
    }

    public LevelCreator Reset()
    {
        this.blocksArray.Select((GameObject block, int index) =>
        {
            Destroy(block);
            return block;
        }).ToArray();
        return this;
    }

    private Vector2 GetAvaliablePosition()
    {
        int colliderCount;
        int tries = 0;
        Vector2 testedPosition;
        Collider2D[] collidersDetected = new Collider2D[2];
        float radius = LocalConfig.FindPosition.InitialRadius;
        Vector2 originPosition = LocalConfig.SpawnArea.OriginPosition;
        Vector2 endPosition = LocalConfig.SpawnArea.EndPosition;
        do
        {
            testedPosition = RandomPosition(originPosition, endPosition);
            colliderCount = Physics2D.OverlapCircleNonAlloc(testedPosition, radius, collidersDetected, this.avoidSpawnInLayers);
            tries++;
            if (tries >= LocalConfig.FindPosition.MaxTries)
            {
                Debug.Log("Didn't find position after " + tries + " tries, reducing radius");
                if (radius >= 0.1)
                {
                    radius -= 0.1f;
                    tries = 0;
                    continue;
                }
                Debug.Log("Failed default position returned");
                return new Vector2();
            }
        } while (colliderCount != 0);
        return testedPosition;
    }

    private Vector2 RandomPosition(Vector2 upperLeftLimit, Vector2 lowerRightLimit)
    {
        float xPosition = Random.Range(upperLeftLimit.x, lowerRightLimit.x);
        float yPosition = Random.Range(upperLeftLimit.y, lowerRightLimit.y);
        return new Vector2(xPosition, yPosition);
    }

    private LayerMask avoidSpawnInLayers;
    GameObject[] blocksArray;
    GameObject blockParent;
}
