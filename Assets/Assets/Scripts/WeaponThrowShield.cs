using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeaponThrowShield : MonoBehaviour
{
    public float damage;
    public PlayerStats stats;
    bool disable;
    public List<GameObject> hitted;
    public float speed;

    public CapsuleCollider detectionCol;
    bool detection;
    public List<Transform> enemyInRange;
    Transform closestEnemy;
    float timer;
    private void Update()
    {
        if ((stats.gameObject.transform.position - transform.position).magnitude < 1)
        {
            stats.gameObject.GetComponent<PlayerSkills>().weaponGround = false;
            Destroy(gameObject);
        }
        if(detection)
        {
            timer += Time.deltaTime;
            detectionCol.enabled = true;
            if (enemyInRange != null && timer > 0.1f)
                closestEnemy = GetClosestEnemy(enemyInRange, this.transform);
            if(closestEnemy != null && timer > 0.1f)
            {
                detectionCol.enabled = false;
                detection = false;
                enemyInRange.Clear();
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().AddForce((closestEnemy.position - transform.position) * speed, ForceMode.VelocityChange);
                closestEnemy = null;
                timer = 0;
            }
            if (closestEnemy == null && timer > 0.1f)
            {
                if(timer > 0.4f)
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                    disable = true;
                    timer = 0;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            disable = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && !disable && !detection && !hitted.Contains(other.gameObject) && other.gameObject.name != "Body")
        {
            hitted.Add(other.gameObject);
            detection = true;
            other.gameObject.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeDamage", damage);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9 && detection && !enemyInRange.Contains(other.gameObject.transform) && !hitted.Contains(other.gameObject) && other.gameObject.name != "Body")
        {
            enemyInRange.Add(other.gameObject.transform);
        }
    }
    Transform GetClosestEnemy(List<Transform> enemies, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
}
