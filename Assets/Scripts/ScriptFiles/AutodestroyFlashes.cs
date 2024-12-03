using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutodestroyFlashes : MonoBehaviour

{
    public float destroyTime = 0.3f;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
