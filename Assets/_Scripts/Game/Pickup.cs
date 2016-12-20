using UnityEngine;

public class Pickup : MonoBehaviour {

    [SerializeField]
    private int healthAmount = 0;
    [SerializeField]
    private int gameTime = 0;
    [SerializeField]
    private int invincibilityTime = 0;

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
