using NesE.nes;
using NesE.nes.memory;
using NesE.nes.ppu;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.nes.teststubs;
using Xunit;

namespace Tests.nes.memory
{
    public class VRAMTest
    {
        private NES _nes;
        private IMemory _vram;

        public VRAMTest()
        {
            _nes = new NES();
            _nes.SetRom(new TestROM());
            _vram = _nes.PPU.Memory;
        }

        [Fact]
        public void ShouldMirrorFirstPart()
        {
            const byte Expected = 10;

            _vram.Set(0x2000, Expected);

            Assert.Equal(Expected, _vram.Get(0x3000));
        }

        [Fact]
        public void ShouldMirrorPalette()
        {
            const byte Expected = 10;

            _vram.Set(0x3F00, Expected);

            Assert.Equal(Expected, _vram.Get(0x3F20));
            Assert.Equal(Expected, _vram.Get(0x3F40));
        }
    }
}
