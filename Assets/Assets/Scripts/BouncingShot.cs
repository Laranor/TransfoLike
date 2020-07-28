using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingShot : MonoBehaviour
{
    public float damage;

    public float multiplier = 1;
    public float bounceIncrease;
    public float bounceMax;
    private float bounce;

    public PlayerStats stats;
    private void Start()
    {
        stats = PlayerManager.instance.player.GetComponent<PlayerStats>();
    }
    private void Update()
    {
        if(bounce >= bounceMax)
        {
            stats.comboValue = 1;
            stats.ResetCombo();
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeDamage", damage * multiplier);
            stats.comboValue += stats.comboIncrease;
            if (!stats.spell3)
                stats.spell3 = true;
            else
                stats.ResetCombo();
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Walls")
        {
            multiplier += bounceIncrease;
            bounce += 1;
        }
    }
}
