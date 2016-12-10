using UnityEngine;

public class Pickup : MonoBehaviour {

    [SerializeField]
    private int healthAmount;
    [SerializeField]
    private int gameTime;
    [SerializeField]
    private int invincibilityTime;

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public ItemStats GetStats()
    {
        ItemStats stats = new ItemStats()
        {
            HealthAmount = this.healthAmount,
            GameTime = this.gameTime,
            InvincibilityTime = this.invincibilityTime
        };
        return stats;
    }
}
