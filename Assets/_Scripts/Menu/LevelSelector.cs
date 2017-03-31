using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

	public LevelSelector Initialize(){
        Transform[] transforms = GetComponentsInChildren<Transform>();
        bool[] activated = transforms.Select(currentTransform =>
        {
            if (currentTransform.name == "GoLeft")
            {
                this.GoLeft = currentTransform.gameObject.GetComponent<Button>();
                return true;
            }
            if (currentTransform.name == "GoRight")
            {
                this.GoRight = currentTransform.gameObject.GetComponent<Button>();
                return true;
            }          
            return false;
        }).ToArray();
        int activatedAmount = activated.Where(element => element).Count();
        if (activatedAmount != 2)
        {
            Debug.LogError("Cound not find proper amount of elements");
        }
        return this;
    }

    private Button GoLeft;
    private Button GoRight;
}
