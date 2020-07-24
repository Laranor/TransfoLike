using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DashDamage : MonoBehaviour
{
    public PlayerSkills playerSkills;
    public PlayerStats playerStats;
    public CharacterMovement characterMovement;

    public bool empale;
    public GameObject empaled;
    public Transform empalePos;

    private void Update()
    {
        if (empale && empaled != null)
        {
            empaled.transform.position = new Vector3(empalePos.position.x, empaled.transform.position.y, empalePos.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(playerStats.form == 1 && characterMovement.dash)
            {
                if (playerStats.revenge)
                {
                    other.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeDamage", (playerSkills.dashSlashDamage + playerStats.strenght) * playerStats.revengeDamage);
                    playerStats.Heal(playerStats.revengeHeal);
                }
                else
                    other.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeDamage", playerSkills.dashSlashDamage + playerStats.strenght);
            }
            if (playerStats.form == 3 && characterMovement.dash)
            {
                if (playerSkills.shieldUp)
                    other.gameObject.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeStun", playerSkills.slowChargeShieldStun);
                if (!playerSkills.shieldUp)
                {
                    other.gameObject.GetComponentInParent<EnemyAI>().pushed = true;
                    other.gameObject.GetComponentInParent<NavMeshAgent>().enabled = false;
                    empale = true;
                    empaled = other.gameObject.GetComponentInParent<EnemyStats>().gameObject;
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (playerStats.form == 3 && characterMovement.dash)
            {
                if (playerSkills.shieldUp)
                    other.gameObject.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeStun", playerSkills.slowChargeShieldStun);
                if (!playerSkills.shieldUp)
                {
                    other.gameObject.GetComponentInParent<EnemyAI>().pushed = true;
                    other.gameObject.GetComponentInParent<NavMeshAgent>().enabled = false;
                    empale = true;
                    empaled = other.gameObject.GetComponentInParent<EnemyStats>().gameObject;
                }
            }
        }
    }
}
