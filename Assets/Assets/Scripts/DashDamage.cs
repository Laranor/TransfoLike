using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDamage : MonoBehaviour
{
    public PlayerSkills playerSkills;
    public PlayerStats playerStats;
    public CharacterMovement characterMovement;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(playerStats.form == 1 && characterMovement.dash)
                other.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeDamage", playerSkills.dashSlashDamage + playerStats.strenght);
        }
    }
}
