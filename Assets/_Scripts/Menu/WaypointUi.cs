using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointUi : MonoBehaviour {

	public Vector3 GetPosition()
    {
        return GetComponent<RectTransform>().position;
    }
}
