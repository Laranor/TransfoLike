using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    PlayerSkills playerSkills;

    private float HP;
    public float maxHP;
    public Image HPBar;

    public int strenght;
    public int agility;
    public int intelligence;

    public float blueForm;
    public float redForm;
    public float greenForm;
    public int maxForm;
    public float transformationTime;
    private float timer;
    public int form;
    public Image green;
    public Image red;
    public Image blue;

    Color baseColor;
    public Renderer body;
    public bool transformed;

    public bool revenge;
    private float revengeCDTimer;
    public float revengeTimer, revengeDuration, revengeDamage, revengeHeal, revengeCD;
    public Image CDpassiv;
    void Start()
    {
        HP = maxHP;
        baseColor = body.material.GetColor("_Color");
        playerSkills = GetComponent<PlayerSkills>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            TakeDamage(10);
        green.fillAmount = greenForm / maxForm;
        red.fillAmount = redForm / maxForm;
        blue.fillAmount = blueForm / maxForm;
        HPBar.fillAmount = HP / maxHP;

        if (HP <= 0)
        {
            Destroy(gameObject);
        }

        if(transformed)
        {
            timer += Time.deltaTime;
            if (form == 1)
            {
                greenForm -= maxForm / transformationTime * Time.deltaTime;
                redForm = 0;
                blueForm = 0;
                CDpassiv.fillAmount = revengeCDTimer / revengeCD;
            }
            if (form == 2)
            {
                redForm -= maxForm / transformationTime * Time.deltaTime;
                greenForm = 0;
                blueForm = 0;
            }
            if (form == 3)
            {
                blueForm -= maxForm / transformationTime * Time.deltaTime;
                redForm = 0;
                greenForm = 0;
            }
            if (timer >= transformationTime)
            {
                timer = 0;
                transformed = false;
                if(form == 1)
                {
                    greenForm = 0;
                    revengeTimer = 0;
                    revenge = false;
                    revengeCDTimer = 0;
                }
                if (form == 2)
                {
                    redForm = 0;
                }
                if (form == 3)
                {
                    blueForm = 0;
                }
                body.material.SetColor("_Color", baseColor);
                form = 0;
                ResetCD();
            }

            if(revenge)
            {
                revengeTimer += Time.deltaTime;
                if(revengeTimer > revengeDuration)
                {
                    revengeTimer = 0;
                    revenge = false;
                }
            }
            if(revengeCDTimer > 0)
            {
                revengeCDTimer -= Time.deltaTime;
            }
        }
        else
        {
            CDpassiv.fillAmount = 0;
        }

        if(greenForm >= maxForm)
        {
            form = 1;
            transformed = true;
            body.material.SetColor("_Color", Color.green);
            ResetCD();
        }
        if (redForm >= maxForm)
        {
            form = 2;
            transformed = true;
            body.material.SetColor("_Color", Color.red);
            ResetCD();
        }
        if (blueForm >= maxForm)
        {
            form = 3;
            transformed = true;
            body.material.SetColor("_Color", Color.blue);
            ResetCD();
        }
    }

    void ResetCD()
    {
        playerSkills.CD1.fillAmount = 0;
        playerSkills.CD2.fillAmount = 0;
        playerSkills.CD3.fillAmount = 0;
        playerSkills.CD4.fillAmount = 0;
    }

    public void TakeDamage(float dmg)
    {
        HP -= dmg;
        //Passif berseker Revenge
        if(form == 1 && !revenge && revengeCDTimer <= 0)
        {
            revenge = true;
            revengeCDTimer = revengeCD;
        }
    }
    public void Heal (float healAmount)
    {
        HP += healAmount;
        if (HP > maxHP)
            HP = maxHP;
    }
}
