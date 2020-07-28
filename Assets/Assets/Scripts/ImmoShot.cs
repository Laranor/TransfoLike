using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmoShot : MonoBehaviour
{
    public float damage;
    public List<GameObject> hitted;
    public float stunDuration, duration;
    float timer;
    private bool hit;
    public PlayerStats stats;
    private void Start()
    {
        stats = PlayerManager.instance.player.GetComponent<PlayerStats>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > duration)
        {
            if (hit == false)
            {
                stats.comboValue = 1;
                stats.ResetCombo();
            }
            else
            {
                stats.comboValue += stats.comboIncrease;
                if(!stats.spell1)
                    stats.spell1 = true;
                else
                    stats.ResetCombo();
            }
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && !hitted.Contains(other.gameObject) && other.gameObject.name != "Body")
        {
            hitted.Add(other.gameObject);
            other.gameObject.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeDamage", damage);
            other.gameObject.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeStun", stunDuration);
            hit = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9 && !hitted.Contains(other.gameObject) && other.gameObject.name != "Body")
        {
            hitted.Add(other.gameObject);
            other.gameObject.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeDamage", damage);
            other.gameObject.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeStun", stunDuration);
            hit = true;
        }
    }
}
