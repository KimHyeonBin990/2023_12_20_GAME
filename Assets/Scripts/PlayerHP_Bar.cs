using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line

public class PlayerHP_Bar : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Slider hpbar;
    public float maxHp;
    public float currenthp;

    void Update()
    {
        transform.position = player.position + new Vector3(0, 0, 0);
        hpbar.value = currenthp / maxHp;
    }
}