using System;
using System.Collections.Generic;
using System.Text;

namespace NesE.nes.cpu
{
    // TODO https://wiki.nesdev.com/w/index.php/NMI
    public class Interupts
    {
        public bool IRQ = false;
        public bool NMI = false;
    }
}
