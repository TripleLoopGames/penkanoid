using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Linq;
using UnityEngine;
using System;

public static class Utils
{
    public static void SetNameAndParent(string name, GameObject child, GameObject parent)
    {
        child.name = name;
        child.transform.SetParent(parent.transform, false);
    }
}
