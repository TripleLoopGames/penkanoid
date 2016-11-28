using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using LocalConfig = Config.Generation;

public class BreakoutCool : MonoBehaviour {

	// Use this for initialization

    private void Initialize()
    {
        this.originPosition = LocalConfig.SpawnArea.OriginPosition;
        this.endPosition = LocalConfig.SpawnArea.EndPosition;
        InitializeBlocks();
    }

    private BreakoutCool InitializeBlocks()
    {

        int[] preArray = new int[20];
        GameObject blockParent = new GameObject("BlockParent");
        GameObject[] blocksArray = preArray.Select((int _, int index) => {
            Vector2 position = GetAvaliablePosition();
            GameObject blockObject = SRResources.Game.Block.Instantiate(position);
            blockObject.GetComponent<Block>().Initialize(index);
            blockObject.transform.SetParent(blockParent.transform);
            return blockObject;
        }).ToArray();
        return this;
    }

    private void Start()
    {
        Initialize();
    }

    private Vector2 GetAvaliablePosition()
    {
        int colliderCount;
        int tries = 0;
        Vector2 testedPosition;
        Collider2D[] collidersDetected = new Collider2D[2];
        float radius = LocalConfig.FindPosition.InitialRadius;
        do
        {
            testedPosition = RandomPosition();
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

    private Vector2 RandomPosition()
    {
        float xPosition = Random.Range(this.originPosition.x, this.endPosition.x);
        float yPosition = Random.Range(this.originPosition.y, this.endPosition.y);
        return new Vector2(xPosition, yPosition);
    }

    private Vector2 originPosition;
    private Vector2 endPosition;

    [SerializeField]
    private LayerMask avoidSpawnInLayers;
}
