public class WorldStage {

    public int Id { get; }
    public string World { get; }
    public string Level { get; }
    public bool IsLast { get; }

    public WorldStage(int id ,string world, string level, bool last)
    {
        this.Id = id;
        this.World = world;
        this.Level = level;
        this.IsLast = last;
    }
}
