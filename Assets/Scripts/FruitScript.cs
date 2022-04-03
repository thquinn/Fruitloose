using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public GameObject meshRoot;
    public ParticleSystem particlesEat, particlesSparkle;
    public Material materialGoldenfruit, materialGold;
    public MeshRenderer[] materialSwapsGoldenfruit, materialSwapsGold;

    GoldenFruit goldenFruit;

    public void Set(GoldenFruit goldenFruit) {
        this.goldenFruit = goldenFruit;
        foreach (MeshRenderer materialSwapGoldenfruit in materialSwapsGoldenfruit) {
            materialSwapGoldenfruit.material = materialGoldenfruit;
        }
        foreach (MeshRenderer materialSwapGold in materialSwapsGold) {
            materialSwapGold.material = materialGold;
        }
        particlesSparkle.Play();
    }

    void Update()
    {
        if (meshRoot == null) {
            return;
        }
        float y = .25f + .1f * Mathf.Sin(Time.time + (goldenFruit == null ? 0 : Mathf.PI));
        meshRoot.transform.localPosition = new Vector3(0, y, 0);
        meshRoot.transform.localRotation = Quaternion.Euler(0, Time.time * 30 + (goldenFruit == null ? 0 : 180), 0);

        if (goldenFruit != null && goldenFruit.maze == null) {
            Eat();
        }
    }

    public void Eat() {
        particlesEat.transform.parent = transform.parent;
        particlesEat.Play();
        Destroy(gameObject);
        Destroy(particlesEat, 2f);
        SFXScript.instance.Chomp();
    }
}
