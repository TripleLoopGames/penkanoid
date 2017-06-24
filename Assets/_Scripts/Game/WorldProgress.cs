using System;
using Localconfig = Config.Worlds;

public class WorldProgress
{
    // TODO: fix silent crash!
    public WorldStage GetFirstStage(string worldName)
    {
        WorldData worldData = FindWorldData(worldName);        
        int levelId = 0;
        String firstLevel = worldData.levelsNames[levelId];
        bool isLast = worldData.levelsNames.Length == levelId + 1;
        return new WorldStage(levelId, worldName, firstLevel, isLast);
    }

    public WorldStage GetNextStage(WorldStage worldStage)
    {
        if (!ValidateStage(worldStage))
        {
            return null;
        }
        WorldData worldData = FindWorldData(worldStage.World, worldStage.IsLast);
        int levelId = worldStage.Id + 1;
        if (worldData.name != worldStage.World)
        {
            levelId = 0;
        }
        bool isLast = worldData.levelsNames.Length <= levelId + 1;
        return new WorldStage(levelId,
                              worldData.name,
                              worldData.levelsNames[levelId],
                              isLast);
    }

    private WorldData FindWorldData(string worldName, bool nextWorld = false)
    { 
        
        if (nextWorld)
        {
            int currentIndexWorld = Array.FindIndex(Localconfig.worldsData, worldData => worldData.name == worldName);
            return Localconfig.worldsData[currentIndexWorld+1];
        }
        return Array.Find(Localconfig.worldsData, worldData => worldData.name == worldName);
    }

    private bool ValidateStage(WorldStage worldStage)
    {
        /*if (worldStage.IsLast)
        {
            return false;
        }*/
        WorldData worldData = FindWorldData(worldStage.World, worldStage.IsLast);
        if (worldData == null)
        {
            return false;
        }
        if (worldData.name != worldStage.World)
        {
            return true;
        }
        if (worldData.levelsNames.Length <= worldStage.Id + 1)
        {
            return false;
        }
        return true;
    }
}
