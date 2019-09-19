namespace NesE.nes.rom.mapper
{
    public class BaseMapper
    {
        private readonly ROM _rom;

        public BaseMapper(ROM rom)
        {
            _rom = rom;
        }

        public bool InMapRange(ushort address)
        {
            return false;
        }
    }
}
