using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float lookRadius;
    public bool pushed;

    Transform target;
    public NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        if (transform.position.y > 0.085)
            transform.position = new Vector3(transform.position.x, 0.083f, transform.position.z);
        if(agent.isActiveAndEnabled && !pushed)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius)
            {
                transform.LookAt(target);
                gameObject.transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance)
                {
                    //attack
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Walls" && pushed)
        {
            PushedOnWall();
            if (target.gameObject.GetComponentInParent<CharacterMovement>().dash)
            {
                target.gameObject.GetComponentInParent<CharacterMovement>().dashTimer = target.gameObject.GetComponentInParent<CharacterMovement>().dashTime;
                GetComponent<EnemyStats>().gameObject.SendMessage("TakeDamage", target.gameObject.GetComponentInParent<PlayerSkills>().slowChargeNoShieldDamage);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Walls" && pushed)
        {
            PushedOnWall();
            if (target.gameObject.GetComponentInParent<CharacterMovement>().dash)
            {
                target.gameObject.GetComponentInParent<CharacterMovement>().dashTimer = target.gameObject.GetComponentInParent<CharacterMovement>().dashTime;
                GetComponent<EnemyStats>().gameObject.SendMessage("TakeDamage", target.gameObject.GetComponentInParent<PlayerSkills>().slowChargeNoShieldDamage);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Walls" && pushed)
        {
            PushedOnWall();
            if (target.gameObject.GetComponentInParent<CharacterMovement>().dash)
                GetComponent<EnemyStats>().gameObject.SendMessage("TakeDamage", target.gameObject.GetComponentInParent<PlayerSkills>().slowChargeNoShieldDamage);
        }
    }
    public void PushedOnWall()
    {
        pushed = false;
        agent.enabled = true;
    }
}
