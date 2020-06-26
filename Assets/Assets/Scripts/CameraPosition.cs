using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform avatarPos;

    public Vector3 cameraPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = avatarPos.position + cameraPos;
    }
}
