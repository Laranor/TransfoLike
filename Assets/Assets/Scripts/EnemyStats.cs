using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    private float HP;
    public float maxHP;
    public Image HPBar;
    public Canvas canvas;
    public Transform cam;
    public bool stunned;
    private float stunnedTime;
    void Start()
    {
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.LookAt(cam);
        HPBar.fillAmount = HP / maxHP;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        if(stunned)
        {
            stunnedTime -= Time.deltaTime;
            if (stunnedTime <= 0)
                stunned = false;
        }
    }

    public void TakeDamage(float dmg)
    {
        HP -= dmg;
    }
    public void TakeStun(float duration)
    {
        stunned = true;
        stunnedTime = duration;
    }
}
