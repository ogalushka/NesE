using System;
using NesE.nes.cpu;
using NesE.nes.memory;
using NesE.nes.ppu;
using NesE.nes.rom;

namespace NesE.nes
{
    public class Nes
    {
        public CPU CPU;
        public PPU PPU;

        public Nes()
        {
        }

        public void StartRom(byte[] romBytes)
        {
            var rom = new ROM(romBytes);
            Setup(rom);
        }

        public void Update(double dt)
        {
            CPU.Step();
        }

        private void Setup(ROM rom)
        {
            var cpuMem = new Memory(13);
            var ppuMem = new Memory(13);

            var inernalRam = new byte[0x800];
            var ppuRegisters = new byte[0x8];
            var ioRegisters = new IORegisters();
            var dummySpace = new byte[0x2000];
            
            ioRegisters.OAMWrite -= WriteOam;
            ioRegisters.OAMWrite += WriteOam;

            cpuMem.AddAddressSpace(0, 0b0000_0111_1111_1111, inernalRam);
            cpuMem.AddAddressSpace(1, 0b0000_0000_0000_0111, ppuRegisters);
            cpuMem.AddAddressSpace(2, ioRegisters);
            cpuMem.AddAddressSpace(3, 0x1FFF, dummySpace);
            cpuMem.AddAddressSpace(4, 0x1FFF, dummySpace);
            cpuMem.AddAddressSpace(5, 0x1FFF, dummySpace);
            cpuMem.AddAddressSpace(6, 0x1FFF, dummySpace);
            cpuMem.AddAddressSpace(7, 0x1FFF, dummySpace);

            Mappers.AddRomMem(rom, cpuMem, ppuMem);

            CPU = new CPU(cpuMem);
            PPU = new PPU(ppuMem, new PPURegisters(ppuRegisters));
        }

        private void WriteOam(byte address)
        {
            int startAdddress = address << 8;
            var endAddresss = startAdddress + 0x100;
            for (var cpuI = startAdddress; cpuI < endAddresss; cpuI++)
            {
                var oamI = cpuI - startAdddress;
                PPU.OAM[oamI] = CPU.Ram.Get(cpuI);
            }
        }
    }
}
