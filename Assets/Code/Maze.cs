using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code {
    public class Maze {
        public Int2 dimensions;
        public Entity[,] entities;
        public Gel gel;
        public Wall[,] wallsRight, wallsBelow;
        public Int2 exit;
        public int tilesPerMove;
        public int tilesLeftThisMove;

        public Maze(Int2 dimensions) {
            this.dimensions = dimensions;
            entities = new Entity[dimensions.x, dimensions.y];
            wallsRight = new Wall[dimensions.x - 1, dimensions.y];
            wallsBelow = new Wall[dimensions.x, dimensions.y - 1];
            RandomWalls();
            exit = new Int2(dimensions.x - 1, dimensions.y - 1);
            gel = new Gel(this, new Int2(0, 0));
            entities[0, 0] = gel;
            tilesPerMove = 1;
            tilesLeftThisMove = tilesPerMove;
        }

        public void RandomWalls() {
            for (int x = 0; x < wallsRight.GetLength(0); x++) {
                for (int y = 0; y < wallsRight.GetLength(1); y++) {
                    wallsRight[x, y] = new Wall(this, new Int2(x, y), false);
                }
            }
            for (int x = 0; x < wallsBelow.GetLength(0); x++) {
                for (int y = 0; y < wallsBelow.GetLength(1); y++) {
                    wallsBelow[x, y] = new Wall(this, new Int2(x, y), true);
                }
            }
            // Randomized Prim's algorithm.
            Int2 firstCell = new Int2(UnityEngine.Random.Range(0, dimensions.x), UnityEngine.Random.Range(0, dimensions.y));
            HashSet<Int2> visited = new HashSet<Int2>();
            visited.Add(firstCell);
            List<Wall> wallList = new List<Wall>();
            RandomWallsAddWallsToList(wallList, firstCell);
            while (wallList.Count > 0) {
                int index = UnityEngine.Random.Range(0, wallList.Count);
                Wall wall = wallList[index];
                wallList.RemoveAt(index);
                Int2 cellOne = wall.coor;
                Int2 cellTwo = new Int2(wall.coor.x + (wall.horizontal ? 0 : 1), wall.coor.y + (wall.horizontal ? 1 : 0));
                if (visited.Contains(cellOne) != visited.Contains(cellTwo)) {
                    (wall.horizontal ? wallsBelow : wallsRight)[cellOne.x, cellOne.y] = null;
                    Int2 unvisitedCell = visited.Contains(cellOne) ? cellTwo : cellOne;
                    visited.Add(unvisitedCell);
                    RandomWallsAddWallsToList(wallList, unvisitedCell);
                }
            }
        }
        void RandomWallsAddWallsToList(List<Wall> list, Int2 cell) {
            if (cell.x > 0 && wallsRight[cell.x - 1, cell.y] != null) {
                list.Add(wallsRight[cell.x - 1, cell.y]);
            }
            if (cell.x < dimensions.x - 1 && wallsRight[cell.x, cell.y] != null) {
                list.Add(wallsRight[cell.x, cell.y]);
            }
            if (cell.y > 0 && wallsBelow[cell.x, cell.y - 1] != null) {
                list.Add(wallsBelow[cell.x, cell.y - 1]);
            }
            if (cell.y < dimensions.y - 1 && wallsBelow[cell.x, cell.y] != null) {
                list.Add(wallsBelow[cell.x, cell.y]);
            }
        }

        public List<Int2> GetNeighbors(Int2 coor) {
            List<Int2> neighbors = new List<Int2>();
            if (coor.x > 0 && wallsRight[coor.x - 1, coor.y] == null) {
                neighbors.Add(new Int2(coor.x - 1, coor.y));
            }
            if (coor.x < dimensions.x - 1 && wallsRight[coor.x, coor.y] == null) {
                neighbors.Add(new Int2(coor.x + 1, coor.y));
            }
            if (coor.y > 0 && wallsBelow[coor.x, coor.y - 1] == null) {
                neighbors.Add(new Int2(coor.x, coor.y - 1));
            }
            if (coor.y < dimensions.y - 1 && wallsBelow[coor.x, coor.y] == null) {
                neighbors.Add(new Int2(coor.x, coor.y + 1));
            }
            return neighbors;
        }

        public bool MoveGel() {
            MoveEntity(gel, gel.path[0]);
            gel.CalculatePath();
            tilesLeftThisMove--;
            if (tilesLeftThisMove == 0) {
                tilesPerMove++;
                tilesLeftThisMove = tilesPerMove;
                return true;
            }
            return false;
        }
        void MoveEntity(Entity entity, Int2 coor) {
            Debug.Assert(entities[coor.x, coor.y] == null);
            entities[entity.coor.x, entity.coor.y] = null;
            entities[coor.x, coor.y] = entity;
            entity.coor = coor;
        }

        public List<Int2> GetWallMoves(Wall wall) {
            List<Int2> wallMoves = new List<Int2>();
            Wall[,] walls = wall.horizontal ? wallsBelow : wallsRight;
            Int2 coor = wall.coor;
            coor += wall.horizontal ? new Int2(-1, 0) : new Int2(0, -1);
            while (coor.x >= 0 && coor.y >= 0 && walls[coor.x, coor.y] == null) {
                // Check for double walls blocking the way.
                if (wall.horizontal && wallsRight[coor.x, coor.y] != null && wallsRight[coor.x, coor.y + 1] != null) {
                    break;
                }
                if (!wall.horizontal && wallsBelow[coor.x, coor.y] != null && wallsBelow[coor.x + 1, coor.y] != null) {
                    break;
                }
                wallMoves.Add(coor);
                coor += wall.horizontal ? new Int2(-1, 0) : new Int2(0, -1);
            }
            coor = wall.coor;
            coor += wall.horizontal ? new Int2(1, 0) : new Int2(0, 1);
            while (coor.x < walls.GetLength(0) && coor.y < walls.GetLength(1) && walls[coor.x, coor.y] == null) {
                // Check for double walls blocking the way.
                if (wall.horizontal && wallsRight[coor.x - 1, coor.y] != null && wallsRight[coor.x - 1, coor.y + 1] != null) {
                    break;
                }
                if (!wall.horizontal && wallsBelow[coor.x, coor.y - 1] != null && wallsBelow[coor.x + 1, coor.y - 1] != null) {
                    break;
                }
                wallMoves.Add(coor);
                coor += wall.horizontal ? new Int2(1, 0) : new Int2(0, 1);
            }
            return wallMoves;
        }
        public void MoveWall(Wall wall, Int2 coor) {
            Wall[,] walls = wall.horizontal ? wallsBelow : wallsRight;
            Debug.Assert(walls[coor.x, coor.y] == null);
            walls[wall.coor.x, wall.coor.y] = null;
            walls[coor.x, coor.y] = wall;
            wall.coor = coor;
        }
    }
}
