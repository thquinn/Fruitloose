using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public GameObject meshRoot;
    public ParticleSystem particles;

    void Update()
    {
        if (meshRoot == null) {
            return;
        }
        float y = .25f + .1f * Mathf.Sin(Time.time);
        meshRoot.transform.localPosition = new Vector3(0, y, 0);
        meshRoot.transform.Rotate(0, .1f, 0);
    }

    public void Eat() {
        particles.transform.parent = transform.parent;
        particles.Play();
        Destroy(gameObject);
        Destroy(particles, 2f);
        SFXScript.instance.Chomp();
    }
}
