using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorUtils : MonoBehaviour {

    public void AddComponentInChildren()
    {
        // component to add
        GameObject[] myObject = GetComponentsInChildren<Transform>().Select(block => {
            block.gameObject.AddComponent<Block>();
            return block.gameObject;
        }).ToArray();
    }
}
