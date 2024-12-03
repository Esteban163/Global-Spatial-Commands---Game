using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public GameObject Garo;
       
    void Update()
    {
        Vector3 position = transform.position;
        position.x = Garo.transform.position.x;
        position.y = Garo.transform.position.y;
        transform.position = position;
    }
}
