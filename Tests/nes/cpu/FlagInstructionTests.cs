using NesE.nes;
using NesE.nes.cpu;
using Xunit;

namespace Tests.nes.cpu
{
    public class FlagInstructionTests
    {
        private readonly CPU _cpu;

        public FlagInstructionTests()
        {
            _cpu = new CPU(new RAM());
        }

        [Theory]
        [InlineData(OP.SEC_IMP, PFlag.C)]
        [InlineData(OP.SED_IMP, PFlag.D)]
        [InlineData(OP.SEI_IMP, PFlag.I)]
        public void ShouldSetFlag(byte op, PFlag flag)
        {
            _cpu.Ram[0] = op;

            _cpu.Step();

            FlagAssert.Set(_cpu, flag);
        }

        [Theory]
        [InlineData(OP.CLC_IMP, PFlag.C)]
        [InlineData(OP.CLD_IMP, PFlag.D)]
        [InlineData(OP.CLI_IMP, PFlag.I)]
        [InlineData(OP.CLV_IMP, PFlag.V)]
        public void ShouldClearFlag(byte op, PFlag flag)
        {
            _cpu.Ram[0] = op;
            _cpu.SetFlag(flag);

            _cpu.Step();

            FlagAssert.Cleared(_cpu, flag);
        }
    }
}
