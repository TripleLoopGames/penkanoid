using RSG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoleTransition : MonoBehaviour
{
    public HoleTransition Initialize(Color color, bool opened)
    {
        RawImage rawImage = this.GetComponent<RawImage>();
        rawImage.color = color;
        this.holeMaterial = rawImage.material;
        float radious = opened ? 1.2f : 0;
        this.holeMaterial.SetFloat("_Radius", radious);
        return this;
    }

    public IPromise Enter()
    {
        return new Promise((resolve, reject) => StartCoroutine(AnimateEnter(resolve, reject)));
    }

    public IPromise Exit()
    {
        return new Promise((resolve, reject) => StartCoroutine(AnimateExit(resolve, reject)));
    }

    IEnumerator AnimateEnter(Action resolve, Action<Exception> reject)
    {
        float currentValue = this.holeMaterial.GetFloat("_Radius");
        while (currentValue < 1.2f)
        {
            this.holeMaterial.SetFloat("_Radius", currentValue);
            currentValue += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        this.holeMaterial.SetFloat("_Radius", 1.2f);
        resolve();
    }

    IEnumerator AnimateExit(Action resolve, Action<Exception> reject)
    {
        float currentValue = this.holeMaterial.GetFloat("_Radius");
        while (currentValue > 0)
        {
            this.holeMaterial.SetFloat("_Radius", currentValue);
            currentValue -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        this.holeMaterial.SetFloat("_Radius", 0);
        resolve();
    }

    private Material holeMaterial;
}
