using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsScript : MonoBehaviour
{
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, Time.time * 2, 0);
    }
}
