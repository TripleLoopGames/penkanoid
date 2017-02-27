using System;
using Localconfig = Config.WorldProgress;

public class WorldProgress
{
    public WorldStage GetFirstStage(string worldName)
    {
        WorldData worldData = FindWorldData(worldName);
        int levelId = 0;
        LevelData firstLevel = worldData.levelsData[levelId];
        bool isLast = worldData.levelsData.Length == levelId + 1;
        return new WorldStage(levelId, worldName, firstLevel.name, isLast);
    }

    public WorldStage GetNextStage(WorldStage worldStage)
    {
        if (!ValidateStage(worldStage))
        {
            return null;
        }
        WorldData worldData = FindWorldData(worldStage.World);
        int levelId = worldStage.Id + 1;
        bool isLast = worldData.levelsData.Length <= levelId + 1;
        return new WorldStage(levelId,
                              worldStage.World,
                              worldData.levelsData[levelId].name,
                              isLast);
    }

    private WorldData FindWorldData(string worldName)
    {
        return Array.Find(Localconfig.worldsData, worldData => worldData.name == worldName);
    }

    private bool ValidateStage(WorldStage worldStage)
    {
        if (worldStage.IsLast)
        {
            return false;
        }
        WorldData worldData = FindWorldData(worldStage.World);
        if (worldData == null)
        {
            return false;
        }
        if (worldData.levelsData.Length <= worldStage.Id + 1)
        {
            return false;
        }
        return true;
    }
}
