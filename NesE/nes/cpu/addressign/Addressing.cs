namespace NesE.nes.cpu.addressign
{
    public class Addressing
    {
        public readonly BaseAddressAccessor Implied;
        public readonly BaseAddressAccessor Accumulator;
        public readonly BaseAddressAccessor Immediate;
        public readonly BaseAddressAccessor Absolute;
        public readonly BaseAddressAccessor AbsoluteX;
        public readonly BaseAddressAccessor AbsoluteY;
        public readonly BaseAddressAccessor IndirectX;
        public readonly BaseAddressAccessor IndirectY;
        public readonly BaseAddressAccessor ZeroPage;
        public readonly BaseAddressAccessor ZeroPageX;
        public readonly BaseAddressAccessor ZeroPageY;
        public readonly BaseAddressAccessor XRegister;
        public readonly BaseAddressAccessor YRegister;
        public readonly BaseAddressAccessor Status;
        public readonly BaseAddressAccessor StackPointer;

        public Addressing(CPU cpu)
        {
            Implied = new Implied(cpu);
            Accumulator = new Accumulator(cpu);
            Immediate = new Immediate(cpu);
            Absolute = new AddressAccessor(cpu, new Absolute());
            AbsoluteX = new AddressAccessor(cpu, new AbsoluteX());
            AbsoluteY = new AddressAccessor(cpu, new AbsoluteY());
            IndirectX = new AddressAccessor(cpu, new IndirectX());
            IndirectY = new AddressAccessor(cpu, new IndirectY());
            ZeroPage = new AddressAccessor(cpu, new ZeroPage());
            ZeroPageX = new AddressAccessor(cpu, new ZeroPageX());
            ZeroPageY = new AddressAccessor(cpu, new ZeroPageY());
            XRegister = new XRegister(cpu);
            YRegister = new YRegister(cpu);
            Status = new Status(cpu);
            StackPointer = new StackPointer(cpu);
        }
    }
}
