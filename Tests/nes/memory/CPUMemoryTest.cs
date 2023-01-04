using NesE.nes.memory;
using Xunit;

namespace Tests.nes.memory
{
    public class CPUMemoryTest
    {
        [Fact]
        public void ShouldMirrorInternalRam()
        {
            var node = new CPUMemory();

            node[1] = 14;

            Assert.Equal(node[1], node[0x801]);
            Assert.Equal(node[1], node[0x1001]);
            Assert.Equal(node[1], node[0x1801]);
        }

        [Fact]
        public void ShouldMirrorPPURegisters()
        {
            var node = new CPUMemory();

            node[0x2000] = 14;

            for (ushort i = 0x2008; i < 0x4000; i += 0x8)
            {
                Assert.Equal(node[0x2000], node[i]);
            }
        }

        [Fact]
        public void ShouldAddAddressSpace()
        {
            var node = new CPUMemory();
            var memory = new byte[0x2000];
            memory[1] = 14;
            node.AddAddressSpace(3, 0b0001_1111_1111_1111, memory);

            Assert.Equal(14, node[0x6001]);
        }
    }
}
