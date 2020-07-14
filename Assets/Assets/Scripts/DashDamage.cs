using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDamage : MonoBehaviour
{
    public PlayerSkills playerSkills;
    public PlayerStats playerStats;
    public CharacterMovement characterMovement;

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

        }
    }
}
