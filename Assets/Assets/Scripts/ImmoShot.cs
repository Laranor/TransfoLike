using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmoShot : MonoBehaviour
{
    public float damage;
    public List<GameObject> hitted;
    public float stunDuration, duration;
    float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > duration)
        {
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
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9 && !hitted.Contains(other.gameObject) && other.gameObject.name != "Body")
        {
            hitted.Add(other.gameObject);
            other.gameObject.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeDamage", damage);
            other.gameObject.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeStun", stunDuration);
        }
    }
}
