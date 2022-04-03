using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code {
    public class MoveWall : Move {
        Wall wall;
        Int2 coor;

        public MoveWall(Wall wall, Int2 coor) {
            this.wall = wall;
            this.coor = coor;
        }

        public void Reverse(Maze maze) {
            maze.MoveWall(wall, coor);
        }
    }
}
