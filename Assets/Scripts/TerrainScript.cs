using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    public GameObject prefabGrass;
    public Collider terrainCollider;
    public LayerMask layerMaskTerrain;

    public void Init(Maze maze) {
        //transform.localScale = new Vector3(maze.entities.GetLength(0) / 5f, 0, maze.entities.GetLength(1) / 5f);
        float mazeWidth = maze.entities.GetLength(0) + .4f;
        float mazeHeight = maze.entities.GetLength(1) + .4f;
        Rect mazeBounds = new Rect(-mazeWidth / 2, -mazeHeight / 2, mazeWidth, mazeHeight);
        float rayY = terrainCollider.bounds.max.y;
        int grassCount = Mathf.RoundToInt(terrainCollider.bounds.size.x * terrainCollider.bounds.size.z - mazeWidth * mazeHeight);
        for (int i = 0; i < grassCount; i++) {
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
