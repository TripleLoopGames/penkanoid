using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public SceneTransition Initialize()
    {
        this.holeMaterial = this.GetComponent<RawImage>().material;
        this.holeMaterial.SetFloat("_Radius", 0);
        return this;
    }

    public SceneTransition Enter(Action onEnd)
    {
        Debug.Log("Enter");
        StartCoroutine(AnimateEnter(onEnd));
        return this;
    }

    public SceneTransition Exit(Action onEnd)
    {
        Debug.Log("Exit");
        StartCoroutine(AnimateExit(onEnd));
	    return this;
	}

    IEnumerator AnimateEnter(Action afterAnimation)
    {
        float currentValue = holeMaterial.GetFloat("_Radius");
        while (currentValue < 1.2f)
        {
            holeMaterial.SetFloat("_Radius", currentValue);
            currentValue += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        holeMaterial.SetFloat("_Radius", 1.2f);
        afterAnimation();
    }

    IEnumerator AnimateExit(Action afterAnimation)
    {
        float currentValue = holeMaterial.GetFloat("_Radius");
        while (currentValue > 0)
        {
            holeMaterial.SetFloat("_Radius", currentValue);
            currentValue -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        holeMaterial.SetFloat("_Radius", 0);
        afterAnimation();
    }

    private Material holeMaterial;
}
