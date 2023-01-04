using NesE.nes;
using NesE.nes.cpu;

namespace Tests.nes.cpu
{
    public class BaseCPUTest
    {
        protected readonly CPU CPU;
        public BaseCPUTest()
        {
            CPU = new CPU(new TestRAM(), new Interupts());
        }
    }
}
