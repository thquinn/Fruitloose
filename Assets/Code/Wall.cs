using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code {
    public class Wall {
        public Maze maze;
        public Int2 coor;
        public bool horizontal;

        public Wall(Maze maze, Int2 coor, bool horizontal) {
            this.maze = maze;
            this.coor = coor;
            this.horizontal = horizontal;
        }
    }
}
