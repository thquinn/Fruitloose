using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public Transform canvasTransform;
    public GameObject prefabTitle, prefabMaze;
    public CameraScript cameraScript;
    public MoveButtonsScript moveButtonsScript;
    public ScoreBarScript scoreBarScript;

    TitleScript titleScript;
    MazeScript mazeScript;

    void Start() {
        titleScript = Instantiate(prefabTitle, canvasTransform).GetComponent<TitleScript>();
    }

    void Update() {
        if (mazeScript == null && Input.GetMouseButtonDown(0)) {
            titleScript.Dismiss();
            StartLevel();
        }
        if (mazeScript != null && mazeScript.maze != null && mazeScript.maze.gel.coor == mazeScript.maze.exit) {
            DetachLevel();
            DestroyLevel();
            titleScript = Instantiate(prefabTitle, canvasTransform).GetComponent<TitleScript>();
        }
    }

    void StartLevel() {
        mazeScript = Instantiate(prefabMaze).GetComponent<MazeScript>();
        cameraScript.mazeScript = mazeScript;
        moveButtonsScript.mazeScript = mazeScript;
        scoreBarScript.mazeScript = mazeScript;
    }
    void DetachLevel() {
        mazeScript = null;
        moveButtonsScript.mazeScript = null;
        scoreBarScript.mazeScript = null;
        Invoke("DestroyLevel", 1);
    }
    void DestroyLevel() {
        Destroy(mazeScript.terrainScript.gameObject);
        Destroy(mazeScript.gameObject);
        cameraScript.mazeScript = null;
    }
}
