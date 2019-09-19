using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public class IRQTest : BaseCPUTest
    {
        [Fact]
        public void ShouldIRQ()
        {
            CPU.Ram[0x5566] = OP.ADC_ABS;
            CPU.PC = 0x5566;
            CPU.P = (PFlag.Z | PFlag.C);
            ushort ExpectedPC = 0x1234;
            var ExpcetedS = CPU.S - 3;
            byte ExpectedStatusReg = (byte)(CPU.P & ~PFlag.B);
            byte pcl = 0x66;
            byte pch = 0x55;

            CPU.Ram[0xFFFE] = 0x34;
            CPU.Ram[0xFFFF] = 0x12;

            CPU.IRQ = true;
            CPU.Step();

            Assert.Equal(ExpectedPC, CPU.PC);
            Assert.Equal(ExpcetedS, CPU.S);
            Assert.Equal(ExpectedStatusReg, CPU.Ram[0x100 + CPU.S + 1]);
            Assert.Equal(pcl, CPU.Ram[0x100 + CPU.S + 2]);
            Assert.Equal(pch, CPU.Ram[0x100 + CPU.S + 3]);
            FlagAssert.AssertFlagSet(CPU, PFlag.I);
        }

        [Fact]
        public void ShouldNMI()
        {
            CPU.Ram[0x5566] = OP.ADC_ABS;
            CPU.PC = 0x5566;
            ushort ExpectedPC = 0x1234;
            var ExpcetedS = CPU.S - 3;
            CPU.P = (PFlag.I | PFlag.C);
            byte ExpectedStatusReg = (byte)(CPU.P & ~PFlag.B);
            byte pcl = 0x66;
            byte pch = 0x55;
            CPU.SetFlag(PFlag.I);

            CPU.Ram[0xFFFA] = 0x34;
            CPU.Ram[0xFFFB] = 0x12;

            CPU.NMI = true;
            CPU.Step();

            Assert.Equal(ExpectedPC, CPU.PC);
            Assert.Equal(ExpcetedS, CPU.S);
            Assert.Equal(ExpectedStatusReg, CPU.Ram[0x100 + CPU.S + 1]);
            Assert.Equal(pcl, CPU.Ram[0x100 + CPU.S + 2]);
            Assert.Equal(pch, CPU.Ram[0x100 + CPU.S + 3]);
            FlagAssert.AssertFlagSet(CPU, PFlag.I);
        }

        [Fact]
        public void ShouldNotIRQIfFlagSet()
        {
            CPU.Ram[0] = OP.ADC_ABS;
            ushort NotExpectedPC = 0x1234;
            CPU.Ram[0xFFFE] = 0x34;
            CPU.Ram[0xFFFF] = 0x12;
            CPU.SetFlag(PFlag.I);

            CPU.Step();

            Assert.NotEqual(NotExpectedPC, CPU.PC);
        }
    }
}
