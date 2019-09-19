using NesE.emu;
using NesE.nes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.IO;
using System.Linq;

namespace NesE
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathToRom = GetAvailableRoms().FirstOrDefault();
            if (string.IsNullOrEmpty(pathToRom))
            {
                throw new Exception("no rom found");
            }

            var romData = File.ReadAllBytes(pathToRom);
            var console = new Nes();
            console.StartRom(romData);

            const int width = 128;
            const int height = 256;

            var pixels = new byte[width * height * 4];
            var texture = new Texture(width, height);
            var sprite = new Sprite(texture);
            RenderWindow w = new RenderWindow(new VideoMode(800, 600), "Test");
            sprite.Scale = new Vector2f(2, 2);
            sprite.Position = new Vector2f(w.Size.X - sprite.GetGlobalBounds().Width, w.Size.Y - sprite.GetGlobalBounds().Height);

            w.Closed += (s, e) => w.Close();

            while (w.IsOpen)
            {
                w.WaitAndDispatchEvents();

                w.Clear();
                console.Update(0);
                DebugDraws.DrawChrRom(console.PPU.Memory, pixels, 0, 0x2000, width);
                texture.Update(pixels);
                w.Draw(sprite);
                w.Display();
            }
        }

        static string[] GetAvailableRoms()
        {
            return Directory.EnumerateFiles("rom/", "*.nes").ToArray();
        }
    }
}
