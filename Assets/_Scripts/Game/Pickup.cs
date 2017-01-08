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
        GameObject pickParticle = SRResources.Particles.ItemObtained.Instantiate();
        pickParticle.name = "ItemObtained_Particle";
        pickParticle.transform.SetParent(this.gameObject.transform.parent, false);
        pickParticle.transform.position = this.gameObject.transform.position;

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
