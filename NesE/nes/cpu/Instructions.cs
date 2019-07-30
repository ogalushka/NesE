using NesE.nes.cpu.addressign;
using NesE.nes.cpu.opcode;
using System.Collections.Generic;

namespace NesE.nes.cpu
{
    public static class Instructions
    {
        public static Instruction Get(byte opcode)
        {
            return instructions[opcode];
        }
        private static readonly Dictionary<int, Instruction> instructions = new Dictionary<int, Instruction>
        {
            { OP.ADC_IMM, new Instruction(OpCode.ADC, Addressing.Immediate) },
            { OP.ADC_ABS, new Instruction(OpCode.ADC, Addressing.Absolute) },
            { OP.ADC_ABX, new Instruction(OpCode.ADC, Addressing.AbsoluteX) },
            { OP.ADC_ABY, new Instruction(OpCode.ADC, Addressing.AbsoluteY) },
            { OP.ADC_IDX, new Instruction(OpCode.ADC, Addressing.IndirectX) },
            { OP.ADC_IDY, new Instruction(OpCode.ADC, Addressing.IndirectY) },
            { OP.ADC_ZEP, new Instruction(OpCode.ADC, Addressing.ZeroPage) },
            { OP.ADC_ZPX, new Instruction(OpCode.ADC, Addressing.ZeroPageX) },

            { OP.SBC_IMM, new Instruction(OpCode.SBC, Addressing.Immediate) },
            { OP.SBC_ABS, new Instruction(OpCode.SBC, Addressing.Absolute) },
            { OP.SBC_ABX, new Instruction(OpCode.SBC, Addressing.AbsoluteX) },
            { OP.SBC_ABY, new Instruction(OpCode.SBC, Addressing.AbsoluteY) },
            { OP.SBC_IDX, new Instruction(OpCode.SBC, Addressing.IndirectX) },
            { OP.SBC_IDY, new Instruction(OpCode.SBC, Addressing.IndirectY) },
            { OP.SBC_ZEP, new Instruction(OpCode.SBC, Addressing.ZeroPage) },
            { OP.SBC_ZPX, new Instruction(OpCode.SBC, Addressing.ZeroPageX) },

            { OP.AND_IMM, new Instruction(OpCode.AND, Addressing.Immediate) },
            { OP.AND_ABS, new Instruction(OpCode.AND, Addressing.Absolute) },
            { OP.AND_ABX, new Instruction(OpCode.AND, Addressing.AbsoluteX) },
            { OP.AND_ABY, new Instruction(OpCode.AND, Addressing.AbsoluteY) },
            { OP.AND_IDX, new Instruction(OpCode.AND, Addressing.IndirectX) },
            { OP.AND_IDY, new Instruction(OpCode.AND, Addressing.IndirectY) },
            { OP.AND_ZEP, new Instruction(OpCode.AND, Addressing.ZeroPage) },
            { OP.AND_ZPX, new Instruction(OpCode.AND, Addressing.ZeroPageX) },

            { OP.EOR_IMM, new Instruction(OpCode.EOR, Addressing.Immediate) },
            { OP.EOR_ABS, new Instruction(OpCode.EOR, Addressing.Absolute) },
            { OP.EOR_ABX, new Instruction(OpCode.EOR, Addressing.AbsoluteX) },
            { OP.EOR_ABY, new Instruction(OpCode.EOR, Addressing.AbsoluteY) },
            { OP.EOR_IDX, new Instruction(OpCode.EOR, Addressing.IndirectX) },
            { OP.EOR_IDY, new Instruction(OpCode.EOR, Addressing.IndirectY) },
            { OP.EOR_ZEP, new Instruction(OpCode.EOR, Addressing.ZeroPage) },
            { OP.EOR_ZPX, new Instruction(OpCode.EOR, Addressing.ZeroPageX) },

            { OP.ORA_IMM, new Instruction(OpCode.ORA, Addressing.Immediate) },
            { OP.ORA_ABS, new Instruction(OpCode.ORA, Addressing.Absolute) },
            { OP.ORA_ABX, new Instruction(OpCode.ORA, Addressing.AbsoluteX) },
            { OP.ORA_ABY, new Instruction(OpCode.ORA, Addressing.AbsoluteY) },
            { OP.ORA_IDX, new Instruction(OpCode.ORA, Addressing.IndirectX) },
            { OP.ORA_IDY, new Instruction(OpCode.ORA, Addressing.IndirectY) },
            { OP.ORA_ZEP, new Instruction(OpCode.ORA, Addressing.ZeroPage) },
            { OP.ORA_ZPX, new Instruction(OpCode.ORA, Addressing.ZeroPageX) },

            { OP.ROL_ACC, new Instruction(OpCode.ROL, Addressing.Accumulator) },
            { OP.ROL_ABS, new Instruction(OpCode.ROL, Addressing.Absolute) },
            { OP.ROL_ABX, new Instruction(OpCode.ROL, Addressing.AbsoluteX) },
            { OP.ROL_ZEP, new Instruction(OpCode.ROL, Addressing.ZeroPage) },
            { OP.ROL_ZPX, new Instruction(OpCode.ROL, Addressing.ZeroPageX) },

            { OP.ROR_ACC, new Instruction(OpCode.ROR, Addressing.Accumulator) },
            { OP.ROR_ABS, new Instruction(OpCode.ROR, Addressing.Absolute) },
            { OP.ROR_ABX, new Instruction(OpCode.ROR, Addressing.AbsoluteX) },
            { OP.ROR_ZEP, new Instruction(OpCode.ROR, Addressing.ZeroPage) },
            { OP.ROR_ZPX, new Instruction(OpCode.ROR, Addressing.ZeroPageX) },

            { OP.ASL_ACC, new Instruction(OpCode.ASL, Addressing.Accumulator) },
            { OP.ASL_ABS, new Instruction(OpCode.ASL, Addressing.Absolute) },
            { OP.ASL_ABX, new Instruction(OpCode.ASL, Addressing.AbsoluteX) },
            { OP.ASL_ZEP, new Instruction(OpCode.ASL, Addressing.ZeroPage) },
            { OP.ASL_ZPX, new Instruction(OpCode.ASL, Addressing.ZeroPageX) },

            { OP.LSR_ACC, new Instruction(OpCode.LSR, Addressing.Accumulator) },
            { OP.LSR_ABS, new Instruction(OpCode.LSR, Addressing.Absolute) },
            { OP.LSR_ABX, new Instruction(OpCode.LSR, Addressing.AbsoluteX) },
            { OP.LSR_ZEP, new Instruction(OpCode.LSR, Addressing.ZeroPage) },
            { OP.LSR_ZPX, new Instruction(OpCode.LSR, Addressing.ZeroPageX) },

            { OP.BIT_ZEP, new Instruction(OpCode.BIT, Addressing.ZeroPage) },
            { OP.BIT_ABS, new Instruction(OpCode.BIT, Addressing.Absolute) },

            { OP.BCC_REL, new Instruction(OpCode.BCC, Addressing.Immediate) },
            { OP.BCS_REL, new Instruction(OpCode.BCS, Addressing.Immediate) },
            { OP.BEQ_REL, new Instruction(OpCode.BEQ, Addressing.Immediate) },
            { OP.BNE_REL, new Instruction(OpCode.BNE, Addressing.Immediate) },
            { OP.BMI_REL, new Instruction(OpCode.BMI, Addressing.Immediate) },
            { OP.BPL_REL, new Instruction(OpCode.BPL, Addressing.Immediate) },
            { OP.BVC_REL, new Instruction(OpCode.BVC, Addressing.Immediate) },
            { OP.BVS_REL, new Instruction(OpCode.BVS, Addressing.Immediate) },

            { OP.BRK_IMP, new Instruction(OpCode.BRK, Addressing.Implied) },

            { OP.CLC_IMP, new Instruction(OpCode.CLC, Addressing.Implied) },
            { OP.SEC_IMP, new Instruction(OpCode.SEC, Addressing.Implied) },
            { OP.CLD_IMP, new Instruction(OpCode.CLD, Addressing.Implied) },
            { OP.SED_IMP, new Instruction(OpCode.SED, Addressing.Implied) },
            { OP.CLI_IMP, new Instruction(OpCode.CLI, Addressing.Implied) },
            { OP.SEI_IMP, new Instruction(OpCode.SEI, Addressing.Implied) },
            { OP.CLV_IMP, new Instruction(OpCode.CLV, Addressing.Implied) },

            { OP.CMP_IMM, new Instruction(OpCode.CMP, Addressing.Immediate) },
            { OP.CMP_ABS, new Instruction(OpCode.CMP, Addressing.Absolute) },
            { OP.CMP_ABX, new Instruction(OpCode.CMP, Addressing.AbsoluteX) },
            { OP.CMP_ABY, new Instruction(OpCode.CMP, Addressing.AbsoluteY) },
            { OP.CMP_IDX, new Instruction(OpCode.CMP, Addressing.IndirectX) },
            { OP.CMP_IDY, new Instruction(OpCode.CMP, Addressing.IndirectY) },
            { OP.CMP_ZEP, new Instruction(OpCode.CMP, Addressing.ZeroPage) },
            { OP.CMP_ZPX, new Instruction(OpCode.CMP, Addressing.ZeroPageX) },

            { OP.CPX_IMM, new Instruction(OpCode.CPX, Addressing.Immediate) },
            { OP.CPX_ZEP, new Instruction(OpCode.CPX, Addressing.ZeroPage) },
            { OP.CPX_ABS, new Instruction(OpCode.CPX, Addressing.Absolute) },

            { OP.CPY_IMM, new Instruction(OpCode.CPY, Addressing.Immediate) },
            { OP.CPY_ZEP, new Instruction(OpCode.CPY, Addressing.ZeroPage) },
            { OP.CPY_ABS, new Instruction(OpCode.CPY, Addressing.Absolute) },

            { OP.DEC_ZEP, new Instruction(OpCode.DEC, Addressing.ZeroPage) },
            { OP.DEC_ZPX, new Instruction(OpCode.DEC, Addressing.ZeroPageX) },
            { OP.DEC_ABS, new Instruction(OpCode.DEC, Addressing.Absolute) },
            { OP.DEC_ABX, new Instruction(OpCode.DEC, Addressing.AbsoluteX) },
        };
    }
}
