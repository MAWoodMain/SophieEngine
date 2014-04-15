using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Workflow
{
    class PhysicsObject
    {
        public enum Wall { Left, Right, None };
        private PictureBox Box;
        private String Name;
        private Vector Vector;
        private PhysicsProperties Properties;

        //new PhysicsObject("Dave", new PictureBox{VALUES}, new Vector(0, 0), false)
        public PhysicsObject(String name, PictureBox box, Vector vector, Boolean physics)
        {
            Name = name;
            Box = box;
            Vector = vector;
            Properties = new PhysicsProperties(this, 3, 15, physics);
            Engine.addObject(this);
        }

        public String getName()
        {
            return Name;
        }
        public Vector getVector()
        {
            return Vector;
        }
        public PictureBox getBox()
        {
            return Box;
        }
        public Point getMiddle()
        {
            return new Point(Box.Location.X + (Box.Width / 2), Box.Location.Y + (Box.Height / 2));
        }
        public void setProperties(PhysicsProperties props)
        {
            Properties = props;
        }

        public void Dispose()
        {
            Box.Dispose();
            Engine.removeObject(this);
        }

        public Boolean isTouchingGround()
        {
            if (Box.Location.Y > (Engine.getWindow().Size.Height - 1) - Box.Size.Height)
            {
                return true;
            }
            return false;
        }
        public Boolean willTouchGround()
        {
            if (Box.Location.Y + Vector.getY() > (Engine.getWindow().Size.Height - 1) - Box.Size.Height)
            {
                return true;
            }
            return false;
        }
        public Wall willTouchWall()
        {
            if (Box.Location.X + Vector.getX() > (Engine.getWindow().Size.Width - 1) - Box.Size.Width) return Wall.Right;
            if (Box.Location.X > Engine.getWindow().Size.Width - 1) return Wall.Right;
            if (Box.Location.X + Vector.getX() < 1 + Box.Size.Width) return Wall.Left;
            if (Box.Location.X < 1) return Wall.Left;
            return Wall.None;
        }

        public void doTick()
        {
            if (Properties.isPhysicsEnabled()) {
                Engine.getWindow().Invoke((MethodInvoker)delegate
                {
                    if (!isTouchingGround()) Vector.setVector(Vector.getX(), Vector.getY() + 1);
                    if (willTouchGround()) Vector.setVector(PhysicsProperties.toZero(Vector.getX(), 1), (Vector.getY() * -1) + Properties.getMass());

                    switch (willTouchWall())
                    {
                        case Wall.Right:
                            Vector.setVector((Vector.getX() + Properties.getMass()) * -1, Vector.getY());
                            break;
                        case Wall.Left:
                            Vector.setVector(Math.Abs(Vector.getX()) + Properties.getMass(), Vector.getY());
                            break;
                    }

                    Properties.checkCollision();
                    Properties.possibleVector();
                    if (!willTouchGround()) Box.Location = new Point(Box.Location.X + Vector.getX(), Box.Location.Y + Vector.getY());
                });
            }
        }
    }
}
