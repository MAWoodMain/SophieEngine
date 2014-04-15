using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Workflow
{
    class Vector
    {
        private Point Trajectory;

        public Vector(int x, int y)
        {
            Trajectory = new Point(x, y);
        }

        public void setVector(int x, int y)
        {
            Trajectory = new Point(x, y);
        }

        public int getX() 
        {
            return Trajectory.X;
        }

        public int getY()
        {
            return Trajectory.Y;
        }
    }
}
