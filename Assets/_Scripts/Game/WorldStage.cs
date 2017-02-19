public class WorldStage {

    public string world;
    public int level;
    public bool isLast;

    public WorldStage(string world, int level, bool last)
    {
        this.world = world;
        this.level = level;
        this.isLast = last;
    }
}
