using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code {
    public class Gel : Entity {
        public List<Int2> path;

        public Gel(Maze maze, Int2 coor) : base(maze, coor, EntityType.Gel) {
            CalculatePath();
        }

        public void CalculatePath() {
            if (path == null) {
                path = new List<Int2>();
            } else {
                path.Clear();
            }
            Dictionary<Int2, Int2> parents = new Dictionary<Int2, Int2>();
            parents.Add(coor, new Int2(-1, -1));
            Queue<Int2> queue = new Queue<Int2>();
            queue.Enqueue(coor);
            Entity overrideGoal = null;
            while (queue.Count > 0) {
                Int2 current = queue.Dequeue();
                foreach (Int2 neighbor in maze.GetNeighbors(current)) {
                    if (parents.ContainsKey(neighbor)) {
                        continue;
                    }
                    parents[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
                if (overrideGoal == null) {
                    Entity entity = maze.entities[current.x, current.y];
                    if (entity != null && entity.type == EntityType.GoldenFruit) {
                        overrideGoal = entity;
                    }
                }
            }
            Int2 goalCoor = overrideGoal != null ? overrideGoal.coor : maze.exit;
            if (!parents.ContainsKey(goalCoor)) {
                return;
            }
            Int2 c = goalCoor;
            while (c != coor) {
                path.Add(c);
                c = parents[c];
            }
            path.Reverse();
        }
    }
}
