using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    PlayerStats stats;
    Animator animAvatar;

    public bool attack1;
    public BoxCollider attackCol1;
    public float attack1BaseDmg, attack1Cooldown;
    private float attack1Timer;
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        animAvatar = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attack1Timer > 0)
        {
            attack1Timer -= Time.deltaTime;
        }

        if(Input.GetButtonDown("1Button"))
        {
            
        }
        if (Input.GetButtonDown("2Button"))
        {
            Attack1();
        }
        if (Input.GetButtonDown("3Button"))
        {
            
        }
        if (Input.GetButtonDown("4Button"))
        {

        }
    }

    void Attack1()
    {
        if (!attack1 && attack1Timer <= 0)
        {
            attackCol1.enabled = true;
            animAvatar.SetTrigger("Attack1");
            attack1 = true;
            attack1Timer = attack1Cooldown;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (attack1)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeDamage", attack1BaseDmg + stats.strenght);
                attack1 = false;
                attackCol1.enabled = false;
                stats.greenForm += 1;
            }
            else
            {
                attack1 = false;
                attackCol1.enabled = false;
            }
        }
    }
}
