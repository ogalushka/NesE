namespace NesE.nes.memory
{
    public interface IMemory
    {
        //void Set(int index, byte value);
        //byte Get(int index);
        byte this[int index] {
            get;
            set;
        }
    }
}
