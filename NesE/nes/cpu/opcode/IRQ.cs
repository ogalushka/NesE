using NesE.nes.cpu.addressign;

namespace NesE.nes.cpu.opcode
{
    public class IRQ : Operation
    {
        private readonly ushort _vectorPCL;
        private readonly ushort _vectorPCH;

        public IRQ(CPU cpu, ushort vectorPCL, ushort vectorPCH) : base(cpu)
        {
            _vectorPCL = vectorPCL;
            _vectorPCH = vectorPCH;
        }

        public override void Execute(BaseAddressAccessor accessor)
        {
            byte pcl = (byte)CPU.PC;
            byte pch = (byte)(CPU.PC >> 8);

            CPU.PutOnStack(pch);
            CPU.PutOnStack(pcl);
            CPU.PutOnStack((byte)CPU.P);

            var newPCL = CPU.Ram.Get(_vectorPCL);
            var newPCH = CPU.Ram.Get(_vectorPCH);

            CPU.PC = (ushort)(newPCH << 8 | newPCL);
            CPU.SetFlag(PFlag.I);
        }
    }
}
