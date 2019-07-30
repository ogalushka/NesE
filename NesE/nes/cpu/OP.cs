namespace NesE.nes.cpu
{
    public static class OP
    {
        public const byte ADC_IMM = 0x69;
        public const byte ADC_ZEP = 0x65;
        public const byte ADC_ZPX = 0x75;
        public const byte ADC_ABS = 0x6D;
        public const byte ADC_ABX = 0x7D;
        public const byte ADC_ABY = 0x79;
        public const byte ADC_IDX = 0x61;
        public const byte ADC_IDY = 0x71;

        public const byte SBC_IMM = 0xE9;
        public const byte SBC_ZEP = 0xE5;
        public const byte SBC_ZPX = 0xF5;
        public const byte SBC_ABS = 0xED;
        public const byte SBC_ABX = 0xFD;
        public const byte SBC_ABY = 0xF9;
        public const byte SBC_IDX = 0xE1;
        public const byte SBC_IDY = 0xF1;

        public const byte AND_IMM = 0x29;
        public const byte AND_ZEP = 0x25;
        public const byte AND_ZPX = 0x35;
        public const byte AND_ABS = 0x2D;
        public const byte AND_ABX = 0x3D;
        public const byte AND_ABY = 0x39;
        public const byte AND_IDX = 0x21;
        public const byte AND_IDY = 0x31;

        public const byte EOR_IMM = 0x49;
        public const byte EOR_ZEP = 0x45;
        public const byte EOR_ZPX = 0x55;
        public const byte EOR_ABS = 0x4D;
        public const byte EOR_ABX = 0x5D;
        public const byte EOR_ABY = 0x59;
        public const byte EOR_IDX = 0x41;
        public const byte EOR_IDY = 0x51;

        public const byte ORA_IMM = 0x09;
        public const byte ORA_ZEP = 0x05;
        public const byte ORA_ZPX = 0x15;
        public const byte ORA_ABS = 0x0D;
        public const byte ORA_ABX = 0x1D;
        public const byte ORA_ABY = 0x19;
        public const byte ORA_IDX = 0x01;
        public const byte ORA_IDY = 0x11;

        public const byte ROL_ACC = 0x2A;
        public const byte ROL_ZEP = 0x26;
        public const byte ROL_ZPX = 0x36;
        public const byte ROL_ABS = 0x2E;
        public const byte ROL_ABX = 0x3E;

        public const byte ROR_ACC = 0x6A;
        public const byte ROR_ZEP = 0x66;
        public const byte ROR_ZPX = 0x76;
        public const byte ROR_ABS = 0x6E;
        public const byte ROR_ABX = 0x7E;

        public const byte ASL_ACC = 0x0A;
        public const byte ASL_ZEP = 0x06;
        public const byte ASL_ZPX = 0x16;
        public const byte ASL_ABS = 0x0E;
        public const byte ASL_ABX = 0x1E;

        public const byte LSR_ACC = 0x4A;
        public const byte LSR_ZEP = 0x46;
        public const byte LSR_ZPX = 0x56;
        public const byte LSR_ABS = 0x4E;
        public const byte LSR_ABX = 0x5E;

        public const byte BCC_REL = 0x90;
        public const byte BCS_REL = 0xB0;
        public const byte BEQ_REL = 0xF0;
        public const byte BNE_REL = 0xD0;
        public const byte BMI_REL = 0x30;
        public const byte BPL_REL = 0x10;
        public const byte BVS_REL = 0x70;
        public const byte BVC_REL = 0x50;

        public const byte BIT_ZEP = 0x24;
        public const byte BIT_ABS = 0x2C;

        public const byte BRK_IMP = 0x00;

        public const byte CLC_IMP = 0x18;
        public const byte SEC_IMP = 0x38;

        public const byte CLD_IMP = 0xD8;
        public const byte SED_IMP = 0xF8;

        public const byte CLI_IMP = 0x58;
        public const byte SEI_IMP = 0x78;

        public const byte CLV_IMP = 0xB8;

        public const byte CMP_IMM = 0xC9;
        public const byte CMP_ZEP = 0xC5;
        public const byte CMP_ZPX = 0xD5;
        public const byte CMP_ABS = 0xCD;
        public const byte CMP_ABX = 0xDD;
        public const byte CMP_ABY = 0xD9;
        public const byte CMP_IDX = 0xC1;
        public const byte CMP_IDY = 0xD1;

        public const byte CPX_IMM = 0xE0;
        public const byte CPX_ZEP = 0xE4;
        public const byte CPX_ABS = 0xEC;

        public const byte CPY_IMM = 0xC0;
        public const byte CPY_ZEP = 0xC4;
        public const byte CPY_ABS = 0xCC;

        public const byte DEC_ZEP = 0xC6;
        public const byte DEC_ZPX = 0xD6;
        public const byte DEC_ABS = 0xCE;
        public const byte DEC_ABX = 0xDE;
    }
}
