using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code {
    public class Entity {
        public Maze maze;
        public Int2 coor;
        public EntityType type;

        public Entity(Maze maze, Int2 coor, EntityType type) {
            this.maze = maze;
            this.coor = coor;
            this.type = type;
        }
    }

    public enum EntityType {
        Gel
    }
}
