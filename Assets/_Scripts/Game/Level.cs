using System;
using System.Collections.Generic;
using UnityEngine;
using ResourcesPickups = SRResources.Game.Building.Pickups;
using System.Linq;

public class Level : MonoBehaviour
{
    public Level Initialize(Block[,] blockLayout, Block[] blocks)
    {
        this.blockLayout = blockLayout;
        this.blocks = blocks;
        return this;
    }

    public Level SetLevelCleared(Action onLevelCleared)
    {
        this.onLevelCleared = onLevelCleared;
        return this;
    }

    public Level EnableIgnoreCollisionResult()
    {
        this.blocks.ToList().ForEach(block =>
        {
            block.GetComponent<Block>().EnableIgnoreCollisionResult();
        });
        return this;
    }

    public Level DestroyPickUps()
    {
        this.pickUps.ForEach(pickUp =>
        {
            // if pickup consumed do not try to destroy it
            if (pickUp == null)
            {
                return;
            }
            Destroy(pickUp);
            return;
        });
        return this;
    }

    public Level Destroy()
    {
        Destroy(this.gameObject);
        return this;
    }

    public Block[] GetAdjecentBlocks(Vector2 position, Block[,] blocksMatrix)
    {
        Vector2[] directions = new Vector2[] {
            new Vector2(-1, -1),
            new Vector2(-1, 0),
            new Vector2(-1, 1),
            new Vector2(0, -1),
            new Vector2(0, 1),
            new Vector2(1, -1),
            new Vector2(1, 0),
            new Vector2(1, 1)
        };

        int maxLength = blocksMatrix.GetLength(0);
        int maxWidth = blocksMatrix.GetLength(1);

        Func<Vector2, bool> isDirectionAvailable = (direction) =>
        {
            Vector2 result = direction + position;
            if (result.x > maxLength || result.y > maxWidth)
            {
                return false;
            }
            if (result.x < 0 || (int)result.y < 0)
            {
                return false;
            }
            Block foundBlock = blocksMatrix[(int)result.x, (int)result.y];
            if (foundBlock == null)
            {
                return false;
            }
            if (!foundBlock.enabled)
            {
                return false;
            }
            return true;
        };
       

        Func<Vector2, Block> getBlock = direction =>
        {
            Vector2 result = direction + position;
            return blocksMatrix[(int)result.x, (int)result.y];
        };

        Block[] adjecentBlocks = directions
            .Where(isDirectionAvailable)
            .Select(getBlock)
            .ToArray();
        return adjecentBlocks;
    }

    // public so it can be set up externaly
    public Level OnBlockDestroyed(string blockId, bool explosive)
    {
        if (explosive)
        {
            Block targetBlock = Array.Find(this.blocks, block => block.GetId() == blockId);
            Block[] blocksToDestroy = GetAdjecentBlocks(targetBlock.GetMatrixPosition(), this.blockLayout);
            Block[] blocksDestroyed = blocksToDestroy.Select(block => block.Kill()).ToArray();
        }
        bool finished = Array.TrueForAll(this.blocks, block =>
        {
            // if not active should not count towards total
            if (!block.gameObject.activeInHierarchy)
            {
                return true;
            }
            // if it's the same as if activated should not count towards total
            if (blockId == block.GetId())
            {
                return true;
            }
            // if it's indestructible should not count towards total
            if (block.Indestructible)
            {
                return true;
            }
            return false;
        });
        if (!finished)
        {
            return this;
        }
        this.onLevelCleared();
        return this;
    }

    private Dictionary<TypeSafe.PrefabResource, int> GetPickups()
    {
        Dictionary<TypeSafe.PrefabResource, int> pickups = new Dictionary<TypeSafe.PrefabResource, int>();
        pickups.Add(ResourcesPickups.heart, 3);
        pickups.Add(ResourcesPickups.clock, 3);
        pickups.Add(ResourcesPickups.star, 3);
        pickups.Add(ResourcesPickups.EmptyPickup, 91);
        return pickups;
    }

    private Block[,] blockLayout;
    private Block[] blocks;
    private List<GameObject> pickUps = new List<GameObject>();
    private Action onLevelCleared;

}
