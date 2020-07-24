using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float stillValue;
    public float sensitivity;
    public float speedRotation;
    public CharacterController avatar;
    public Vector3 lastDirection;
    public Vector3 lastRotation;

    public bool dash;
    private float dashTime;
    float dashTimer;
    public float dashSpeed;

    PlayerStats playerStats;
    public DashDamage dashDamage;
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        avatar = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dash)
        {
            avatar.Move(lastDirection * dashSpeed);
            dashTimer += Time.deltaTime;
            if(dashTimer >= dashTime)
            {
                dashTimer = 0;
                dash = false;
                gameObject.layer = 8;
                if(playerStats.revenge)
                {
                    playerStats.revenge = false;
                    playerStats.revengeTimer = 0;
                }
                dashDamage.empale = false;
                dashDamage.empaled = null;
            }
        }
        if(!dash)
        {
            //Deplacement: joystick gauche
            Vector3 inputDirection = Vector3.zero;
            inputDirection.x = Input.GetAxis("Deplacement_X");
            if (inputDirection.x < stillValue && inputDirection.x > -stillValue)
                inputDirection.x = 0;
            inputDirection.z = Input.GetAxis("Deplacement_Z");
            if (inputDirection.z < stillValue && inputDirection.z > -stillValue)
                inputDirection.z = 0;
            avatar.Move(inputDirection * sensitivity);
            if (inputDirection != Vector3.zero)
                lastDirection = inputDirection.normalized;
            if (inputDirection.x != 0 || inputDirection.z != 0) //Rotation en fonction de la direction
            {
                Vector3 LookForRotation = new Vector3(inputDirection.x, 0, inputDirection.z);
                lastRotation = LookForRotation.normalized;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookForRotation), speedRotation * Time.deltaTime);
            }
        }
        if(transform.position.y > 0.01)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    public void Dash(float dashRange)
    {
        dash = true;
        dashTime = dashRange;
    }
}
