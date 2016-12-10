public class ItemStats
{
    public ItemStats()
    {
        this.HealthAmount = 0;
        this.GameTime = 0;
        this.InvincibilityTime = 0;
    }

    public int HealthAmount { get; set; }
    public int GameTime { get; set; }
    public int InvincibilityTime { get; set; }
}
