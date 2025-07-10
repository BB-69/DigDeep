using UnityEngine;

public class GetDamage : MonoBehaviour
{
    public float Hitpoints;
    public void TakeHit(float damage)
    {
        Hitpoints -= damage;

        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
