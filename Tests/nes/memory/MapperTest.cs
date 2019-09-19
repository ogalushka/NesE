using NesE.nes.memory;
using NesE.nes.rom;
using System;
using Xunit;

namespace Tests.nes.memory
{
    public class MapperTest
    {
        [Fact]
        public void ShouldMapPrgToMemory()
        {
            var prgMemory = new byte[32 * 1024];
            prgMemory[0] = 123;

            var rom = GetRomWith(2, prgMemory);
            var mapper = Mappers.GetMapper(rom);
            Assert.Equal(123, mapper[0x8000]);
        }

        [Fact]
        public void ShouldMirrorPrg()
        {
            var prgMemory = new byte[16 * 1024];
            prgMemory[0] = 123;

            var rom = GetRomWith(1, prgMemory);
            var mapper = Mappers.GetMapper(rom);
            Assert.Equal(123, mapper[0x8000]);
            Assert.Equal(123, mapper[0xC000]);
        }

        private static ROM GetRomWith(byte prgChunks, byte[] prgMemory)
        {
            var romBytes = new byte[0x10000];
            romBytes[0] = 0x4E;
            romBytes[1] = 0x45;
            romBytes[2] = 0x53;
            romBytes[3] = 0x1A;
            romBytes[4] = prgChunks;
            Array.Copy(prgMemory, 0, romBytes, 16, prgMemory.Length);

            return new ROM(romBytes);
        }
    }
}
