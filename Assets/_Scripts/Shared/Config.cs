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
        public static readonly WorldData[] worldsData = new WorldData[]
        {
           new WorldData("basic", new LevelData[]
           {
              new LevelData { name="W01_S01_level"},
              new LevelData { name="W01_S02_level"},
              new LevelData { name="W01_S03_level"},
              new LevelData { name="W01_S04_level"},
              new LevelData { name="W01_S05_level"},
           }),
           new WorldData("rock", new LevelData[]
           {
              new LevelData { name="W02_S01_level"},
              new LevelData { name="W02_S02_level"},
              new LevelData { name="W02_S03_level"},
              new LevelData { name="W02_S04_level"},
              new LevelData { name="W02_S05_level"},
           })
        };
    }

    public static class Exceptions
    {
        public const string RefusedLogin = "User Refused Login";
        public const string FailedLogin = "User Login has Failed";
        public const string FailedPublishScore = "Publish score has Failed"; 
    }
}