using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    public GameObject prefabGrass;
    public Collider terrainCollider;
    public LayerMask layerMaskTerrain;

    public void SpawnGrass(float mazeWidth, float mazeHeight) {
        mazeWidth += .4f;
        mazeHeight += .4f;
        Rect mazeBounds = new Rect(-mazeWidth / 2, -mazeHeight / 2, mazeWidth, mazeHeight);
        float rayY = terrainCollider.bounds.max.y;
        for (int i = 0; i < 1000; i++) {
            float rayX = Random.Range(terrainCollider.bounds.min.x, terrainCollider.bounds.max.x);
            float rayZ = Random.Range(terrainCollider.bounds.min.z, terrainCollider.bounds.max.z);
            if (mazeBounds.Contains(new Vector2(rayX, rayZ))) {
                continue;
            }
            Vector3 rayOrigin = new Vector3(rayX, rayY, rayZ);
            Ray ray = new Ray(rayOrigin, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMaskTerrain)) {
                Instantiate(prefabGrass, transform);
                prefabGrass.transform.position = hit.point;
                prefabGrass.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                prefabGrass.transform.Rotate(Vector3.up, Random.Range(0, 360f), Space.Self);
            }
        }
    }
}
