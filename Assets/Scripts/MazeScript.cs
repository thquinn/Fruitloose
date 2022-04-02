using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeScript : MonoBehaviour {
    public GameObject prefabTile, prefabWall, prefabGel;

    Maze maze;

    void Start() {
        maze = new Maze(new Int2(8, 8));
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
        Instantiate(prefabGel, transform).GetComponent<GelScript>().Set(maze.gel);
        // Walls.
        for (int x = 0; x < maze.wallsRight.GetLength(0); x++) {
            for (int y = 0; y < maze.wallsRight.GetLength(1); y++) {
                Wall wall = maze.wallsRight[x, y];
                if (wall != null) {
                    Instantiate(prefabWall, transform).GetComponent<WallScript>().Set(wall);
                }
            }
        }
        for (int x = 0; x < maze.wallsBelow.GetLength(0); x++) {
            for (int y = 0; y < maze.wallsBelow.GetLength(1); y++) {
                Wall wall = maze.wallsBelow[x, y];
                if (wall != null) {
                    Instantiate(prefabWall, transform).GetComponent<WallScript>().Set(wall);
                }
            }
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            maze.MoveEntity(maze.gel, maze.gel.path[0]);
            maze.gel.CalculatePath();
        }
    }
}
