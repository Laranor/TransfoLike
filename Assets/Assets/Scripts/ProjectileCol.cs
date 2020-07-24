using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileCol : MonoBehaviour
{
    public WeaponThrowNoShield WTNS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && !WTNS.damageProc)
        {
            other.gameObject.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeDamage", WTNS.damage);
            other.gameObject.GetComponentInParent<EnemyAI>().pushed = true;
            WTNS.damageProc = false;
            other.gameObject.GetComponentInParent<NavMeshAgent>().enabled = false;
            WTNS.empale = true;
            WTNS.empaled = other.gameObject.GetComponentInParent<EnemyStats>().gameObject;
        }
    }
}
