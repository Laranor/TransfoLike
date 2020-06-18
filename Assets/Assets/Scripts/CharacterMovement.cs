using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool manette = true;
    public float stillValue;
    public float sensitivity;
    public float speedRotation;
    CharacterController avatar;
    void Start()
    {
        avatar = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(manette)
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
            //Rotation: joystick droite
            Vector3 inputRotation = Vector3.zero;
            float inputRotationX = inputRotation.x = Input.GetAxis("Rotation_X");
            if (inputRotationX < stillValue && inputRotationX > -stillValue)
                inputRotationX = 0;
            float inputRotationZ = inputRotation.z = Input.GetAxis("Rotation_Z");
            if (inputRotationZ < stillValue && inputRotationZ > -stillValue)
                inputRotationZ = 0;
            if (inputRotationX != 0 || inputRotationZ != 0)
            {
                Vector3 LookForRotation = new Vector3(inputRotationX, 0, inputRotationZ);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookForRotation), speedRotation * Time.deltaTime);
            }
        }
    }
}
