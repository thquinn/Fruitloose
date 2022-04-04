using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float distance;
    public float sensitivity;
    public float scrollSensitivity;
    float horizontalAngle = Mathf.PI * 2 / 3;
    float verticalAngle = Mathf.PI / 6;

    public MazeScript mazeScript;
    Vector3 lookAt = new Vector3(0, 6, 0);

    void Update() {
        if (mazeScript != null) {
            // Input.
            distance *= Mathf.Pow(scrollSensitivity, Input.mouseScrollDelta.y);
            distance = Mathf.Clamp(distance, 4, 7.5f);
            if (Input.GetMouseButton(1)) {
                horizontalAngle -= Input.GetAxis("Mouse X") * sensitivity;
                verticalAngle -= Input.GetAxis("Mouse Y") * sensitivity;
                verticalAngle = Mathf.Clamp(verticalAngle, Mathf.PI * .05f, Mathf.PI * .3f);
            }
            lookAt = Util.Damp(lookAt, new Vector3(0, -1, 0), .05f, Time.deltaTime);
        } else {
            distance = Util.Damp(distance, 6, .1f, Time.deltaTime);
            verticalAngle = Util.Damp(verticalAngle, Mathf.PI * .133f, .1f, Time.deltaTime);
            lookAt = Util.Damp(lookAt, new Vector3(0, 6, 0), .05f, Time.deltaTime);
        }

        // Set position.
        float xzDistance = distance * Mathf.Cos(verticalAngle);
        float x = Mathf.Cos(horizontalAngle) * xzDistance;
        float y = Mathf.Sin(verticalAngle) * distance;
        float z = Mathf.Sin(horizontalAngle) * xzDistance;
        transform.localPosition = new Vector3(x, y, z);
        transform.LookAt(lookAt);
    }
}
