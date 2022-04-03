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
        float positionSmoothing = firstUpdate ? 0 : .0001f;
        float rotationSmoothing = firstUpdate ? 0 : .00015f;
        float adjustedDeltaTime = Time.deltaTime * Mathf.Pow(gel.maze.tilesPerMove, .33f);
        firstUpdate = false;
        float targetX = gel.coor.x - gel.maze.dimensions.x / 2f + .5f;
        float targetZ = -gel.coor.y + gel.maze.dimensions.y / 2f - .5f;
        Vector2 localPosition2 = new Vector2(transform.localPosition.x, transform.localPosition.z);
        Vector2 targetPosition2 = new Vector2(targetX, targetZ);
        localPosition2 = Util.Damp(localPosition2, targetPosition2, positionSmoothing, adjustedDeltaTime);
        float distance2 = Vector2.Distance(localPosition2, targetPosition2);
        float height = 1 - Mathf.Abs(2 * (distance2 - .5f));
        float scale = 1 + .05f * Mathf.Sin((1 - Mathf.Pow(distance2, .4f)) * 2 * Mathf.PI);
        transform.localScale = new Vector3(1, scale, 1);
        height *= .2f;
        transform.localPosition = new Vector3(localPosition2.x, height, localPosition2.y);
        if (distance2 < .1f) {
            UpdateTargetRotation();
        }
        transform.localRotation = Util.Damp(transform.localRotation, targetRotation, rotationSmoothing, adjustedDeltaTime);
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
