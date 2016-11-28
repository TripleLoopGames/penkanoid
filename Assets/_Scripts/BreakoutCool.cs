using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using LocalConfig = Config.Generation;

public class BreakoutCool : MonoBehaviour {

	// Use this for initialization

    private void Initialize()
    {
        _originPosition = LocalConfig.SpawnArea.OriginPosition;
        _endPosition = LocalConfig.SpawnArea.EndPosition;
        InitializeBlocks();
    }

    private BreakoutCool InitializeBlocks()
    {
        int[] preArray = new int[20];
        GameObject[] blocksArray = preArray.Select((int ho) => {
            Vector2 position = GetAvaliablePosition();
            return SRResources.Game.Block.Instantiate(position);
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
            colliderCount = Physics2D.OverlapCircleNonAlloc(testedPosition, radius, collidersDetected, _avoidSpawnInLayers);
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
        float xPosition = Random.Range(_originPosition.x, _endPosition.x);
        float yPosition = Random.Range(_originPosition.y, _endPosition.y);
        return new Vector2(xPosition, yPosition);
    }

    private Vector2 _originPosition;
    private Vector2 _endPosition;

    [SerializeField]
    private LayerMask _avoidSpawnInLayers;
}
