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
    public float slowChargeNoShieldCD, slowChargeNoShieldTime, slowChargeNoShieldDamage;
    public float slowChargeShieldCD, slowChargeShieldTime, slowChargeShieldStun;
    private float dashTimer, dashTimerShield;
    public Image CD4;

    public bool attack1;
    BoxCollider attackCol1;
    public BoxCollider stompRapideCol, cleaveCol, stompShieldCol, stompNoShieldCol;
    public float stompRapideDamage, stompRapideCD, stompRapideCT;
    public float cleaveDamage, cleaveCD, cleaveCT;
    public float stompShieldDamage, stompShieldCD, stompShieldCT, stompShieldStun;
    public float stompNoShieldDamage, stompNoShieldCD, stompNoShieldCT;
    private float attack1Timer, attack1TimerShield, attack1Casting;
    public Image CD1;

    public bool attack2;
    CapsuleCollider attackCol2;
    public CapsuleCollider cleave360Col, tourbillonCol, tripleProcCol;
    public float cleave360Damage, cleave360CD, cleave360CT;
    public float tourbillonDamage, tourbillonCD, tourbillonCT;
    public float shieldValue, shieldMax, shieldCD, shieldCharge;
    public bool shieldUp;
    private float attack2Timer, attack2Casting;
    public Image CD2, shieldBar;
    public GameObject shield;

    public bool trigger;

    public bool attack3;
    public BoxCollider attackCol3;
    public GameObject tirRapideProjectile, weaponThrowShield, weaponThrowNoShield;
    public Transform projectileSpawn;
    public float projectileForce, weaponThrowShieldProjectileForce, weaponThrowNoShieldProjectileForce;
    public float tirRapideDamage, tirRapideCD, tirRapideCT;
    public float boomDamage, boomCD, boomCT, boomReducCD;
    public float weaponThrowShieldDamage, weaponThrowShieldCD, weaponThrowShieldCT;
    public float weaponThrowNoShieldDamage, weaponThrowNoShieldCD, weaponThrowNoShieldCT, weaponThrowNoShieldStun;
    public bool weaponGround;
    private float attack3Timer, attack3TimerShield, attack3Casting;
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
            attackCol1 = stompRapideCol;
            attackCol2 = cleave360Col;
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
                CD1.fillAmount = attack1Timer / stompRapideCD;
            }
            else
                CD1.fillAmount = 0;
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
                CD2.fillAmount = attack2Timer / cleave360CD;
            }
            else
                CD2.fillAmount = 0;
            //Tir Rapide
            if (attack3Timer > 0)
            {
                attack3Timer -= Time.deltaTime;
                CD3.fillAmount = attack3Timer / tirRapideCD;
            }
            else
                CD3.fillAmount = 0;
            //Dash
            if (dashTimer > 0)
            {
                dashTimer -= Time.deltaTime;
                CD4.fillAmount = dashTimer / dashCooldown;
            }
            else
                CD4.fillAmount = 0;
        }

        if (stats.form == 1)
        {
            attackCol1 = cleaveCol; ;
            attackCol2 = tourbillonCol;
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
                        if (stats.revenge)
                        {
                            targets[i].SendMessage("TakeDamage", (cleaveDamage + stats.strenght) * stats.revengeDamage);
                            stats.Heal(stats.revengeHeal);
                        }
                        else
                            targets[i].SendMessage("TakeDamage", cleaveDamage + stats.strenght);
                        if (attack3Timer > 0)
                        {
                            attack3Timer -= boomReducCD;
                        }
                        if (attack3Timer < 0)
                            attack3Timer = 0;

                    }
                }
                if(stats.revenge)
                {
                    stats.revenge = false;
                    stats.revengeTimer = 0;
                }
                attackCol1.enabled = false;
                attack1 = false;
                attack1Casting = 0;
                targets.Clear();
            }
            if (attack1Timer > 0)
            {
                attack1Timer -= Time.deltaTime;
                CD1.fillAmount = attack1Timer / cleaveCD;
            }
            else
                CD1.fillAmount = 0;
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
                        if (stats.revenge)
                        {
                            targets[i].SendMessage("TakeDamage", (tourbillonDamage * stats.revengeDamage) + stats.strenght );
                            stats.Heal(stats.revengeHeal);
                        }
                        else
                            targets[i].SendMessage("TakeDamage", tourbillonDamage + stats.strenght);
                    }
                }
                if (stats.revenge)
                {
                    stats.revenge = false;
                    stats.revengeTimer = 0;
                }
                attackCol2.enabled = false;
                attack2 = false;
                attack2Casting = 0;
                targets.Clear();
            }
            if (attack2Timer > 0)
            {
                attack2Timer -= Time.deltaTime;
                CD2.fillAmount = attack2Timer / tourbillonCD;
            }
            else
                CD2.fillAmount = 0;
            //Dash Slash
            if (dashTimer > 0)
            {
                dashTimer -= Time.deltaTime;
                CD4.fillAmount = dashTimer / dashSlashCooldown;
            }
            else
                CD4.fillAmount = 0;
            //Boom
            if (attack3Casting <= boomCT && attack3)
            {
                attack3Casting += Time.deltaTime;
            }
            if (attack3Casting >= boomCT && attack3)
            {
                if (targets.Count > 0)
                {
                    for (int i = 0; i < targets.Count; i++)
                    {
                        if (stats.revenge)
                        {
                            targets[i].SendMessage("TakeDamage", (boomDamage + stats.strenght) * stats.revengeDamage);
                            stats.Heal(stats.revengeHeal);
                        }
                        targets[i].SendMessage("TakeDamage", boomDamage + stats.strenght);
                    }
                }
                if (stats.revenge)
                {
                    stats.revenge = false;
                    stats.revengeTimer = 0;
                }
                attackCol3.enabled = false;
                attack3 = false;
                attack3Casting = 0;
                targets.Clear();
            }
            if (attack3Timer > 0)
            {
                attack3Timer -= Time.deltaTime;
                CD3.fillAmount = attack3Timer / boomCD;
            }
            else
                CD3.fillAmount = 0;
        }

        if (stats.form == 2)
        {
            attackCol2 = tripleProcCol;
        }

        if (stats.form == 3)
        {
            shield.SetActive(true);
            
            shieldBar.fillAmount = shieldValue / shieldMax;
            //Spell with shield
            if (shieldUp)
            {
                shield.transform.position = new Vector3(shield.transform.position.x, 110, shield.transform.position.z);
                attackCol1 = stompShieldCol;
                //Stomp
                if (attack1Casting <= stompShieldCT && attack1)
                {
                    attack1Casting += Time.deltaTime;
                }
                if (attack1Casting >= stompShieldCT && attack1)
                {
                    if (targets.Count > 0)
                    {
                        for (int i = 0; i < targets.Count; i++)
                        {
                            targets[i].SendMessage("TakeDamage", stompShieldDamage + stats.strenght);
                            targets[i].SendMessage("TakeStun", stompShieldStun);
                            if (!stats.transformed)
                                stats.greenForm += 1;
                        }
                    }
                    attackCol1.enabled = false;
                    attack1 = false;
                    attack1Casting = 0;
                    targets.Clear();
                }
                if (attack1TimerShield > 0)
                {
                    CD1.fillAmount = attack1TimerShield / stompShieldCD;
                }
                else
                    CD1.fillAmount = 0;
                //Weapon Throw
                if (attack3TimerShield > 0)
                {
                    CD3.fillAmount = attack3TimerShield / weaponThrowShieldCD;
                }
                else
                    CD3.fillAmount = 0;
                //Slow Charge
                if (dashTimerShield > 0)
                {
                    CD4.fillAmount = dashTimerShield / slowChargeShieldCD;
                }
                else
                    CD4.fillAmount = 0;
            }
            //Spell without shield
            else
            {
                shield.transform.position = new Vector3(shield.transform.position.x, 90, shield.transform.position.z);
                attackCol1 = stompNoShieldCol;
                if (shieldValue < shieldMax && !weaponGround)
                    shieldValue += shieldCharge * Time.deltaTime;
                //Stomp
                if (attack1Casting <= stompNoShieldCT && attack1)
                {
                    attack1Casting += Time.deltaTime;
                }
                if (attack1Casting >= stompNoShieldCT && attack1)
                {
                    if (targets.Count > 0)
                    {
                        for (int i = 0; i < targets.Count; i++)
                        {
                            targets[i].SendMessage("TakeDamage", stompNoShieldDamage + stats.strenght);
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
                    CD1.fillAmount = attack1Timer / stompNoShieldCD;
                }
                else
                    CD1.fillAmount = 0;
                //Weapon Throw
                if (attack3Timer > 0)
                {
                    CD3.fillAmount = attack3Timer / weaponThrowNoShieldCD;
                }
                else
                    CD3.fillAmount = 0;
                //Slow Charge
                if (dashTimer > 0)
                {
                    CD4.fillAmount = dashTimer / slowChargeNoShieldCD;
                }
                else
                    CD4.fillAmount = 0;
            }
            if (attack1TimerShield > 0)
            {
                attack1TimerShield -= Time.deltaTime;
            }
            if (attack3TimerShield > 0)
            {
                attack3TimerShield -= Time.deltaTime;
            }
            if (dashTimerShield > 0)
            {
                dashTimerShield -= Time.deltaTime;
            }
            if (attack1Timer > 0)
            {
                attack1Timer -= Time.deltaTime;
            }
            if (attack3Timer > 0)
            {
                attack3Timer -= Time.deltaTime;
            }
            if (dashTimer > 0)
            {
                dashTimer -= Time.deltaTime;
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
                    ImmoShot();
                if (stats.form == 3)
                    Stomp();
            }
            if (Input.GetButtonDown("2Button"))
            {
                if (stats.form == 0)
                    Cleave360();
                if (stats.form == 1)
                    Tourbillon();
                if (stats.form == 2)
                    TripleProc();
                if (stats.form == 3)
                    Shield();
            }
            if (Input.GetButtonDown("3Button"))
            {
                if (stats.form == 0)
                    TirRapide();
                if (stats.form == 1)
                    Boom();
                if (stats.form == 2)
                    BoucingShot();
                if (stats.form == 3)
                    WeaponThrow();
            }
            if (Input.GetButtonDown("4Button"))
            {
                if (stats.form == 0)
                    Dash();
                if (stats.form == 1)
                    SlashDash();
                if (stats.form == 2)
                    Teleportation();
                if (stats.form == 3)
                    SlowCharge();

            }
        }
    }

    //Forme de base
    void TirRapide()
    {
        if(attack3Timer <= 0)
        {
            GameObject projectileClone = Instantiate(tirRapideProjectile, projectileSpawn.position, Quaternion.identity);
            projectileClone.GetComponent<Rigidbody>().AddForce(projectileSpawn.transform.forward * projectileForce);
            projectileClone.GetComponent<Projectile>().damage = tirRapideDamage + stats.strenght;
            projectileClone.GetComponent<Projectile>().stats = stats;
            attack3Timer = tirRapideCD;
            CD3.fillAmount = 1;
        }

    }
    void Dash()
    {
        if (dashTimer <= 0)
        {
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
            gameObject.layer = 12;
            CM.Dash(dashSlashTime);
            dashTimer = dashSlashCooldown;
            CD4.fillAmount = 1;
        }
    }
    void Boom()
    {
        if (attack3Timer <= 0)
        {
            attack3Casting = 0;
            attack3 = true;
            attackCol3.enabled = true;
            animAvatar.SetTrigger("Attack3");
            attack3Timer = boomCD;
            CD3.fillAmount = 1;
            animAvatar.SetBool("Attacking", true);
        }
    }

    //Forme Caster
    void ImmoShot()
    {
        if (attack1Timer <= 0)
        {

        }
    }
    void TripleProc()
    {
        if (attack2Timer <= 0)
        {

        }
    }
    void Teleportation()
    {
        if (dashTimer <= 0)
        {

        }
    }
    void BoucingShot()
    {
        if (attack3Timer <= 0)
        {

        }
    }

    //Forme Tank
    void Stomp()
    {
        if(shieldUp)
        {
            if (attack1TimerShield <= 0)
            {
                attack1Casting = 0;
                attack1 = true;
                attackCol1.enabled = true;
                animAvatar.SetTrigger("Attack1");
                attack1TimerShield = stompShieldCD;
                CD1.fillAmount = 1;
                animAvatar.SetBool("Attacking", true);
            }
        }
        else
        {
            if (attack1Timer <= 0)
            {
                attack1Casting = 0;
                attack1 = true;
                attackCol1.enabled = true;
                animAvatar.SetTrigger("Attack1");
                attack1Timer = stompNoShieldCD;
                CD1.fillAmount = 1;
                animAvatar.SetBool("Attacking", true);
            }
        }
    }
    void Shield()
    {
        if (shieldUp)
        {
            shieldUp = false;
        }
        else
        {
            if(shieldValue > 0 && !weaponGround)
                shieldUp = true;
        }
    }
    void SlowCharge()
    {
        if (shieldUp)
        {
            if (dashTimerShield <= 0)
            {
                CM.Dash(slowChargeShieldTime);
                dashTimerShield = slowChargeShieldCD;
                CD4.fillAmount = 1;
            }
        }
        else
        {
            if (dashTimer <= 0)
            {
                CM.Dash(slowChargeNoShieldTime);
                dashTimer = slowChargeNoShieldCD;
                CD4.fillAmount = 1;
            }
        }
    }
    void WeaponThrow()
    {
        if (shieldUp)
        {
            if (attack3TimerShield <= 0)
            {
                GameObject projectileClone = Instantiate(weaponThrowShield, projectileSpawn.position, Quaternion.identity);
                projectileClone.GetComponent<Rigidbody>().AddForce(projectileSpawn.transform.forward * weaponThrowShieldProjectileForce);
                projectileClone.GetComponent<Projectile>().damage = weaponThrowShieldDamage + stats.strenght;
                projectileClone.GetComponent<Projectile>().stats = stats;
                attack3TimerShield = weaponThrowShieldCD;
                CD3.fillAmount = 1;
                weaponGround = true;
                shieldUp = false;
            }
        }
        else
        {
            if (attack3Timer <= 0)
            {
                GameObject projectileClone = Instantiate(weaponThrowShield, projectileSpawn.position, Quaternion.identity);
                projectileClone.GetComponent<Rigidbody>().AddForce(projectileSpawn.transform.forward * weaponThrowNoShieldProjectileForce);
                projectileClone.GetComponent<Projectile>().damage = weaponThrowNoShieldDamage + stats.strenght;
                projectileClone.GetComponent<Projectile>().stats = stats;
                attack3Timer = weaponThrowNoShieldCD;
                CD3.fillAmount = 1;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (attack1 || attack2 || attack3)
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
        if (attack1 || attack2 || attack3)
        {
            if (other.gameObject.tag == "Enemy")
            {
                if (!targets.Contains(other.GetComponentInParent<EnemyStats>().gameObject))
                    targets.Add(other.GetComponentInParent<EnemyStats>().gameObject);
            }
        }
    }
}
