using System;
using System.Diagnostics;

namespace NesE.nes
{
    public class Clock
    {
        private const double Frequency = (236 / 11) * 1000 * 1000; //NTSC
        private const int CPUDivider = 12;
        private const int PPUDivider = 4;

        private double _currentTime = 0;
        private double _timePerTick;
        private int _currentCPUDivider;
        private int _currentPPUDivider;

        public event Action CPUStep;
        public event Action PPUStep;

        public Clock()
        {
            _timePerTick = 1 / Frequency;
            _currentCPUDivider = CPUDivider;
            _currentPPUDivider = PPUDivider;
        }

        public double TimeToNextTick => _timePerTick * Math.Min(_currentCPUDivider, _currentPPUDivider) - _currentTime;

        public void Update(double dt)
        {
            var delta = dt + _currentTime;
            var tickCount = (int)Math.Floor(delta / _timePerTick);
            _currentTime = delta - (tickCount * _timePerTick);
            for (var i = 0; i < tickCount; i++)
            {
                Tick();
            }
        }

        private void Tick()
        {
            _currentCPUDivider--;
            _currentPPUDivider--;
            if (_currentCPUDivider <= 0)
            {
                CPUStep();
                _currentCPUDivider = CPUDivider;
            }

            if (_currentPPUDivider <= 0)
            {
                PPUStep();
                _currentPPUDivider = PPUDivider;
            }
        }
    }
}
