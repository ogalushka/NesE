using NesE.nes.cpu.addressign;
using NesE.nes.cpu.opcode;

namespace NesE.nes.cpu
{
    public class OpCode
    {
        public readonly Operation ADC;
        public readonly Operation SBC;
        public readonly Operation AND;
        public readonly Operation EOR;
        public readonly Operation ORA;
        public readonly Operation ROL;
        public readonly Operation ROR;
        public readonly Operation ASL;
        public readonly Operation LSR;
        public readonly Operation BCC;
        public readonly Operation BCS;
        public readonly Operation BEQ;
        public readonly Operation BNE;
        public readonly Operation BMI;
        public readonly Operation BPL;
        public readonly Operation BVS;
        public readonly Operation BVC;
        public readonly Operation BIT;
        public readonly Operation BRK;
        public readonly Operation CLC;
        public readonly Operation SEC;
        public readonly Operation CLD;
        public readonly Operation SED;
        public readonly Operation CLI;
        public readonly Operation SEI;
        public readonly Operation CLV;
        public readonly Operation CMP;
        public readonly Operation CPX;
        public readonly Operation CPY;
        public readonly Operation DEC;
        public readonly Operation INC;
        public readonly Operation LDA;
        public readonly Operation LDX;
        public readonly Operation LDY;
        public readonly Operation NOP;
        public readonly Operation PSH;
        public readonly Operation PLP;
        public readonly Operation PLA;
        public readonly Operation STA;
        public readonly Operation STX;
        public readonly Operation STY;
        public readonly Operation TAX;
        public readonly Operation TAY;
        public readonly Operation TSX;
        public readonly Operation TXA;
        public readonly Operation TXS;
        public readonly Operation TYA;

        public OpCode(CPU cpu)
        {
            ADC = new ADC(cpu);
            SBC = new SBC(cpu);
            AND = new AND(cpu);
            EOR = new EOR(cpu);
            ORA = new ORA(cpu);
            ROL = new ROL(cpu);
            ROR = new ROR(cpu);
            ASL = new ASL(cpu);
            LSR = new LSR(cpu);
            BCC = new BranchOnFlagClear(cpu, PFlag.C);
            BCS = new BranchOnFlagSet(cpu, PFlag.C);
            BEQ = new BranchOnFlagSet(cpu, PFlag.Z);
            BNE = new BranchOnFlagClear(cpu, PFlag.Z);
            BMI = new BranchOnFlagSet(cpu, PFlag.N);
            BPL = new BranchOnFlagClear(cpu, PFlag.N);
            BVS = new BranchOnFlagSet(cpu, PFlag.V);
            BVC = new BranchOnFlagClear(cpu, PFlag.V);
            BIT = new BIT(cpu);
            BRK = new BRK(cpu);
            CLC = new FlagClear(cpu, PFlag.C);
            SEC = new FlagSet(cpu, PFlag.C);
            CLD = new FlagClear(cpu, PFlag.D);
            SED = new FlagSet(cpu, PFlag.D);
            CLI = new FlagClear(cpu, PFlag.I);
            SEI = new FlagSet(cpu, PFlag.I);
            CLV = new FlagClear(cpu, PFlag.V);
            CMP = new Compare(cpu, new Accumulator(cpu));
            CPX = new Compare(cpu, new XRegister(cpu));
            CPY = new Compare(cpu, new YRegister(cpu));
            DEC = new Decrement(cpu);
            INC = new Increment(cpu);
            LDA = new Load(cpu, new Accumulator(cpu));
            LDX = new Load(cpu, new XRegister(cpu));
            LDY = new Load(cpu, new YRegister(cpu));
            NOP = new NOP(cpu);
            PSH = new PushRegister(cpu);
            PLP = new PullRegister(cpu, false);
            PLA = new PullRegister(cpu, true);
            STA = new Transfer(cpu, new Accumulator(cpu), false);
            STX = new Transfer(cpu, new XRegister(cpu), false);
            STY = new Transfer(cpu, new YRegister(cpu), false);
            TAX = new Transfer(cpu, new Accumulator(cpu), true);
            TAY = new Transfer(cpu, new Accumulator(cpu), true);
            TSX = new Transfer(cpu, new StackPointer(cpu), true);
            TXA = new Transfer(cpu, new XRegister(cpu), true);
            TXS = new Transfer(cpu, new XRegister(cpu), false);
            TYA = new Transfer(cpu, new YRegister(cpu), true);
        }
    }
}
