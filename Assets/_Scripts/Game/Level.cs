using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResourcesPickups = SRResources.Game.Pickups;

public class Level : MonoBehaviour
{

    public Level Initialize()
    {
        int index = 0;
        Dictionary<TypeSafe.PrefabResource, int> pickups = GetPickups();
        GameObject[] blocks = GetComponentsInChildren<Block>().Select(block =>
        {
            block.Initialize(index);
            TypeSafe.PrefabResource choosen = Utils.RandomWeightedChooser(pickups);
            if(choosen.Name != "EmptyPickup")
            {
                GameObject pickUP = choosen.Instantiate();
                block.SetItemOnHit(pickUP);
            }
            index++;
            return block.gameObject;
        }).ToArray();
        return this;
    }

    private Dictionary<TypeSafe.PrefabResource, int> GetPickups()
    {
        Dictionary<TypeSafe.PrefabResource, int> pickups = new Dictionary<TypeSafe.PrefabResource, int>();
        pickups.Add(ResourcesPickups.Heart, 3);
        pickups.Add(ResourcesPickups.Clock, 3);
        pickups.Add(ResourcesPickups.Star, 3);
        pickups.Add(ResourcesPickups.EmptyPickup, 91);
        return pickups;
    }

    public Level Destroy()
    {
        Destroy(this.gameObject);
        return this;
    }
}
