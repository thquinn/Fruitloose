using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelScript : MonoBehaviour {
    Gel gel;
    bool firstUpdate, doneAnimating;
    Quaternion targetRotation;

    public void Set(Gel gel) {
        this.gel = gel;
        UpdateTargetRotation();
        firstUpdate = true;
        doneAnimating = true;
    }

    void Update() {
        float positionT = firstUpdate ? 1 : .03f;
        float rotationT = firstUpdate ? 1 : .05f;
        firstUpdate = false;
        float targetX = gel.coor.x - gel.maze.dimensions.x / 2f + .5f;
        float targetZ = -gel.coor.y + gel.maze.dimensions.y / 2f - .5f;
        Vector2 localPosition2 = new Vector2(transform.localPosition.x, transform.localPosition.z);
        Vector3 targetPosition2 = new Vector2(targetX, targetZ);
        localPosition2 = Vector2.Lerp(localPosition2, targetPosition2, positionT);
        float distance2 = Vector2.Distance(localPosition2, targetPosition2);
        float height = 1 - Mathf.Abs(2 * (distance2 - .5f));
        height *= .2f;
        transform.localPosition = new Vector3(localPosition2.x, height, localPosition2.y);
        if (distance2 < .1f) {
            UpdateTargetRotation();
        }
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, rotationT);
        doneAnimating = distance2 < .01f && Quaternion.Angle(transform.localRotation, targetRotation) < 5;
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

    public bool DoneAnimating() {
        return doneAnimating;
    }
}
