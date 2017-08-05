public class WorldData {

    public string name;
    public string[] levelsNames;

    public int time;

    public WorldData(string name, int time, string[] levelsNames)
    {
        this.name = name;
        this.levelsNames = levelsNames;
        this.time = time;
    }
}
