using NesE.nes.cpu.addressign;
using NesE.nes.cpu.opcode;
using System.Collections.Generic;

namespace NesE.nes.cpu
{
    public class Instructions
    {
        private readonly Dictionary<int, Instruction> instructions;
        public readonly Instruction IRQ;
        public readonly Instruction UnmaskedIRQ;
        
        public Instruction Get(byte opcode)
        {
            return instructions[opcode];
        }

        public Instructions(CPU cpu)
        {
            var opCodes = new OpCode(cpu);
            var addressing = new Addressing(cpu);

            IRQ = new Instruction(new IRQ(cpu, 0xFFFE, 0xFFFF), addressing.Implied);
            UnmaskedIRQ = new Instruction(new IRQ(cpu, 0xFFFA, 0xFFFB), addressing.Implied);

            instructions = new Dictionary<int, Instruction>
            {
                { OP.ADC_IMM, new Instruction(opCodes.ADC, addressing.Immediate) },
                { OP.ADC_ABS, new Instruction(opCodes.ADC, addressing.Absolute) },
                { OP.ADC_ABX, new Instruction(opCodes.ADC, addressing.AbsoluteX) },
                { OP.ADC_ABY, new Instruction(opCodes.ADC, addressing.AbsoluteY) },
                { OP.ADC_IDX, new Instruction(opCodes.ADC, addressing.IndirectX) },
                { OP.ADC_IDY, new Instruction(opCodes.ADC, addressing.IndirectY) },
                { OP.ADC_ZEP, new Instruction(opCodes.ADC, addressing.ZeroPage) },
                { OP.ADC_ZPX, new Instruction(opCodes.ADC, addressing.ZeroPageX) },

                { OP.SBC_IMM, new Instruction(opCodes.SBC, addressing.Immediate) },
                { OP.SBC_ABS, new Instruction(opCodes.SBC, addressing.Absolute) },
                { OP.SBC_ABX, new Instruction(opCodes.SBC, addressing.AbsoluteX) },
                { OP.SBC_ABY, new Instruction(opCodes.SBC, addressing.AbsoluteY) },
                { OP.SBC_IDX, new Instruction(opCodes.SBC, addressing.IndirectX) },
                { OP.SBC_IDY, new Instruction(opCodes.SBC, addressing.IndirectY) },
                { OP.SBC_ZEP, new Instruction(opCodes.SBC, addressing.ZeroPage) },
                { OP.SBC_ZPX, new Instruction(opCodes.SBC, addressing.ZeroPageX) },

                { OP.AND_IMM, new Instruction(opCodes.AND, addressing.Immediate) },
                { OP.AND_ABS, new Instruction(opCodes.AND, addressing.Absolute) },
                { OP.AND_ABX, new Instruction(opCodes.AND, addressing.AbsoluteX) },
                { OP.AND_ABY, new Instruction(opCodes.AND, addressing.AbsoluteY) },
                { OP.AND_IDX, new Instruction(opCodes.AND, addressing.IndirectX) },
                { OP.AND_IDY, new Instruction(opCodes.AND, addressing.IndirectY) },
                { OP.AND_ZEP, new Instruction(opCodes.AND, addressing.ZeroPage) },
                { OP.AND_ZPX, new Instruction(opCodes.AND, addressing.ZeroPageX) },

                { OP.EOR_IMM, new Instruction(opCodes.EOR, addressing.Immediate) },
                { OP.EOR_ABS, new Instruction(opCodes.EOR, addressing.Absolute) },
                { OP.EOR_ABX, new Instruction(opCodes.EOR, addressing.AbsoluteX) },
                { OP.EOR_ABY, new Instruction(opCodes.EOR, addressing.AbsoluteY) },
                { OP.EOR_IDX, new Instruction(opCodes.EOR, addressing.IndirectX) },
                { OP.EOR_IDY, new Instruction(opCodes.EOR, addressing.IndirectY) },
                { OP.EOR_ZEP, new Instruction(opCodes.EOR, addressing.ZeroPage) },
                { OP.EOR_ZPX, new Instruction(opCodes.EOR, addressing.ZeroPageX) },

                { OP.ORA_IMM, new Instruction(opCodes.ORA, addressing.Immediate) },
                { OP.ORA_ABS, new Instruction(opCodes.ORA, addressing.Absolute) },
                { OP.ORA_ABX, new Instruction(opCodes.ORA, addressing.AbsoluteX) },
                { OP.ORA_ABY, new Instruction(opCodes.ORA, addressing.AbsoluteY) },
                { OP.ORA_IDX, new Instruction(opCodes.ORA, addressing.IndirectX) },
                { OP.ORA_IDY, new Instruction(opCodes.ORA, addressing.IndirectY) },
                { OP.ORA_ZEP, new Instruction(opCodes.ORA, addressing.ZeroPage) },
                { OP.ORA_ZPX, new Instruction(opCodes.ORA, addressing.ZeroPageX) },

                { OP.ROL_ACC, new Instruction(opCodes.ROL, addressing.Accumulator) },
                { OP.ROL_ABS, new Instruction(opCodes.ROL, addressing.Absolute) },
                { OP.ROL_ABX, new Instruction(opCodes.ROL, addressing.AbsoluteX) },
                { OP.ROL_ZEP, new Instruction(opCodes.ROL, addressing.ZeroPage) },
                { OP.ROL_ZPX, new Instruction(opCodes.ROL, addressing.ZeroPageX) },

                { OP.ROR_ACC, new Instruction(opCodes.ROR, addressing.Accumulator) },
                { OP.ROR_ABS, new Instruction(opCodes.ROR, addressing.Absolute) },
                { OP.ROR_ABX, new Instruction(opCodes.ROR, addressing.AbsoluteX) },
                { OP.ROR_ZEP, new Instruction(opCodes.ROR, addressing.ZeroPage) },
                { OP.ROR_ZPX, new Instruction(opCodes.ROR, addressing.ZeroPageX) },

                { OP.ASL_ACC, new Instruction(opCodes.ASL, addressing.Accumulator) },
                { OP.ASL_ABS, new Instruction(opCodes.ASL, addressing.Absolute) },
                { OP.ASL_ABX, new Instruction(opCodes.ASL, addressing.AbsoluteX) },
                { OP.ASL_ZEP, new Instruction(opCodes.ASL, addressing.ZeroPage) },
                { OP.ASL_ZPX, new Instruction(opCodes.ASL, addressing.ZeroPageX) },

                { OP.LSR_ACC, new Instruction(opCodes.LSR, addressing.Accumulator) },
                { OP.LSR_ABS, new Instruction(opCodes.LSR, addressing.Absolute) },
                { OP.LSR_ABX, new Instruction(opCodes.LSR, addressing.AbsoluteX) },
                { OP.LSR_ZEP, new Instruction(opCodes.LSR, addressing.ZeroPage) },
                { OP.LSR_ZPX, new Instruction(opCodes.LSR, addressing.ZeroPageX) },

                { OP.BIT_ZEP, new Instruction(opCodes.BIT, addressing.ZeroPage) },
                { OP.BIT_ABS, new Instruction(opCodes.BIT, addressing.Absolute) },

                { OP.BCC_REL, new Instruction(opCodes.BCC, addressing.Immediate) },
                { OP.BCS_REL, new Instruction(opCodes.BCS, addressing.Immediate) },
                { OP.BEQ_REL, new Instruction(opCodes.BEQ, addressing.Immediate) },
                { OP.BNE_REL, new Instruction(opCodes.BNE, addressing.Immediate) },
                { OP.BMI_REL, new Instruction(opCodes.BMI, addressing.Immediate) },
                { OP.BPL_REL, new Instruction(opCodes.BPL, addressing.Immediate) },
                { OP.BVC_REL, new Instruction(opCodes.BVC, addressing.Immediate) },
                { OP.BVS_REL, new Instruction(opCodes.BVS, addressing.Immediate) },

                { OP.BRK_IMP, new Instruction(opCodes.BRK, addressing.Implied) },

                { OP.CLC_IMP, new Instruction(opCodes.CLC, addressing.Implied) },
                { OP.SEC_IMP, new Instruction(opCodes.SEC, addressing.Implied) },
                { OP.CLD_IMP, new Instruction(opCodes.CLD, addressing.Implied) },
                { OP.SED_IMP, new Instruction(opCodes.SED, addressing.Implied) },
                { OP.CLI_IMP, new Instruction(opCodes.CLI, addressing.Implied) },
                { OP.SEI_IMP, new Instruction(opCodes.SEI, addressing.Implied) },
                { OP.CLV_IMP, new Instruction(opCodes.CLV, addressing.Implied) },

                { OP.CMP_IMM, new Instruction(opCodes.CMP, addressing.Immediate) },
                { OP.CMP_ABS, new Instruction(opCodes.CMP, addressing.Absolute) },
                { OP.CMP_ABX, new Instruction(opCodes.CMP, addressing.AbsoluteX) },
                { OP.CMP_ABY, new Instruction(opCodes.CMP, addressing.AbsoluteY) },
                { OP.CMP_IDX, new Instruction(opCodes.CMP, addressing.IndirectX) },
                { OP.CMP_IDY, new Instruction(opCodes.CMP, addressing.IndirectY) },
                { OP.CMP_ZEP, new Instruction(opCodes.CMP, addressing.ZeroPage) },
                { OP.CMP_ZPX, new Instruction(opCodes.CMP, addressing.ZeroPageX) },

                { OP.CPX_IMM, new Instruction(opCodes.CPX, addressing.Immediate) },
                { OP.CPX_ZEP, new Instruction(opCodes.CPX, addressing.ZeroPage) },
                { OP.CPX_ABS, new Instruction(opCodes.CPX, addressing.Absolute) },

                { OP.CPY_IMM, new Instruction(opCodes.CPY, addressing.Immediate) },
                { OP.CPY_ZEP, new Instruction(opCodes.CPY, addressing.ZeroPage) },
                { OP.CPY_ABS, new Instruction(opCodes.CPY, addressing.Absolute) },

                { OP.DEC_ZEP, new Instruction(opCodes.DEC, addressing.ZeroPage) },
                { OP.DEC_ZPX, new Instruction(opCodes.DEC, addressing.ZeroPageX) },
                { OP.DEC_ABS, new Instruction(opCodes.DEC, addressing.Absolute) },
                { OP.DEC_ABX, new Instruction(opCodes.DEC, addressing.AbsoluteX) },

                { OP.DEX_IMP, new Instruction(opCodes.DEC, addressing.XRegister) },
                { OP.DEY_IMP, new Instruction(opCodes.DEC, addressing.YRegister) },

                { OP.INC_ZEP, new Instruction(opCodes.INC, addressing.ZeroPage) },
                { OP.INC_ZPX, new Instruction(opCodes.INC, addressing.ZeroPageX) },
                { OP.INC_ABS, new Instruction(opCodes.INC, addressing.Absolute) },
                { OP.INC_ABX, new Instruction(opCodes.INC, addressing.AbsoluteX) },

                { OP.INX_IMP, new Instruction(opCodes.INC, addressing.XRegister) },
                { OP.INY_IMP, new Instruction(opCodes.INC, addressing.YRegister) },

                { OP.JMP_ABS, new Instruction(new JMP(cpu, new Absolute()), addressing.Implied) },
                { OP.JMP_IND, new Instruction(new JMP(cpu, new IndirectNoCarry()), addressing.Implied) },

                { OP.JSR_ABS, new Instruction(new JSR(cpu), addressing.Implied) },
                { OP.RTS_IMP, new Instruction(new RTS(cpu), addressing.Implied) },
                { OP.RTI_IMP, new Instruction(new RTI(cpu), addressing.Implied) },

                { OP.LDA_IMM, new Instruction(opCodes.LDA, addressing.Immediate) },
                { OP.LDA_ZEP, new Instruction(opCodes.LDA, addressing.ZeroPage) },
                { OP.LDA_ZPX, new Instruction(opCodes.LDA, addressing.ZeroPageX) },
                { OP.LDA_ABS, new Instruction(opCodes.LDA, addressing.Absolute) },
                { OP.LDA_ABX, new Instruction(opCodes.LDA, addressing.AbsoluteX) },
                { OP.LDA_ABY, new Instruction(opCodes.LDA, addressing.AbsoluteY) },
                { OP.LDA_IDX, new Instruction(opCodes.LDA, addressing.IndirectX) },
                { OP.LDA_IDY, new Instruction(opCodes.LDA, addressing.IndirectY) },
                { OP.LDX_IMM, new Instruction(opCodes.LDX, addressing.Immediate) },
                { OP.LDX_ZEP, new Instruction(opCodes.LDX, addressing.ZeroPage) },
                { OP.LDX_ZPY, new Instruction(opCodes.LDX, addressing.ZeroPageY) },
                { OP.LDX_ABS, new Instruction(opCodes.LDX, addressing.Absolute) },
                { OP.LDX_ABY, new Instruction(opCodes.LDX, addressing.AbsoluteY) },
                { OP.LDY_IMM, new Instruction(opCodes.LDY, addressing.Immediate) },
                { OP.LDY_ZEP, new Instruction(opCodes.LDY, addressing.ZeroPage) },
                { OP.LDY_ZPX, new Instruction(opCodes.LDY, addressing.ZeroPageX) },
                { OP.LDY_ABS, new Instruction(opCodes.LDY, addressing.Absolute) },
                { OP.LDY_ABX, new Instruction(opCodes.LDY, addressing.AbsoluteX) },

                { OP.NOP_IMP, new Instruction(opCodes.NOP, addressing.Implied) },

                { OP.PHA_IMP, new Instruction(opCodes.PSH, addressing.Accumulator) },
                { OP.PHP_IMP, new Instruction(opCodes.PSH, addressing.Status) },
                { OP.PLA_IMP, new Instruction(opCodes.PLA, addressing.Accumulator) },
                { OP.PLP_IMP, new Instruction(opCodes.PLP, addressing.Status) },

                { OP.STA_ZEP, new Instruction(opCodes.STA, addressing.ZeroPage) },
                { OP.STA_ZPX, new Instruction(opCodes.STA, addressing.ZeroPageX) },
                { OP.STA_ABS, new Instruction(opCodes.STA, addressing.Absolute) },
                { OP.STA_ABX, new Instruction(opCodes.STA, addressing.AbsoluteX) },
                { OP.STA_ABY, new Instruction(opCodes.STA, addressing.AbsoluteY) },
                { OP.STA_IDX, new Instruction(opCodes.STA, addressing.IndirectX) },
                { OP.STA_IDY, new Instruction(opCodes.STA, addressing.IndirectY) },

                { OP.STX_ZEP, new Instruction(opCodes.STX, addressing.ZeroPage) },
                { OP.STX_ZPY, new Instruction(opCodes.STX, addressing.ZeroPageY) },
                { OP.STX_ABS, new Instruction(opCodes.STX, addressing.Absolute) },

                { OP.STY_ZEP, new Instruction(opCodes.STY, addressing.ZeroPage) },
                { OP.STY_ZPX, new Instruction(opCodes.STY, addressing.ZeroPageX) },
                { OP.STY_ABS, new Instruction(opCodes.STY, addressing.Absolute) },

                { OP.TAX_IMP, new Instruction(opCodes.TAX, addressing.XRegister) },
                { OP.TAY_IMP, new Instruction(opCodes.TAY, addressing.YRegister) },
                { OP.TSX_IMP, new Instruction(opCodes.TSX, addressing.XRegister) },
                { OP.TXA_IMP, new Instruction(opCodes.TXA, addressing.Accumulator) },
                { OP.TXS_IMP, new Instruction(opCodes.TXS, addressing.StackPointer) },
                { OP.TYA_IMP, new Instruction(opCodes.TYA, addressing.Accumulator) },
            };
        }
    }
}
