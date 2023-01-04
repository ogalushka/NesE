using NesE.nes;
using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public class FlagInstructionTests : BaseCPUTest
    {
        [Theory]
        [InlineData(OP.SEC_IMP, PFlag.C)]
        [InlineData(OP.SED_IMP, PFlag.D)]
        [InlineData(OP.SEI_IMP, PFlag.I)]
        public void ShouldSetFlag(byte op, PFlag flag)
        {
            CPU.RAM[0] = op;

            CPU.Step();

            FlagAssert.AssertFlagSet(CPU, flag);
        }

        [Theory]
        [InlineData(OP.CLC_IMP, PFlag.C)]
        [InlineData(OP.CLD_IMP, PFlag.D)]
        [InlineData(OP.CLI_IMP, PFlag.I)]
        [InlineData(OP.CLV_IMP, PFlag.V)]
        public void ShouldClearFlag(byte op, PFlag flag)
        {
            CPU.RAM[0] = op;
            CPU.SetFlag(flag);

            CPU.Step();

            FlagAssert.AssertFlagCleared(CPU, flag);
        }
    }
}
