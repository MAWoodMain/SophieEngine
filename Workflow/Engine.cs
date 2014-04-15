using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Workflow
{
    class Engine
    {
        private static Form Window;
        private static List<PhysicsObject> Objects;
        private static long Ticks;
        private static int TickGap = 20;

        public static void Start(Form window)
        {
            Objects = new List<PhysicsObject>();
            Window = window;
        }
        public static void Properties(int SizeX, int SizeY)
        {
            Window.Size = new Size(SizeX, SizeY);
            Window.FormBorderStyle = FormBorderStyle.None;
        }
        public static Form getWindow()
        {
            return Window;
        }

        public static List<PhysicsObject> getObjects()
        {
            return Objects;
        }
        public static void addObject(PhysicsObject Object)
        {
            Window.Controls.Add(Object.getBox());
            Objects.Add(Object);
        }
        public static void removeObject(PhysicsObject Object)
        {
            Objects.Remove(Object);
        }

        public static void Run()
        {
            Thread ticks = new Thread(new ThreadStart(doTick));
            ticks.Start();
            Ticks = 0;
        }
        public static long getTicks()
        {
            return Ticks;
        }
        public static int getTickGap()
        {
            return TickGap;
        }
        public static void doTick()
        {
            while (true)
            {
                try
                {
                    foreach (PhysicsObject po in Objects)
                    {
                        po.doTick();
                    }
                }
                catch { }
                Ticks += 1;
                Thread.Sleep(TickGap);
            }
        }
    }
}
