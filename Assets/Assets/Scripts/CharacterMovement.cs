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

            //Rotation: joystick droite
            /*Vector3 inputRotation = Vector3.zero;
            float inputRotationX = inputRotation.x = Input.GetAxis("Rotation_X");
            if (inputRotationX < stillValue && inputRotationX > -stillValue)
                inputRotationX = 0;
            float inputRotationZ = inputRotation.z = Input.GetAxis("Rotation_Z");
            if (inputRotationZ < stillValue && inputRotationZ > -stillValue)
                inputRotationZ = 0;
            if (inputRotationX != 0 || inputRotationZ != 0)
            {
                Vector3 LookForRotation = new Vector3(inputRotationX, 0, inputRotationZ);
                lastRotation = LookForRotation.normalized;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookForRotation), speedRotation * Time.deltaTime);
            }*/
        }
    }

    public void Dash(float dashRange)
    {
        dash = true;
        dashTime = dashRange;
    }
}
