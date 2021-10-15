using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeyboardCapture
{
    internal class KeyState
    {
        public KeyState(Keys key, State state = State.None)
        {
            this.keyInfo = key;
            this.state = state;
        }

        public State state;
        public Keys keyInfo;
    }
}
