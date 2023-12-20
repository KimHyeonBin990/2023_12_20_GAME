using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hpbar_Control : MonoBehaviour
{
    public List<Transform> obj;
    public List<GameObject> hp_bar;

    Camera mainCamera; // Rename the variable

    void Start()
    {
        mainCamera = Camera.main; // Use the type name
        for (int i = 0; i < obj.Count; i++)
        {
            hp_bar[i].transform.position = obj[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < obj.Count; i++)
        {
            hp_bar[i].transform.position = mainCamera.WorldToScreenPoint(obj[i].position + new Vector3(0, 1f, 0));
        }
    }
}