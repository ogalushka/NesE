using NesE.nes;
using NesE.nes.cpu;
using NesE.nes.cpu.opcode;
using Xunit;

namespace Tests.nes.cpu
{
    public class BranchOnFlagTests : BaseCPUTest
    {

        [Theory]
        [InlineData(OP.BCS_REL, 0x10, PFlag.C)]
        [InlineData(OP.BCS_REL, -1, PFlag.C)]
        [InlineData(OP.BEQ_REL, 0x10, PFlag.Z)]
        [InlineData(OP.BEQ_REL, -1, PFlag.Z)]
        [InlineData(OP.BMI_REL, 0x10, PFlag.N)]
        [InlineData(OP.BMI_REL, -1, PFlag.N)]
        [InlineData(OP.BVS_REL, 0x10, PFlag.V)]
        [InlineData(OP.BVS_REL, -1, PFlag.V)]
        public void ShouldBranchIfSet(byte op, sbyte value, PFlag flag)
        {
            var ExpectedPC = 2 + value;
            CPU.RAM[0] = op;
            CPU.RAM[1] = (byte)value;
            CPU.SetFlag(flag);

            CPU.Step();

            Assert.Equal(ExpectedPC, CPU.PC);
        }

        [Theory]
        [InlineData(OP.BCS_REL, PFlag.C)]
        [InlineData(OP.BEQ_REL, PFlag.Z)]
        [InlineData(OP.BMI_REL, PFlag.N)]
        [InlineData(OP.BVS_REL, PFlag.V)]
        public void ShouldNotBranchIfClear(byte op, PFlag flag)
        {
            var ExpectedPC = 2;
            CPU.RAM[0] = op;
            CPU.RAM[1] = 100;
            CPU.ClearFlag(flag);

            CPU.Step();

            Assert.Equal(ExpectedPC, CPU.PC);
        }

        [Theory]
        [InlineData(OP.BCC_REL, 0x10, PFlag.C)]
        [InlineData(OP.BCC_REL, -1, PFlag.C)]
        [InlineData(OP.BNE_REL, 0x10, PFlag.Z)]
        [InlineData(OP.BNE_REL, -1, PFlag.Z)]
        [InlineData(OP.BPL_REL, 0x10, PFlag.N)]
        [InlineData(OP.BPL_REL, -1, PFlag.N)]
        [InlineData(OP.BVC_REL, 0x10, PFlag.V)]
        [InlineData(OP.BVC_REL, -1, PFlag.V)]
        public void ShouldBranchIfClear(byte op, sbyte value, PFlag flag)
        {
            var ExpectedPC = 2 + value;
            CPU.RAM[0] = op;
            CPU.RAM[1] = (byte)value;
            CPU.ClearFlag(flag);

            CPU.Step();

            Assert.Equal(ExpectedPC, CPU.PC);
        }

        [Theory]
        [InlineData(OP.BCC_REL, PFlag.C)]
        [InlineData(OP.BNE_REL, PFlag.Z)]
        [InlineData(OP.BPL_REL, PFlag.N)]
        [InlineData(OP.BVC_REL, PFlag.V)]
        public void ShouldNotBranchIfSet(byte op, PFlag flag)
        {
            var ExpectedPC = 2;
            CPU.RAM[0] = op;
            CPU.RAM[1] = 100;
            CPU.SetFlag(flag);

            CPU.Step();

            Assert.Equal(ExpectedPC, CPU.PC);
        }
    }
}
