using NesE.nes;
using NesE.nes.rom;
using System.IO;
using Xunit;

namespace Tests.nes.nestest
{
    public class NesTestRomTest
    {
        [Fact]
        public void TestRom()
        {
            var console = new NES();
            var romData = File.ReadAllBytes("rom/nestest.nes");
            var logData = File.ReadAllLines("rom/log.txt");
            console.SetRom(new ROM(romData));
            console.CPU.PC = 0xC000;
            console.CPU.S = 0xFD;

            var i = 0;
            var cpu = console.CPU;

            ///C62F custom removed jsr
            while (cpu.PC != 0xC62F && i < logData.Length)
            {
                var progress = (double)i / logData.Length;
                var currentLog = logData[i];
                var cpuRegisters = $"A:{cpu.A.ToString("X2")} X:{cpu.X.ToString("X2")} Y:{cpu.Y.ToString("X2")} P:{((byte)cpu.P).ToString("X2")} SP:{cpu.S.ToString("X2")}";
                var logRegisters = currentLog.Substring(48, 25);
                //Debug.WriteLine(currentLog);
                //Debug.WriteLine($"{cpu.PC.ToString("X4")}                                            A:{cpu.A.ToString("X2")} X:{cpu.X.ToString("X2")} Y:{cpu.Y.ToString("X2")} P:{((byte)cpu.P).ToString("X2")} SP:{cpu.S.ToString("X2")}");
                Assert.Equal(0, console.CPU.RAM[0x2]);
                Assert.Equal(0, console.CPU.RAM[0x3]);
                Assert.Equal(currentLog.Substring(0, 4), console.CPU.PC.ToString("X4"));
                Assert.Equal(logRegisters, cpuRegisters);
                console.Update(0);
                
                i++;
            }
        }
    }
}
