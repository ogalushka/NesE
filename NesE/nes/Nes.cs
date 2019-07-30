using NesE.nes.cpu;
using System;
using System.Collections.Generic;
using System.Text;

namespace NesE.nes
{
    class Nes
    {
        private CPU _cpu;
        private RAM _ram;
        private Clock _clock;

        public Nes()
        {
            _ram = new RAM();
            _cpu = new CPU(_ram);
            _clock = new Clock();
            _clock.CPUStep += StepCPU;
            _clock.PPUStep += StepPPU;
        }

        public double TimeToNextTick => _clock.TimeToNextTick;

        public void Update(double dt)
        {
            _clock.Update(dt);
        }

        private void StepCPU()
        {
            _cpu.Step();
        }

        private void StepPPU()
        {
        }
    }
}
