using NesE.nes;
using SFML.Window;
using System.Diagnostics;

namespace NesE
{
    class Program
    {
        static void Main(string[] args)
        {
            //Window w = new Window(new VideoMode(800, 600), "Test");
            //w.Closed += (s, e) => w.Close();
            var nes = new Nes();
            var stopwatch = new Stopwatch();
            double dt = 0;
            while (/*w.IsOpen*/ true)
            {
                stopwatch.Stop();
                dt = stopwatch.Elapsed.TotalSeconds;
                stopwatch.Reset();
                stopwatch.Start();

                //w.WaitAndDispatchEvents();
                nes.Update(dt);
                //Debug.WriteLine(dt);
            }
        }
    }
}
