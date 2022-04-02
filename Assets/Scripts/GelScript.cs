using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelScript : MonoBehaviour
{
    Gel gel;
    bool firstUpdate;
    Quaternion targetRotation;

    public void Set(Gel gel) {
        this.gel = gel;
        UpdateTargetRotation();
        firstUpdate = true;
    }

    void Update() {
        float positionT = firstUpdate ? 1 : .05f;
        float rotationT = firstUpdate ? 1 : .02f;
        firstUpdate = false;
        float targetX = gel.coor.x - gel.maze.dimensions.x / 2f + .5f;
        float targetZ = -gel.coor.y + gel.maze.dimensions.y / 2f - .5f;
        Vector3 targetPosition = new Vector3(targetX, 0, targetZ);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, positionT);
        if (Vector3.Distance(transform.localPosition, targetPosition) < .05f) {
            UpdateTargetRotation();
        }
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, rotationT);
    }
    void UpdateTargetRotation() {
        if (gel.path.Count == 0) {
            return;
        }
        Int2 next = gel.path[0];
        int dx = next.x - gel.coor.x;
        int dy = next.y - gel.coor.y;
        if (dx == 1) {
            targetRotation = Quaternion.Euler(0, 90, 0);
        } else if (dy == 1) {
            targetRotation = Quaternion.Euler(0, 180, 0);
        } else if (dx == -1) {
            targetRotation = Quaternion.Euler(0, 270, 0);
        } else {
            Debug.Assert(dy == -1);
            targetRotation = Quaternion.identity;
        }
    }
}
