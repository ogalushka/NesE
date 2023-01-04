using NesE.nes.rom;
using System;

namespace NesE.nes.memory
{
    public class Mappers
    {
        public static IMemory GetMapper(ROM rom)
        {
            if (rom.Mapper != 0)
            {
                throw new Exception($"Mapper {rom.Mapper} is not supported");
            }

            var memory = new CPUMemory();

            var ram = new byte[8 * 1024];
            memory.AddAddressSpace(0b011, 0b0001_1111_1111_1111, ram);


            var prgRom = rom.GetPrgRom();
            byte[] prgRom1;
            byte[] prgRom2;
            var chunkSize = 16 * 1024;
            if (prgRom.Length > chunkSize)
            {
                prgRom1 = new byte[chunkSize];
                prgRom2 = new byte[chunkSize];
                Array.Copy(prgRom, 0, prgRom1, 0, chunkSize);
                Array.Copy(prgRom, chunkSize - 1, prgRom2, 0, chunkSize);
            }
            else
            {
                prgRom1 = prgRom;
                prgRom2 = prgRom;
            }

            memory.AddAddressSpace(0b100, 0b0011_1111_1111_1111, prgRom1);
            memory.AddAddressSpace(0b101, 0b0011_1111_1111_1111, prgRom1);
            memory.AddAddressSpace(0b110, 0b0011_1111_1111_1111, prgRom2);
            memory.AddAddressSpace(0b111, 0b0011_1111_1111_1111, prgRom2);

            return memory;
        }

        public static IMemory AddRomMem(IROM rom, Memory cpuMemory, Memory ppuMem)
        {
            if (rom.Mapper != 0)
            {
                throw new Exception($"Mapper {rom.Mapper} is not supported");
            }

            var ram = new byte[8 * 1024];
            cpuMemory.AddAddressSpace(0b011, 0b0001_1111_1111_1111, ram);


            var prgRom = rom.GetPrgRom();
            byte[] prgRom1;
            byte[] prgRom2;
            var chunkSize = 16 * 1024;
            if (prgRom.Length > chunkSize)
            {
                prgRom1 = new byte[chunkSize];
                prgRom2 = new byte[chunkSize];
                Array.Copy(prgRom, 0, prgRom1, 0, chunkSize);
                Array.Copy(prgRom, chunkSize - 1, prgRom2, 0, chunkSize);
            }
            else
            {
                prgRom1 = prgRom;
                prgRom2 = prgRom;
            }

            cpuMemory.AddAddressSpace(0b100, 0b0011_1111_1111_1111, prgRom1);
            cpuMemory.AddAddressSpace(0b101, 0b0011_1111_1111_1111, prgRom1);
            cpuMemory.AddAddressSpace(0b110, 0b0011_1111_1111_1111, prgRom2);
            cpuMemory.AddAddressSpace(0b111, 0b0011_1111_1111_1111, prgRom2);

            var chrRom = rom.GetChrRom();
            ppuMem.AddAddressSpace(0, 0b0001_1111_1111_1111, rom.GetChrRom());
            ppuMem.AddAddressSpace(1, 0b0001_1111_1111_1111, rom.GetChrRom());

            return cpuMemory;
        }
    }
}
