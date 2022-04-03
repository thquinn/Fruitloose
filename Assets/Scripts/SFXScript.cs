using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    public static SFXScript instance;
    public AudioSource chomp;

    void Start()
    {
        instance = this;
    }

    public void Chomp() {
        chomp.PlayOneShot(chomp.clip);
    }
}
