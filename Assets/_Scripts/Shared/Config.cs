using UnityEngine;
using System.Collections;

public static class Config
{
    public static class LevelFactory
    {
        public const int Rows = 11;
        public const int Columns = 15;
        public const float OffsetRows = 0.60f;
        public const float OffsetColumns = 1.22f;
        public static readonly Vector2 InitialPosition = new Vector2(-8.78f, 3.51f);
        
    }

    public static class SoundPlayer
    {
        public static class SoundTypes
        {
            public const string Music = "Music";
            public const string Effect = "Effects";
        }
    }

    public static class Player
    {
        public static readonly Vector2 InitialPosition = new Vector2(0f, -5.5f);
        public const int InitialHealth = 3;
    }

    public static class GameFlow
    {
        public const int countDownTime = 200;
    }

    public static class Ball
    {
        public const int lifetime = 10;
    }

    public static class WorldProgress
    {
        public const int lifetime = 10;
    }    
}