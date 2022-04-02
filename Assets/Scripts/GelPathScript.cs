using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GelPathScript : MonoBehaviour
{
    public GameObject prefabPathSegment;
    public Sprite pathStart, pathStraight, pathTurn, pathEnd;

    Gel gel;
    string lastPathString = "";
    List<GameObject> pathSegments = new List<GameObject>();

    public void Set(Gel gel) {
        this.gel = gel;
    }

    void Update() {
        if (gel == null) {
            return;
        }
        string pathString = string.Join(",", gel.path.Select(c => c.x + "," + c.y));
        if (pathString == lastPathString) {
            return;
        }
        lastPathString = pathString;
        foreach (GameObject pathSegment in pathSegments) {
            Destroy(pathSegment);
        }
        pathSegments.Clear();
        if (gel.path.Count == 0) {
            return;
        }
        List<Int2> pathWithStart = new List<Int2>(gel.path);
        pathWithStart.Insert(0, gel.coor);
        int moves = Mathf.Min(gel.maze.tilesLeftThisMove, gel.path.Count);
        MakeSegments(pathWithStart.GetRange(0, moves + 1), 1);
        MakeSegments(pathWithStart.GetRange(moves, pathWithStart.Count - moves), .5f);
    }
    void MakeSegments(List<Int2> path, float alphaMultiplier) {
        if (path.Count == 1) {
            return;
        }
        float xOffset = -gel.maze.dimensions.x / 2f + .5f;
        float yOffset = -gel.maze.dimensions.y / 2f + .5f;
        for (int i = 0; i < path.Count; i++) {
            Int2 coor = path[i];
            GameObject pathSegment = Instantiate(prefabPathSegment, transform);
            pathSegment.transform.localPosition = new Vector3(coor.x + xOffset, 0, -coor.y - xOffset);
            SpriteRenderer spriteRenderer = pathSegment.GetComponentInChildren<SpriteRenderer>();
            Color c = spriteRenderer.color;
            c.a *= alphaMultiplier;
            spriteRenderer.color = c;
            // Sprite selection and rotation.
            Int2 delta1 = i > 0 ? coor - path[i - 1] : new Int2(0, 0);
            Int2 delta2 = i < path.Count - 1 ? path[i + 1] - coor : new Int2(0, 0);
            float yRotation = 0;
            if (i == 0) { // Path start.
                spriteRenderer.sprite = pathStart;
                if (delta2.x == 1) {
                    yRotation = 90;
                } else if (delta2.y == 1) {
                    yRotation = 180;
                } else if (delta2.x == -1) {
                    yRotation = 270;
                }
            } else if (i == path.Count - 1) { // Path end.
                spriteRenderer.sprite = pathEnd;
                if (delta1.x == 1) {
                    yRotation = 90;
                } else if (delta1.y == 1) {
                    yRotation = 180;
                } else if (delta1.x == -1) {
                    yRotation = 270;
                }
            } else if (delta1 == delta2) { // Path straight.
                if (delta1.y == 0) {
                    yRotation = 90;
                }
            } else { // Path turn.
                spriteRenderer.sprite = pathTurn;
                Int2 dDelta = delta2 - delta1;
                if (dDelta.x < 0 && dDelta.y < 0) {
                    yRotation = 90;
                } else if (dDelta.x < 0 && dDelta.y > 0) {
                    yRotation = 0;
                } else if (dDelta.x > 0 && dDelta.y < 0) {
                    yRotation = 180;
                } else {
                    yRotation = 270;
                }
            }
            pathSegment.transform.localRotation = Quaternion.Euler(0, yRotation, 0);
            pathSegments.Add(pathSegment);
        }
    }
}
