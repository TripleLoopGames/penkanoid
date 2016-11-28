using UnityEngine;
using System.Collections;

public static class Config
{
    public static class Generation
    {
        public static class SpawnArea
        {
            public static readonly Vector2 OriginPosition = new Vector2(-7.76f, 4.04f);
            public static readonly Vector2 EndPosition = new Vector2(8.21f, 0);
        }

        public static class FindPosition
        {
            //inital limit area between elements
            public const float InitialRadius = 0.6f;
            //tries before reducing the radius
            public const int MaxTries = 15;
        }
    }
}