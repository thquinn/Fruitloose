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
            while (queue.Count > 0) {
                Int2 current = queue.Dequeue();
                foreach (Int2 neighbor in maze.GetNeighbors(current)) {
                    if (parents.ContainsKey(neighbor)) {
                        continue;
                    }
                    parents[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
                if (parents.ContainsKey(maze.exit)) {
                    break;
                }
            }
            if (!parents.ContainsKey(maze.exit)) {
                return;
            }
            Int2 c = maze.exit;
            while (c != coor) {
                path.Add(c);
                c = parents[c];
            }
            path.Reverse();
        }
    }
}
