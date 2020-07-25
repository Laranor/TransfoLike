using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingShot : MonoBehaviour
{
    public float damage;

    public float multiplier = 1;
    public float bounceIncrease;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeDamage", damage * multiplier);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Walls")
        {
            multiplier += bounceIncrease;
        }
    }
}
