using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeaponThrowNoShield : MonoBehaviour
{
    public float damage;
    public float stunDuration;
    public bool damageProc;
    public bool empale;
    public GameObject empaled;
    public Transform empalePos;

    private void Update()
    {
        if(empale && empaled != null)
        {
            empaled.transform.position = new Vector3(empalePos.position.x, empaled.transform.position.y, empalePos.position.z);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Walls")
        {
            if(empaled != null)
            {
                empaled.SendMessage("PushedOnWall");
                empaled.SendMessage("TakeStun", stunDuration);
            }
            Destroy(gameObject);
        }
    }
}
