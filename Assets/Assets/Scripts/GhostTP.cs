using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTP : MonoBehaviour
{
    public bool colWall;
    public GameObject avatar;
    public float tpRange;
    void Update()
    {
        if (!colWall && transform.localPosition.z < tpRange)
            transform.localPosition += new Vector3(0, 0, 0.3f);
        if (!colWall && transform.localPosition.z >= tpRange)
            transform.localPosition = new Vector3(0, 1, tpRange);
        if (colWall)
            transform.localPosition = new Vector3(0, 1, transform.localPosition.z);
        if (colWall && transform.localPosition.z > 0)
            transform.localPosition -= new Vector3(0, 0, 0.3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 15)
        {
            colWall = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 15)
        {
            colWall = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 15)
        {
            colWall = true;
        }
    }
}
