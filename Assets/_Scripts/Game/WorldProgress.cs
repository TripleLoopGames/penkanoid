using Localconfig = Config.WorldProgress;

public class WorldProgress {

    public WorldStage GetFirstStage(string world)
    {
        // fake for now return always first world not finished
        return new WorldStage(world, 1, false);
    }

    public WorldStage GetNextStage(WorldStage worldStage)
    {
        // fake for now return always passed world
        return new WorldStage(worldStage.world, worldStage.level, false);
    }
}
 