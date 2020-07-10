using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    PlayerStats stats;
    Animator animAvatar;
    CharacterMovement CM;

    public float dashCooldown, dashTime;
    public float dashSlashCooldown, dashSlashTime, dashSlashDamage;
    private float dashTimer;
    public Image CD4;

    public bool attack1;
    public BoxCollider attackCol1;
    public float stompRapideDamage, stompRapideCD, stompRapideCT;
    public float cleaveDamage, cleaveCD, cleaveCT;
    private float attack1Timer, attack1Casting;
    public Image CD1;

    public bool attack2;
    public CapsuleCollider attackCol2;
    public float cleave360Damage, cleave360CD, cleave360CT;
    public float tourbillonDamage, tourbillonCD, tourbillonCT;
    private float attack2Timer, attack2Casting;
    public Image CD2;

    public bool trigger;

    public GameObject projectile;
    public Transform projectileSpawn;
    public float projectileForce;
    public float attack3BaseDmg, attack3Cooldown;
    private float attack3Timer;
    public Image CD3;

    public List<GameObject> targets;
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        animAvatar = GetComponent<Animator>();
        CM = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stats.form == 0)
        {
            //Stomp Rapide
            if (attack1Casting <= stompRapideCT && attack1)
            {
                attack1Casting += Time.deltaTime;
            }
            if (attack1Casting >= stompRapideCT && attack1)
            {
                if (targets.Count > 0)
                {
                    for (int i = 0; i < targets.Count; i++)
                    {
                        targets[i].SendMessage("TakeDamage", stompRapideDamage + stats.strenght);
                        if (!stats.transformed)
                            stats.greenForm += 1;
                    }
                }
                attackCol1.enabled = false;
                attack1 = false;
                attack1Casting = 0;
                targets.Clear();
            }
            if (attack1Timer > 0)
            {
                attack1Timer -= Time.deltaTime;
                CD1.fillAmount -= 1 / stompRapideCD * Time.deltaTime;
            }
            //Cleave 360
            if (attack2Casting <= cleave360CT && attack2)
            {
                attack2Casting += Time.deltaTime;
            }
            if (attack2Casting >= cleave360CT && attack2)
            {
                if (targets.Count > 0)
                {
                    for (int i = 0; i < targets.Count; i++)
                    {
                        targets[i].SendMessage("TakeDamage", cleave360Damage + stats.strenght);
                        if (!stats.transformed)
                            stats.redForm += 1;
                    }
                }
                attackCol2.enabled = false;
                attack2 = false;
                attack2Casting = 0;
                targets.Clear();
            }
            if (attack2Timer > 0)
            {
                attack2Timer -= Time.deltaTime;
                CD2.fillAmount -= 1 / cleave360CD * Time.deltaTime;
            }
            //Tir Rapide
            if (attack3Timer > 0)
            {
                attack3Timer -= Time.deltaTime;
                CD3.fillAmount -= 1 / attack3Cooldown * Time.deltaTime;
            }
            //Dash
            if (dashTimer > 0)
            {
                dashTimer -= Time.deltaTime;
                CD4.fillAmount -= 1 / dashCooldown * Time.deltaTime;
            }
        }

        if (stats.form == 1)
        {
            //Cleave
            if (attack1Casting <= cleaveCT && attack1)
            {
                attack1Casting += Time.deltaTime;
            }
            if (attack1Casting >= cleaveCT && attack1)
            {
                if (targets.Count > 0)
                {
                    for (int i = 0; i < targets.Count; i++)
                    {
                        targets[i].SendMessage("TakeDamage", cleaveDamage + stats.strenght);
                    }
                }
                attackCol1.enabled = false;
                attack1 = false;
                attack1Casting = 0;
                targets.Clear();
            }
            if (attack1Timer > 0)
            {
                attack1Timer -= Time.deltaTime;
                CD1.fillAmount -= 1 / cleaveCD * Time.deltaTime;
            }
            //Tourbillon
            if (attack2Casting <= tourbillonCT && attack2)
            {
                attack2Casting += Time.deltaTime;
            }
            if (attack2Casting >= tourbillonCT && attack2)
            {
                if (targets.Count > 0)
                {
                    for (int i = 0; i < targets.Count; i++)
                    {
                        targets[i].SendMessage("TakeDamage", tourbillonDamage + stats.strenght);
                    }
                }
                attackCol2.enabled = false;
                attack2 = false;
                attack2Casting = 0;
                targets.Clear();
            }
            if (attack2Timer > 0)
            {
                attack2Timer -= Time.deltaTime;
                CD2.fillAmount -= 1 / tourbillonCD * Time.deltaTime;
            }
            //Dash Slash
            if (dashTimer > 0)
            {
                dashTimer -= Time.deltaTime;
                CD4.fillAmount -= 1 / dashSlashCooldown * Time.deltaTime;
            }
        }

        if (!animAvatar.GetBool("Attacking"))
        {
            if (Input.GetButtonDown("1Button"))
            {
                if (stats.form == 0)
                    StompRapide();
                if (stats.form == 1)
                    Cleave();
                if (stats.form == 2)
                    StompRapide();
                if (stats.form == 3)
                    StompRapide();
            }
            if (Input.GetButtonDown("2Button"))
            {
                if (stats.form == 0)
                    Cleave360();
                if (stats.form == 1)
                    Tourbillon();
                if (stats.form == 2)
                    Cleave360();
                if (stats.form == 3)
                    Cleave360();
            }
            if (Input.GetButtonDown("3Button"))
            {
                if (stats.form == 0)
                    TirRapide();
                if (stats.form == 1)
                    TirRapide();
                if (stats.form == 2)
                    TirRapide();
                if (stats.form == 3)
                    TirRapide();
            }
            if (Input.GetButtonDown("4Button"))
            {
                if (stats.form == 0)
                    Dash();
                if (stats.form == 1)
                    SlashDash();
                if (stats.form == 2)
                    Dash();
                if (stats.form == 3)
                    Dash();

            }
        }
    }

    //Forme de base
    void TirRapide()
    {
        if(attack3Timer <= 0)
        {
            GameObject projectileClone = Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
            projectileClone.GetComponent<Rigidbody>().AddForce(projectileSpawn.transform.forward * projectileForce);
            projectileClone.GetComponent<Projectile>().damage = attack3BaseDmg + stats.strenght;
            projectileClone.GetComponent<Projectile>().stats = stats;
            attack3Timer = attack3Cooldown;
            CD3.fillAmount = 1;
        }

    }
    void Dash()
    {
        if (dashTimer <= 0)
        {
            gameObject.layer = 12;
            CM.Dash(dashTime);
            dashTimer = dashCooldown;
            CD4.fillAmount = 1;
        }
    }

    void StompRapide()
    {
        if (attack1Timer <= 0)
        {
            attack1Casting = 0;
            attack1 = true;
            attackCol1.enabled = true;
            animAvatar.SetTrigger("Attack1");
            attack1Timer = stompRapideCD;
            CD1.fillAmount = 1;
            animAvatar.SetBool("Attacking", true);
        }
    }

    void Cleave360()
    {
        if (attack2Timer <= 0)
        {
            attack2Casting = 0;
            attack2 = true;
            attackCol2.enabled = true;
            animAvatar.SetTrigger("Attack2");
            attack2Timer = cleave360CD;
            CD2.fillAmount = 1;
            animAvatar.SetBool("Attacking", true);
        }
    }

    //Forme Berserker
    void Cleave()
    {
        if (attack1Timer <= 0)
        {
            attack1Casting = 0;
            attackCol1.enabled = true;
            animAvatar.SetTrigger("Attack1");
            attack1 = true;
            attack1Timer = cleaveCD;
            CD1.fillAmount = 1;
            animAvatar.SetBool("Attacking", true);
        }
    }
    void Tourbillon()
    {
        if (attack2Timer <= 0)
        {
            attack2Casting = 0;
            attack2 = true;
            attackCol2.enabled = true;
            animAvatar.SetTrigger("Attack2");
            attack2Timer = tourbillonCD;
            CD2.fillAmount = 1;
            animAvatar.SetBool("Attacking", true);
        }
    }
    void SlashDash()
    {
        if (dashTimer <= 0)
        {
            CM.Dash(dashSlashTime);
            dashTimer = dashSlashCooldown;
            CD4.fillAmount = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (attack1 || attack2)
        {
            if (other.gameObject.tag == "Enemy")
            {
                if (targets.Contains(other.GetComponentInParent<EnemyStats>().gameObject))
                    targets.Remove(other.GetComponentInParent<EnemyStats>().gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (attack1 || attack2)
        {
            if (other.gameObject.tag == "Enemy")
            {
                if (!targets.Contains(other.GetComponentInParent<EnemyStats>().gameObject))
                    targets.Add(other.GetComponentInParent<EnemyStats>().gameObject);
            }
        }
    }
}
