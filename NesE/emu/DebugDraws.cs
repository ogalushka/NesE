using NesE.nes.memory;
using System;
using System.Diagnostics;

namespace NesE.emu
{
    public static class DebugDraws
    {
        private static byte[] Color = new byte[] { 0, 85, 170, 255 };
        private static byte[] Alpha = new byte[] { 0, 255, 255, 255 };

        public static void DrawChrRom(IMemory ppuMem, byte[] outputPixels, int startAddress, int endAddress, int width)
        {
            const int TileSize = 8;
            var pixelsToDraw = (endAddress - startAddress) * 4;
            if (pixelsToDraw != outputPixels.Length / 4)
            {
                throw new Exception($"Invalid parameters cant draw {pixelsToDraw} pixels in {outputPixels.Length} length array, expected {pixelsToDraw * 4} array length");
            }

            var tilePerWidth = width / TileSize;

            for (var tileIndex = startAddress; tileIndex < endAddress; tileIndex += 0x10)
            {
                for (var lineIndex = 0; lineIndex < TileSize; lineIndex++)
                {
                    var lineByte1 = ppuMem[tileIndex + lineIndex];
                    var lineByte2 = ppuMem[tileIndex + lineIndex + TileSize];

                    for (var pixelIndex = TileSize - 1; pixelIndex >= 0; pixelIndex--)
                    {
                        var part1 = ((lineByte1 >> pixelIndex) & 1) * 1;
                        var part2 = ((lineByte2 >> pixelIndex) & 1) * 2;
                        var pixelColor = part1 + part2;

                        var tile = (tileIndex >> 4) - (startAddress >> 4);
                        var tileX = tile % tilePerWidth;
                        var tileY = tile / tilePerWidth;
                        var pixelX = (tileX * TileSize) + (7 - pixelIndex);
                        var pixelY = (tileY * TileSize) + lineIndex;
                        var outPixelIndex = ((pixelY * width) + pixelX) * 4;

                        outputPixels[outPixelIndex] = Color[pixelColor];
                        outputPixels[outPixelIndex + 1] = Color[pixelColor];
                        outputPixels[outPixelIndex + 2] = Color[pixelColor];
                        outputPixels[outPixelIndex + 3] = Alpha[pixelColor];
                    }
                }
            }
        }
    }
}
