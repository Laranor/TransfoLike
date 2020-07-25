using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float HP;
    public float maxHP;
    public Image HPBar;
    public Canvas canvas;
    public Transform cam;
    public bool stunned;
    private float stunnedTime;
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        HP = maxHP;
    }

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
            agent.enabled = false;
            stunnedTime -= Time.deltaTime;
            if (stunnedTime <= 0)
            {
                stunned = false;
                agent.enabled = true;
            }
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
