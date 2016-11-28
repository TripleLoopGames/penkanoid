using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    public Block Die()
    {
        gameObject.SetActive(false);
        return this;
    }

    private void Start()
    {
        Color[] colors = { Color.red, Color.magenta, Color.blue, Color.cyan, Color.green};
        Color randomColor = colors[Random.Range(0, colors.Length - 1)];
        GetComponent<SpriteRenderer>().color = randomColor;
    }
}



