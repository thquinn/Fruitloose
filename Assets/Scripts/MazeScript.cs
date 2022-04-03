using Assets.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeScript : MonoBehaviour {
    public GameObject prefabTerrain, prefabTile, prefabWall, prefabGel, prefabArrow, prefabGelPath, prefabFruit;
    public LayerMask layerMaskWall, layerMaskArrow;

    public Maze maze;
    GelScript gelScript;
    FruitScript fruitScript;
    Dictionary<Collider, Wall> wallColliders;
    Wall selectedWall;
    Dictionary<Collider, Int2> wallMoveColliders;
    public Move undo;
    bool waitForAnimation;

    void Start() {
        maze = new Maze(new Int2(5, 5));
        TerrainScript terrainScript = Instantiate(prefabTerrain).GetComponent<TerrainScript>();
        terrainScript.SpawnGrass(maze.entities.GetLength(0), maze.entities.GetLength(1));
        float xOffset = -maze.dimensions.x / 2f + .5f;
        float yOffset = -maze.dimensions.y / 2f + .5f;
        // Floor.
        for (int x = 0; x < maze.dimensions.x; x++) {
            for (int y = 0; y < maze.dimensions.y; y++) {
                GameObject tile = Instantiate(prefabTile, transform);
                tile.transform.localPosition = new Vector3(x + xOffset, 0, -y - yOffset);
            }
        }
        // Gel.
        gelScript = Instantiate(prefabGel, transform).GetComponent<GelScript>();
        gelScript.Set(maze.gel);
        Instantiate(prefabGelPath, transform).GetComponent<GelPathScript>().Set(maze.gel);
        // Fruit.
        fruitScript = Instantiate(prefabFruit, transform).GetComponent<FruitScript>();
        fruitScript.transform.localPosition = new Vector3(maze.exit.x + xOffset, 0, -maze.exit.y - yOffset);
        // Walls.
        wallColliders = new Dictionary<Collider, Wall>();
        for (int x = 0; x < maze.wallsRight.GetLength(0); x++) {
            for (int y = 0; y < maze.wallsRight.GetLength(1); y++) {
                Wall wall = maze.wallsRight[x, y];
                if (wall != null) {
                    GameObject wallObject = Instantiate(prefabWall, transform);
                    wallObject.GetComponent<WallScript>().Set(wall);
                    wallColliders[wallObject.GetComponentInChildren<Collider>()] = wall;
                }
            }
        }
        for (int x = 0; x < maze.wallsBelow.GetLength(0); x++) {
            for (int y = 0; y < maze.wallsBelow.GetLength(1); y++) {
                Wall wall = maze.wallsBelow[x, y];
                if (wall != null) {
                    GameObject wallObject = Instantiate(prefabWall, transform);
                    wallObject.GetComponent<WallScript>().Set(wall);
                    wallColliders[wallObject.GetComponentInChildren<Collider>()] = wall;
                }
            }
        }
        // Other.
        wallMoveColliders = new Dictionary<Collider, Int2>();
    }

    public void ConfirmMove() {
        undo = null;
        waitForAnimation = true;
    }
    public void UndoMove() {
        if (undo == null) {
            return;
        }
        undo.Reverse(maze);
        maze.gel.CalculatePath();
        undo = null;
    }

    void Update() {
        if (waitForAnimation) {
            if (gelScript.DoneAnimating() && maze.MoveGel()) {
                waitForAnimation = false;
            }
            if (fruitScript != null && maze.gel.coor == maze.exit) {
                fruitScript.Eat();
                fruitScript = null;
            }
            return;
        }
        UpdateMoveSelection();
    }
    void UpdateMoveSelection() {
        if (!Input.GetMouseButtonDown(0)) {
            return;
        }
        if (undo != null) {
            return;
        }
        if (maze.gel.coor == maze.exit) {
            return;
        }
        // Arrow selection.
        Int2 nullMove = new Int2(-1, -1);
        Int2 selectedWallMove = wallMoveColliders.GetOrDefault(Util.GetMouseCollider(layerMaskArrow), nullMove);
        if (selectedWallMove != nullMove) {
            undo = new MoveWall(selectedWall, selectedWall.coor);
            maze.MoveWall(selectedWall, selectedWallMove);
            maze.gel.CalculatePath();
            selectedWall = null;
            DestroyAllArrows();
            return;
        }
        // Wall selection.
        Wall clickedWall = wallColliders.GetOrDefault(Util.GetMouseCollider(layerMaskWall));
        if (clickedWall != selectedWall) {
            selectedWall = clickedWall;
            DestroyAllArrows();
            CreateArrowsForSelectedWall();
        }
    }
    void DestroyAllArrows() {
        foreach (Collider collider in wallMoveColliders.Keys) {
            Destroy(collider.transform.parent.gameObject);
        }
        wallMoveColliders.Clear();
    }
    void CreateArrowsForSelectedWall() {
        if (selectedWall == null) {
            return;
        }
        float xOffset = -maze.dimensions.x / 2f + .5f + (selectedWall.horizontal ? 0 : .5f);
        float yOffset = -maze.dimensions.y / 2f + .5f + (selectedWall.horizontal ? .5f : 0);
        List<Int2> wallMoves = maze.GetWallMoves(selectedWall);
        foreach (Int2 wallMove in wallMoves) {
            GameObject arrowObject = Instantiate(prefabArrow, transform);
            arrowObject.transform.localPosition = new Vector3(wallMove.x + xOffset, 0, -wallMove.y - yOffset);
            if (selectedWall.horizontal) {
                arrowObject.transform.localRotation = Quaternion.Euler(0, wallMove.x < selectedWall.coor.x ? -90 : 90, 0);
            } else {
                arrowObject.transform.localRotation = Quaternion.Euler(0, wallMove.y < selectedWall.coor.y ? 0 : 180, 0);
            }
            wallMoveColliders[arrowObject.GetComponentInChildren<Collider>()] = wallMove;
        }
    }
}
