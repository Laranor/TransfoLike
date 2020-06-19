using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    PlayerStats stats;
    Animator animAvatar;

    public bool attack1;
    public BoxCollider attackCol1;
    public float attack1BaseDmg, attack1Cooldown;
    private float attack1Timer;
    public Image CD1;

    public bool attack2;
    public BoxCollider attackCol2;
    public float attack2BaseDmg, attack2Cooldown;
    private float attack2Timer;
    public Image CD2;

    public bool trigger;
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
            CD1.fillAmount -= 1 / attack1Cooldown * Time.deltaTime;
        }
        if (attack2Timer > 0)
        {
            attack2Timer -= Time.deltaTime;
            CD2.fillAmount -= 1 / attack2Cooldown * Time.deltaTime;
        }
        if (!animAvatar.GetBool("Attacking"))
        {
            attackCol1.enabled = false;
            attackCol2.enabled = false;
            attack1 = false;
            attack2 = false;
            if (Input.GetButtonDown("1Button"))
            {
                if (stats.form == 0)
                    Attack2();
                if (stats.form == 1)
                    Attack2();
                if (stats.form == 2)
                    Attack2();
                if (stats.form == 3)
                    Attack2();
            }
            if (Input.GetButtonDown("2Button"))
            {
                if (stats.form == 0)
                    Attack1();
                if (stats.form == 1)
                    Attack1();
                if (stats.form == 2)
                    Attack1();
                if (stats.form == 3)
                    Attack1();
            }
            if (Input.GetButtonDown("3Button"))
            {

            }
            if (Input.GetButtonDown("4Button"))
            {

            }
        }
    }

    void Attack1()
    {
        if (attack1Timer <= 0)
        {
            attackCol1.enabled = true;
            animAvatar.SetTrigger("Attack1");
            attack1 = true;
            attack1Timer = attack1Cooldown;
            CD1.fillAmount = 1;
            animAvatar.SetBool("Attacking", true);
        }
    }

    void Attack2()
    {
        if (attack2Timer <= 0)
        {
            attackCol2.enabled = true;
            animAvatar.SetTrigger("Attack2");
            attack2 = true;
            attack2Timer = attack2Cooldown;
            CD2.fillAmount = 1;
            animAvatar.SetBool("Attacking", true);
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
            /*if (other.gameObject.tag != "Enemy" && trigger || other == null)
            {
                attackCol1.enabled = false;
            }*/
        }
        if (attack2)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.GetComponentInParent<EnemyStats>().gameObject.SendMessage("TakeDamage", attack2BaseDmg + stats.strenght);
                attack2 = false;
                attackCol2.enabled = false;
                stats.blueForm += 1;
            }
            /*else
            {
                attackCol2.enabled = false;
            }*/
        }
    }
}
