using NesE.nes.cpu.opcode;

namespace NesE.nes.cpu
{
    public static class OpCode
    {
        public static IOpCode ADC = new ADC();
        public static IOpCode SBC = new SBC();
        public static IOpCode AND = new AND();
        public static IOpCode EOR = new EOR();
        public static IOpCode ORA = new ORA();
        public static IOpCode ROL = new ROL();
        public static IOpCode ROR = new ROR();
        public static IOpCode ASL = new ASL();
        public static IOpCode LSR = new LSR();
        public static IOpCode BCC = new BranchOnFlagClear(PFlag.C);
        public static IOpCode BCS = new BranchOnFlagSet(PFlag.C);
        public static IOpCode BEQ = new BranchOnFlagSet(PFlag.Z);
        public static IOpCode BNE = new BranchOnFlagClear(PFlag.Z);
        public static IOpCode BMI = new BranchOnFlagSet(PFlag.N);
        public static IOpCode BPL = new BranchOnFlagClear(PFlag.N);
        public static IOpCode BVS = new BranchOnFlagSet(PFlag.V);
        public static IOpCode BVC = new BranchOnFlagClear(PFlag.V);
        public static IOpCode BIT = new BIT();
        public static IOpCode BRK = new BRK();
        public static IOpCode CLC = new FlagClear(PFlag.C);
        public static IOpCode SEC = new FlagSet(PFlag.C);
        public static IOpCode CLD = new FlagClear(PFlag.D);
        public static IOpCode SED = new FlagSet(PFlag.D);
        public static IOpCode CLI = new FlagClear(PFlag.I);
        public static IOpCode SEI = new FlagSet(PFlag.I);
        public static IOpCode CLV = new FlagClear(PFlag.V);
        public static IOpCode CMP = new Compare(cpu => cpu.A);
        public static IOpCode CPX = new Compare(cpu => cpu.X);
        public static IOpCode CPY = new Compare(cpu => cpu.Y);
        public static IOpCode DEC = new DEC();
    }
}
