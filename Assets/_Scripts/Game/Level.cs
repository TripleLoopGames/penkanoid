using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public Level Initialize()
    {
        int index = 0;
        GameObject[] blocks = GetComponentsInChildren<Block>().Select(block => {
            block.Initialize(index);
            index++;
            return block.gameObject;
        }).ToArray();
        return this;
    }

    public Level Destroy()
    {
        Destroy(this.gameObject);
        return this;
    }
}
