using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    Wall wall;
    bool firstUpdate;

    public void Set(Wall wall) {
        this.wall = wall;
        if (wall.horizontal) {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        firstUpdate = true;
    }

    void Update() {
        float smoothing = firstUpdate ? 0 : .001f;
        firstUpdate = false;
        float targetX = wall.coor.x - wall.maze.dimensions.x / 2f + (wall.horizontal ? .5f : 1f);
        float targetZ = -wall.coor.y + wall.maze.dimensions.y / 2f - (wall.horizontal ? 1f : .5f);
        Vector3 targetPosition = new Vector3(targetX, 0, targetZ);
        transform.localPosition = Util.Damp(transform.localPosition, targetPosition, smoothing, Time.deltaTime);
    }
}
