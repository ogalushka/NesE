using NesE.nes.rom;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.nes.rom
{
    public class RomPraseTest
    {
        [Fact]
        public void ShouldParsePRGSize()
        {
            var bytes = new byte[16];
            bytes[4] = 1;

            var rom = new ROM(bytes);

            Assert.Equal(16384, rom.PrgRomBytes);
        }

        [Fact]
        public void ShouldParseCHRSize()
        {
            var bytes = new byte[16];
            bytes[5] = 1;

            var rom = new ROM(bytes);

            Assert.Equal(8192, rom.ChrRomBytes);
        }

        [Theory]
        [InlineData(1, RomMirroring.Vertical)]
        [InlineData(2, RomMirroring.Horizontal)]
        public void ShouldPraseMIrroring(byte data, RomMirroring mirroring)
        {
            var bytes = new byte[16];
            bytes[6] = data;
            var rom = new ROM(bytes);

            Assert.Equal(mirroring, rom.Mirroring);
        }

        [Theory]
        [InlineData(0b10, true)]
        [InlineData(0, false)]
        public void ShouldPrasePersistentMem(byte data, bool expected)
        {
            var bytes = new byte[16];
            bytes[6] = data;
            var rom = new ROM(bytes);

            Assert.Equal(expected, rom.PersistentMem);
        }

        [Theory]
        [InlineData(0b100, true)]
        [InlineData(0, false)]
        public void ShouldPraseTrainer(byte data, bool expected)
        {
            var bytes = new byte[16];
            bytes[6] = data;
            var rom = new ROM(bytes);

            Assert.Equal(expected, rom.Trainer);
        }

        [Theory]
        [InlineData(0b1000, true)]
        [InlineData(0, false)]
        public void ShouldPraseIgnoreMirroring(byte data, bool expected)
        {
            var bytes = new byte[16];
            bytes[6] = data;
            var rom = new ROM(bytes);

            Assert.Equal(expected, rom.IgnoreMirroring);
        }

        [Fact]
        public void ShouldParseMapperNumber()
        {
            var bytes = new byte[16];

            const byte Expected = 0x4F;
            const byte lowerNymb = 0xF;
            const byte higherNymb = 0x4;

            bytes[6] = lowerNymb << 4;
            bytes[7] = higherNymb << 4;

            var rom = new ROM(bytes);

            Assert.Equal(Expected, rom.Mapper);
        }

    }
}
