using NesE.emu;
using NesE.nes;
using NesE.nes.ppu;
using NesE.nes.rom;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace NesE
{
    class Program
    {
        // TODO RESET NMI
        private const int ScreenWidth = 256;
        private const int ScreenHeight = 240;

        static void Main(string[] args)
        {
            const int FrameTimeMS = 1000 / 60 - 10;
            var pathToRom = GetAvailableRoms().FirstOrDefault();
            if (string.IsNullOrEmpty(pathToRom))
            {
                throw new Exception("no rom found");
            }

            RenderWindow w = new RenderWindow(new VideoMode(800, 600), "Test");
            w.Closed += (s, e) => w.Close();

            var romData = File.ReadAllBytes(pathToRom);
            var console = new NES();
            console.SetRom(new ROM(romData));

            const int chrWidth = 128;
            const int chrHeight = 256;

            var chrPixels = new byte[chrWidth * chrHeight * 4];
            var chrTexture = new Texture(chrWidth, chrHeight);
            var chrTable = new Sprite(chrTexture);

            chrTable.Scale = new Vector2f(2, 2);
            chrTable.Position = new Vector2f(w.Size.X - chrTable.GetGlobalBounds().Width, w.Size.Y - chrTable.GetGlobalBounds().Height);

            var screenPixels = new byte[ScreenWidth * ScreenHeight * 4];
            var screenTexture = new Texture(ScreenWidth, ScreenHeight);
            var screen = new Sprite(screenTexture);
            screen.Scale = new Vector2f(2, 2);

            for (var i = 0; i < screenPixels.Length; i++)
            {
                screenPixels[i] = byte.MaxValue;
            }

            var font = new Font("asset/cour.ttf");
            var text = new Text();
            text.Font = font;
            text.FillColor = Color.White;

            float elapsed = 0;
            int[] fps = new int[60];
            int fpsIndex = 0;

            var clock = new Clock();
            clock.Restart();

            while (w.IsOpen)
            {
                elapsed = clock.ElapsedTime.AsSeconds();
                clock.Restart();

#if DEBUG
                // Skip update if large step because of breakpoint
                if (elapsed < 1f)
#endif
                {
                    console.Update(elapsed);
                }

                DrawUnits(screenPixels, console.PPU.RenderQueue);
                screenTexture.Update(screenPixels);

                DebugDraws.DrawChrRom(console.PPU.Memory, chrPixels, 0, 0x2000, chrWidth);
                chrTexture.Update(chrPixels);

                fps[fpsIndex] = (int)MathF.Round(1f / elapsed);
                fpsIndex = ++fpsIndex % fps.Length;
                text.DisplayedString = Avarage(fps).ToString() + "\n" + Min(fps).ToString();

                w.Clear();
                w.Draw(chrTable);
                w.Draw(screen);
                w.Draw(text);
                w.Display();

                w.DispatchEvents();

                var elapsedMiliseconds = clock.ElapsedTime.AsMilliseconds();
                if (elapsedMiliseconds < FrameTimeMS)
                {
                    //Thread.Sleep(FrameTimeMS - elapsedMiliseconds);
                }
            }
        }

        private static void DrawUnits(byte[] pixels, Queue<RenderUnit> renderUnits)
        {
            byte[] Color = new byte[] { 0, 85, 170, 255 };
            byte[] Alpha = new byte[] { 0, 255, 255, 255 };
            const int bytePerPixel = 4;
            const int pixelsPerTile = 8;
            while (renderUnits.Count != 0)
            {
                var unit = renderUnits.Dequeue();

                var lineStartIndex = (unit.Y * ScreenWidth + unit.X) * bytePerPixel;

                for (var i = 0; i < pixelsPerTile; i++)
                //for (var lineIndex = lineStartIndex; lineIndex < lineEndIndex; lineIndex += pixelsPerTile)
                {
                    var pixelIndex = lineStartIndex + (i * bytePerPixel);

                    var part1 = ((unit.LowPattern >> i) & 1) * 1;
                    var part2 = ((unit.HighPattern >> i) & 1) * 2;
                    var pixelColor = part1 + part2;

                    if (pixelColor != 0)
                    {
                        continue;
                    }

                    // RGBA
                    pixels[pixelIndex] = Color[pixelColor];
                    pixels[pixelIndex + 1] = Color[pixelColor];
                    pixels[pixelIndex + 2] = Color[pixelColor];
                    pixels[pixelIndex + 3] = Alpha[pixelColor];
                }
            }
        }

        private static int Avarage(int[] values)
        {
            var total = 0;
            for (var i = 0; i < values.Length; i++)
            {
                total += values[i];
            }

            return total / values.Length;
        }

        private static int Min(int[] values)
        {
            var min = int.MaxValue;
            for (var i = 0; i < values.Length; i++)
            {
                if (min > values[i])
                {
                    min = values[i];
                }
            }
            return min;
        }

        static string[] GetAvailableRoms()
        {
            return Directory.EnumerateFiles("rom/", "*.nes").ToArray();
        }
    }
}
