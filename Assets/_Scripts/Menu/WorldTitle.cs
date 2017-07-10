using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldTitle : MonoBehaviour {

	public WorldTitle ChangeTextImage(string type)
    {
        Image uiTitle = GetComponentInChildren<Image>();
        if (type == "basic")
        {
            uiTitle.sprite = TitleSprites[0];
        }
        if (type == "rock")
        {
            uiTitle.sprite = TitleSprites[1];
        }
        if (type == "jungle")
        {
            uiTitle.sprite = TitleSprites[2];
        }
        //Ice world doesn't exists
        /*if (type == "ice")
        {
            uiTitle.sprite = TitleSprites[3];
        }*/
        if (type == "lava")
        {
            uiTitle.sprite = TitleSprites[3];
        }
        return this;
    }

    [SerializeField]
    private Sprite[] TitleSprites;

}
