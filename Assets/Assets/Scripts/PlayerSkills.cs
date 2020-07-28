using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    PlayerStats stats;
    Animator animAvatar;
    CharacterMovement CM;

    public bool tpAttack;
    public CapsuleCollider tpCol;
    public Transform ghostPos;
    public float dashCooldown, dashTime;
    public float dashSlashCooldown, dashSlashTime, dashSlashDamage;
    public float slowChargeNoShieldCD, slowChargeNoShieldTime, slowChargeNoShieldDamage;
    public float slowChargeShieldCD, slowChargeShieldTime, slowChargeShieldStun;
    public float teleportationCD, teleportationRange, teleportationDamage, teleportationCT;
    private float dashTimer, dashTimerShield, tpCasting;
    public Image CD4;

    public bool attack1;
    BoxCollider attackCol1;
    public GameObject ImmoShotProjectile;
    public BoxCollider stompRapideCol, cleaveCol, stompShieldCol, stompNoShieldCol;
    public float stompRapideDamage, stompRapideCD, stompRapideCT;
    public float cleaveDamage, cleaveCD, cleaveCT;
    public float stompShieldDamage, stompShieldCD, stompShieldCT, stompShieldStun;
    public float stompNoShieldDamage, stompNoShieldCD, stompNoShieldCT;
    public float ImmoShotDamage, ImmoShotCD, ImmoShotCT, ImmoShotStun;
    private float attack1Timer, attack1TimerShield, attack1Casting;
    public Image CD1;

    public bool attack2;
    CapsuleCollider attackCol2;
    public CapsuleCollider cleave360Col, tourbillonCol, tripleProcCol;
    public float cleave360Damage, cleave360CD, cleave360CT;
    public float tourbillonDamage, tourbillonCD, tourbillonCT;
    public float shieldValue, shieldMax, shieldCD, shieldCharge;
    public float tripleProcDamage, tripleProcCD, tripleProcCT, procNum;
    public bool shieldUp;
    private float attack2Timer, attack2Casting;
    public Image CD2, shieldBar;
    public GameObject shield;

    public bool trigger;

    public bool attack3;
    public BoxCollider attackCol3;
    public GameObject tirRapideProjectile, weaponThrowShield, weaponThrowNoShield, bouncingShotProjectile;
    public Transform projectileSpawn;
    public float projectileForce, bouncingShotProjectileForce, weaponThrowShieldProjectileForce, weaponThrowNoShieldProjectileForce;
    public float tirRapideDamage, tirRapideCD, tirRapideCT;
    public float boomDamage, boomCD, boomCT, boomReducCD;
    public float weaponThrowShieldDamage, weaponThrowShieldCD, weaponThrowShieldCT;
    public float weaponThrowNoShieldDamage, weaponThrowNoShieldCD, weaponThrowNoShieldCT, weaponThrowNoShieldStun;
    public float bouncingShotDamage, bouncingShotCD, bouncingShotCT;
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
                        targets[i].SendMessage("TakeDamage", stompRapideDamage);
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
                        targets[i].SendMessage("TakeDamage", cleave360Damage);
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
                            targets[i].SendMessage("TakeDamage", cleaveDamage * stats.revengeDamage);
                            stats.Heal(stats.revengeHeal);
                        }
                        else
                            targets[i].SendMessage("TakeDamage", cleaveDamage);
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
                            targets[i].SendMessage("TakeDamage", tourbillonDamage * stats.revengeDamage);
                            stats.Heal(stats.revengeHeal);
                        }
                        else
                            targets[i].SendMessage("TakeDamage", tourbillonDamage);
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
                            targets[i].SendMessage("TakeDamage", boomDamage * stats.revengeDamage);
                            stats.Heal(stats.revengeHeal);
                        }
                        targets[i].SendMessage("TakeDamage", boomDamage);
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
            //Immo Shot
            if (attack1Timer > 0)
            {
                attack1Timer -= Time.deltaTime;
                CD1.fillAmount = attack1Timer / ImmoShotCD;
            }
            else
                CD1.fillAmount = 0;
            //TripleProc
            if (attack2Casting <= tripleProcCT && attack2)
            {
                attack2Casting += Time.deltaTime;
            }
            if (attack2Casting >= tripleProcCT && attack2)
            {
                if (targets.Count > 0)
                {
                    for (int i = 0; i < targets.Count; i++)
                    {
                        targets[i].SendMessage("TakeDamage", tripleProcDamage * stats.comboValue);
                    }
                    if(procNum == 2)
                    {
                        stats.comboValue += stats.comboIncrease;
                        if (!stats.spell2)
                            stats.spell2 = true;
                        else
                            stats.ResetCombo();
                    }
                }
                if (targets.Count <= 0 && procNum == 2)
                {
                    stats.comboValue = 1;
                    stats.ResetCombo();
                }
                attackCol2.enabled = false;
                attack2 = false;
                attack2Casting = 0;
                targets.Clear();
                if (procNum < 2)
                {
                    procNum += 1;
                    attack2Casting = 0;
                    attack2 = true;
                    attackCol2.enabled = true;
                    animAvatar.SetTrigger("Attack2");
                    animAvatar.SetBool("Attacking", true);
                }
                else
                    procNum = 0;
            }
            if (attack2Timer > 0)
            {
                attack2Timer -= Time.deltaTime;
                CD2.fillAmount = attack2Timer / tripleProcCD;
            }
            //Bouncing Shot
            if (attack3Timer > 0)
            {
                attack3Timer -= Time.deltaTime;
                CD3.fillAmount = attack3Timer / bouncingShotCD;
            }
            else
                CD3.fillAmount = 0;
            //Teleportation
            if (tpCasting <= teleportationCT && tpAttack)
            {
                tpCasting += Time.deltaTime;
            }
            if (tpCasting >= teleportationCT && tpAttack)
            {
                if (targets.Count > 0)
                {
                    for (int i = 0; i < targets.Count; i++)
                    {
                        targets[i].SendMessage("TakeDamage", teleportationDamage * stats.comboValue);
                    }
                    stats.comboValue += stats.comboIncrease;
                    if (!stats.spell4)
                        stats.spell4 = true;
                    else
                        stats.ResetCombo();
                }
                else
                {
                    stats.comboValue = 1;
                    stats.ResetCombo();
                }
                tpCol.enabled = false;
                tpAttack = false;
                tpCasting = 0;
                targets.Clear();
            }
            if (dashTimer > 0)
            {
                dashTimer -= Time.deltaTime;
                CD4.fillAmount = dashTimer / teleportationCD;
            }
            else
                CD4.fillAmount = 0;
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
                            targets[i].SendMessage("TakeDamage", stompShieldDamage);
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
                            targets[i].SendMessage("TakeDamage", stompNoShieldDamage);
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
            projectileClone.GetComponent<Rigidbody>().AddForce(projectileSpawn.transform.forward * projectileForce, ForceMode.VelocityChange);
            projectileClone.GetComponent<Projectile>().damage = tirRapideDamage;
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
            GameObject projectileClone = Instantiate(ImmoShotProjectile, projectileSpawn.position, transform.rotation);
            projectileClone.GetComponent<ImmoShot>().damage = ImmoShotDamage * stats.comboValue;
            projectileClone.GetComponent<ImmoShot>().stunDuration = ImmoShotStun;
            attack1Timer = ImmoShotCD;
            CD1.fillAmount = 1;
        }
    }
    void TripleProc()
    {
        if (attack2Timer <= 0)
        {
            attack2Casting = 0;
            attack2 = true;
            attackCol2.enabled = true;
            animAvatar.SetTrigger("Attack2");
            attack2Timer = tripleProcCD;
            CD2.fillAmount = 1;
            animAvatar.SetBool("Attacking", true);
        }
    }
    void Teleportation()
    {
        if (dashTimer <= 0)
        {
            CM.avatar.enabled = false;
            transform.position = ghostPos.position;
            CM.avatar.enabled = true;
            dashTimer = teleportationCD;
            tpCasting = 0;
            tpAttack = true;
            tpCol.enabled = true;
            animAvatar.SetTrigger("Attack2");
            animAvatar.SetBool("Attacking", true);
            CD4.fillAmount = 1;
        }
    }
    void BoucingShot()
    {
        if (attack3Timer <= 0)
        {
            GameObject projectileClone = Instantiate(bouncingShotProjectile, projectileSpawn.position, Quaternion.identity);
            projectileClone.GetComponent<Rigidbody>().AddForce(projectileSpawn.transform.forward * bouncingShotProjectileForce, ForceMode.VelocityChange);
            projectileClone.GetComponent<BouncingShot>().damage = bouncingShotDamage * stats.comboValue;
            attack3Timer = bouncingShotCD;
            CD3.fillAmount = 1;
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
                projectileClone.GetComponent<Rigidbody>().AddForce(projectileSpawn.transform.forward * weaponThrowShieldProjectileForce, ForceMode.VelocityChange);
                projectileClone.GetComponent<WeaponThrowShield>().damage = weaponThrowShieldDamage;
                projectileClone.GetComponent<WeaponThrowShield>().stats = stats;
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
                GameObject projectileClone = Instantiate(weaponThrowNoShield, projectileSpawn.position, transform.rotation);
                projectileClone.GetComponent<Rigidbody>().AddForce(projectileSpawn.transform.forward * weaponThrowNoShieldProjectileForce, ForceMode.VelocityChange);
                projectileClone.GetComponent<WeaponThrowNoShield>().damage = weaponThrowNoShieldDamage;
                projectileClone.GetComponent<WeaponThrowNoShield>().stunDuration = weaponThrowNoShieldStun;
                attack3Timer = weaponThrowNoShieldCD;
                CD3.fillAmount = 1;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (attack1 || attack2 || attack3 || tpAttack)
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
        if (attack1 || attack2 || attack3 || tpAttack)
        {
            if (other.gameObject.tag == "Enemy")
            {
                if (!targets.Contains(other.GetComponentInParent<EnemyStats>().gameObject))
                    targets.Add(other.GetComponentInParent<EnemyStats>().gameObject);
            }
        }
    }
}
