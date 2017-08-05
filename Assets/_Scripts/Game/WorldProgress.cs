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
        WorldData worldData = FindWorldData(worldStage.World);
        int levelId = worldStage.Id + 1;
        bool isLast = worldData.levelsNames.Length <= levelId + 1;
        return new WorldStage(levelId,
                              worldData.name,
                              worldData.levelsNames[levelId],
                              isLast);
    }

    public string GetNextWorld(string worldName){
        int worldIndex = Array.FindIndex(Localconfig.worldsData, worldData => worldData.name == worldName);
        if(worldIndex == -1 || worldIndex >= (Localconfig.worldsData.Length - 1)){
            return null;
        }
        return Localconfig.worldsData[worldIndex+1].name;
    }

    public int GetWorldTime(string worldName){
        return FindWorldData(worldName).time;
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
        if (worldData.levelsNames.Length <= worldStage.Id + 1)
        {
            return false;
        }
        return true;
    }

}
