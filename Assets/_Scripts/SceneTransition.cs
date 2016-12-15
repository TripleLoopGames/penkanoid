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

    public SceneTransition TransitionIn ()
    {
        StartCoroutine(ExecuteTransitionIn());
	    return this;
	}

    public SceneTransition TransitionOut()
    {
        StartCoroutine(ExecuteTransitionOut());
        return this;
    }

    IEnumerator ExecuteTransitionIn()
    {
        float currentValue = holeMaterial.GetFloat("_Radius");
        while (currentValue > 0)
        {
            Debug.Log(currentValue);
            currentValue -= 0.5f;
            holeMaterial.SetFloat("_Radius", currentValue);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator ExecuteTransitionOut()
    {
        float currentValue = holeMaterial.GetFloat("_Radius");
        while (currentValue < 2.5)
        {
            Debug.Log(currentValue);
            currentValue += 0.5f;
            holeMaterial.SetFloat("_Radius", currentValue);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private Material holeMaterial;
}
