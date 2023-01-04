using NesE.nes;

namespace Tests.nes.teststubs
{
    public class OpWriter
    {
        private NES _nes;
        private ushort _counter;

        public ushort Counter => _counter;

        public OpWriter(NES nes)
        {
            _nes = nes;
        }

        public void PushUShort(ushort u)
        {
            PushByte((byte)u); //low
            PushByte((byte)(u >> 8)); //high
        }

        public void PushByte(byte b)
        {
            _nes.CPU.RAM[_counter++] = b;
        }

        public void Reset()
        {
            _counter = 0;
        }
    }
}
