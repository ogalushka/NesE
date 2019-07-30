using System;
using System.Collections.Generic;
using System.Text;

namespace NesE.nes.cpu.addressign
{
    public static class Addressing
    {
        public static IAddressing Implied = new Implied();
        public static IAddressing Accumulator = new Accumulator();
        public static IAddressing Absolute = new Absolute();
        public static IAddressing AbsoluteX = new AbsoluteX();
        public static IAddressing AbsoluteY = new AbsoluteY();
        public static IAddressing Immediate = new Immediate();
        public static IAddressing IndirectX = new IndirectX();
        public static IAddressing IndirectY = new IndirectY();
        public static IAddressing ZeroPage = new ZeroPage();
        public static IAddressing ZeroPageX = new ZeroPageX();
    }
}
