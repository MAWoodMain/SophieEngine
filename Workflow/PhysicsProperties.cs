using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow
{
    class PhysicsProperties
    {
        private PhysicsObject Object;
        private Vector Difference;
        private int Mass;
        private int Gravity;
        private Boolean Enabled;

        public PhysicsProperties(PhysicsObject obj, int mass, int gravity, Boolean physicsenabled)
        {
            Object = obj;
            Difference = new Vector(0, 0);
            Mass = mass;
            Gravity = gravity;
            Enabled = physicsenabled;
        }

        public static int toZero(int Vel, int am)
        {
            if (Vel > 0) return Vel - am;
            if (Vel < 0) return Vel + (am * -1);
            return 0;
        }

        public int getMass()
        {
            return Mass;
        }

        public Boolean isPhysicsEnabled()
        {
            return Enabled;
        }

        public void possibleVector()
        {
            if (Object.getVector().getX() > Gravity) Object.getVector().setVector(Gravity, Object.getVector().getY());
            if (Object.getVector().getX() < Gravity * -1) Object.getVector().setVector(Gravity * -1, Object.getVector().getY());
            if (Object.getVector().getY() > Gravity) Object.getVector().setVector(Object.getVector().getX(), Gravity);
            if (Object.getVector().getY() < Gravity * -1) Object.getVector().setVector(Object.getVector().getX(), Gravity * -1);
        }

        public void checkCollision() //make way to find out from what direction
        {
            foreach (PhysicsObject po in Engine.getObjects())
            {
                if (Object != po)
                {
                    if (Object.getBox().Bounds.IntersectsWith(po.getBox().Bounds))
                    {
                        if (Object.getVector().getY() > 1)
                        {
                            if (Object.getMiddle().Y > po.getMiddle().Y) Object.getVector().setVector(Object.getVector().getX(), Math.Abs(Object.getVector().getY() - 3)); //Move down
                            if (Object.getMiddle().Y <= po.getMiddle().Y) Object.getVector().setVector(Object.getVector().getX(), Math.Abs(Object.getVector().getY() - 3) * -1); //Move up
                        }
                        if (po.getVector().getX() > 1)
                        {
                            if (Object.getMiddle().X >= po.getMiddle().X) Object.getVector().setVector(Math.Abs(Object.getVector().getX() + Mass), Object.getVector().getY()); //Move rights
                            if (Object.getMiddle().X < po.getMiddle().X) Object.getVector().setVector(Math.Abs(Object.getVector().getX() + Mass) * -1, Object.getVector().getY()); //Move left
                        }
                    }
                }
            }
        }
    }
}
