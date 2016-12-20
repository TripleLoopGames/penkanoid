using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Linq;

public static class Utils
{
    public static T RandomWeightedChooser<T>(Dictionary<T, int> weights)
    {
        int totalWeight = weights.Sum(c => c.Value);
        int choice = Random.Range(0, totalWeight);
        int sum = 0;
        foreach (var obj in weights)
        {
            for (int i = sum; i < obj.Value + sum; i++)
            {
                if (i >= choice)
                {
                    return obj.Key;
                }
            }
            sum += obj.Value;
        }

        return weights.First().Key;
    }
}
