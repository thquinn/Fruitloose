using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code {
    public class GoldenFruit : Entity {
        public GoldenFruit(Maze maze, Int2 coor) : base(maze, coor, EntityType.GoldenFruit) {
        }
    }
}
