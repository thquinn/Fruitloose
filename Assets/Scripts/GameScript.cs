using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public Transform canvasTransform;
    public GameObject prefabTitle, prefabMaze, prefabScorePopup;
    public CameraScript cameraScript;
    public MoveButtonsScript moveButtonsScript;
    public ScoreBarScript scoreBarScript;
    public ScorePopupScript scorePopupScript;

    TitleScript titleScript;
    MazeScript mazeScript;
    bool detaching;
    int levelIndex;
    int totalScore;

    void Start() {
        titleScript = Instantiate(prefabTitle, canvasTransform).GetComponent<TitleScript>();
    }

    void Update() {
        if (titleScript != null && mazeScript == null && Input.GetMouseButtonDown(0)) {
            titleScript.Dismiss();
            titleScript = null;
            StartLevel();
        }
        if (!detaching && mazeScript != null && mazeScript.maze != null && mazeScript.maze.gel.coor == mazeScript.maze.exit) {
            totalScore += mazeScript.maze.totalTiles - mazeScript.maze.par;
            DetachLevel();
            scorePopupScript = Instantiate(prefabScorePopup, canvasTransform).GetComponent<ScorePopupScript>();
            levelIndex++;
            scorePopupScript.SetText(levelIndex, 6, mazeScript.maze.par, mazeScript.maze.totalTiles);
        }
        if (!detaching && scorePopupScript != null && Input.GetMouseButtonDown(0)) {
            scorePopupScript.Dismiss();
            scorePopupScript = null;
            StartLevel();
        }
    }

    void StartLevel() {
        mazeScript = Instantiate(prefabMaze).GetComponent<MazeScript>();
        mazeScript.Init(new Maze(new Int2(6, 6)));
        cameraScript.mazeScript = mazeScript;
        moveButtonsScript.mazeScript = mazeScript;
        scoreBarScript.mazeScript = mazeScript;
    }
    void DetachLevel() {
        cameraScript.mazeScript = null;
        moveButtonsScript.mazeScript = null;
        scoreBarScript.mazeScript = null;
        detaching = true;
        Invoke("DestroyLevel", 1);
    }
    void DestroyLevel() {
        Destroy(mazeScript.terrainScript.gameObject);
        Destroy(mazeScript.gameObject);
        mazeScript = null;
        detaching = false;
    }
}
