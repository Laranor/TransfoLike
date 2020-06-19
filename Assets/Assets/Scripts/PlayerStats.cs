using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float HP;

    public int strenght;
    public int agility;
    public int intelligence;

    public float blueForm;
    public float redForm;
    public float greenForm;
    public int maxForm;
    public int form;
    public Image green;
    public Image red;
    public Image blue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        green.fillAmount = greenForm / maxForm;
        red.fillAmount = redForm / maxForm;
        blue.fillAmount = blueForm / maxForm;
    }
}
