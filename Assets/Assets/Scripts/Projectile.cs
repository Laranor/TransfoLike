using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public PlayerStats stats;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            if (!stats.transformed)
                stats.blueForm += 1;
            collision.gameObject.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeDamage", damage);
        }
        Destroy(gameObject);
    }
}
