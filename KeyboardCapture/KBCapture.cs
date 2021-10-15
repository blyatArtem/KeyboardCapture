using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KeyboardCapture
{
    public class KBCapture
    {
        [DllImport("user32.dll")]
        private static extern short GetKeyState(Keys key);

        /// <summary>
        /// Главный класс, в котором прописына логика перехвата.
        /// </summary>
        /// <param name="allKeys">allKeys отвечает за использование всех клавишей, помимо тех, которые используются для печати</param>
        public KBCapture(bool allKeys = false)
        {
            _keyList = new List<KeyState>();
            if (!allKeys)
            {
                for (int i = 48; i < 91; i++)
                {
                    _keyList.Add(new KeyState((Keys)i));
                }

                _keyList.Add(new KeyState((Keys)187));
                _keyList.Add(new KeyState((Keys)188));
                _keyList.Add(new KeyState((Keys)189));
                _keyList.Add(new KeyState((Keys)191));
                _keyList.Add(new KeyState((Keys)190));
                _keyList.Add(new KeyState((Keys)219));
                _keyList.Add(new KeyState((Keys)221));
                _keyList.Add(new KeyState((Keys)222));
                _keyList.Add(new KeyState((Keys)192));

                _keyList.Add(new KeyState((Keys)8));    //backspace
                _keyList.Add(new KeyState((Keys)13));   //enter
                _keyList.Add(new KeyState((Keys)20));   //caps
                _keyList.Add(new KeyState((Keys)32));   //space
                _keyList.Add(new KeyState((Keys)160));  //LShiftKey
                _keyList.Add(new KeyState((Keys)161));  //RShiftKey
                _keyList.Add(new KeyState((Keys)162));  //LCrtl
                _keyList.Add(new KeyState((Keys)163));  //RCtrl
                _keyList.Add(new KeyState((Keys)164));  //LAlt
                _keyList.Add(new KeyState((Keys)165));  //RAlt}
            }
            else
            {
                foreach (Keys key in (Keys[])Enum.GetValues(typeof(Keys)))
                {
                    _keyList.Add(new KeyState(key));
                }
            }
        }

        /// <summary>
        /// Запускает перехват клавиш.
        /// </summary>
        public void Start()
        {
            if (_thread == null || !_thread.IsAlive)
            {
                _thread = new Thread(Update);
                _thread.Start();
            }
        }

        /// <summary>
        /// Останавливает перехват клавиш.
        /// </summary>
        public void Stop()
        {
            if (_thread.IsAlive)
                _thread.Abort();
        }

        private void Update()
        {
            while (true)
            {
                foreach (KeyState key in _keyList)
                {
                    int down = GetKeyState(key.keyInfo) < 0 ? 1 : 0;
                    if (key.state == State.None && down == 1)
                    {
                        key.state = State.Pressed;
                        InvokePressedEvent(key.keyInfo);
                    }
                    else if (key.state == State.Down)
                    {
                        key.state = (State)down;
                        if (down == 0)
                        {
                            InvokeReleasedEvent(key.keyInfo);
                        }
                    }
                    else if (key.state == State.Pressed)
                        key.state = State.Down;
                }
                Thread.Sleep(_interval);
            }
        }

        /// <param name="key">Спец. символы: BACKSPACE, ENTER CAPS LOCK SPACE SHIFT CTRL ALT . / - + [ ] ' ; `</param>
        private void InvokePressedEvent(Keys key)
        {
            keyPressedWindowsForms?.Invoke(key);

            if (keyPressedString == null)
                return;
            if ((int)key == 8)
                keyPressedString.Invoke("{BACKSPACE}");
            else if ((int)key == 13)
                keyPressedString.Invoke("{ENTER}");
            else if ((int)key == 20)
                keyPressedString.Invoke("{CAPS}");
            else if ((int)key == 32)
                keyPressedString.Invoke("{SPACE}");
            else if ((int)key == 160 || (int)key == 161)
                keyPressedString.Invoke("{SHIFT}");
            else if ((int)key == 162 || (int)key == 163)
                keyPressedString.Invoke("{CTRL}");
            else if ((int)key == 164 || (int)key == 165)
                keyPressedString.Invoke("{ALT}");
            else if ((int)key == 188)
                keyPressedString.Invoke(",");
            else if ((int)key == 190)
                keyPressedString.Invoke(".");
            else if ((int)key == 191)
                keyPressedString.Invoke("/");
            else if ((int)key == 189)
                keyPressedString.Invoke("-");
            else if ((int)key == 187)
                keyPressedString.Invoke("+");
            else if ((int)key == 219)
                keyPressedString.Invoke("[");
            else if ((int)key == 221)
                keyPressedString.Invoke("]");
            else if ((int)key == 222)
                keyPressedString.Invoke("'");
            else if ((int)key == 186)
                keyPressedString.Invoke(";");
            else if ((int)key == 192)
                keyPressedString.Invoke("`");
            else
                keyPressedString.Invoke(key.ToString());
        }

        /// <param name="key">Спец. символы: BACKSPACE, ENTER CAPS LOCK SPACE SHIFT CTRL ALT . / - + [ ] ' ; `</param>
        private void InvokeReleasedEvent(Keys key)
        {
            keyRealeasedWindowsForms?.Invoke(key);

            if (keyRealeasedString == null)
                return;
            if ((int)key == 8)
                keyRealeasedString.Invoke("{BACKSPACE}");
            else if ((int)key == 13)
                keyRealeasedString.Invoke("{ENTER}");
            else if ((int)key == 20)
                keyRealeasedString.Invoke("{CAPS}");
            else if ((int)key == 32)
                keyRealeasedString.Invoke("{SPACE}");
            else if ((int)key == 160 || (int)key == 161)
                keyRealeasedString.Invoke("{SHIFT}");
            else if ((int)key == 162 || (int)key == 163)
                keyRealeasedString.Invoke("{CTRL}");
            else if ((int)key == 164 || (int)key == 165)
                keyRealeasedString.Invoke("{ALT}");
            else if ((int)key == 188)
                keyRealeasedString.Invoke(",");
            else if ((int)key == 190)
                keyRealeasedString.Invoke(".");
            else if ((int)key == 191)
                keyRealeasedString.Invoke("/");
            else if ((int)key == 189)
                keyRealeasedString.Invoke("-");
            else if ((int)key == 187)
                keyPressedString.Invoke("+");
            else if ((int)key == 219)
                keyRealeasedString.Invoke("[");
            else if ((int)key == 221)
                keyRealeasedString.Invoke("]");
            else if ((int)key == 222)
                keyRealeasedString.Invoke("'");
            else if ((int)key == 186)
                keyRealeasedString.Invoke(";");
            else if ((int)key == 192)
                keyRealeasedString.Invoke("`");
            else
                keyRealeasedString.Invoke(key.ToString());
        }

        /// <summary>
        /// Интервал цикла проверки всех клавиш (Минимум 10).
        /// </summary>
        public int Interval
        {
            get => _interval;
            set
            {
                if (value < 10)
                {
                    _interval = 10;
                    return;
                }
                _interval = value;
            }
        }

        public delegate void KeyPressedWindowsForms(Keys key);
        public delegate void KeyPressedString(string @char);
        public delegate void KeyReleasedWindowsForms(Keys key);
        public delegate void KeyReleasedString(string @char);
        public event KeyPressedWindowsForms keyPressedWindowsForms;
        public event KeyPressedString keyPressedString;
        public event KeyPressedWindowsForms keyRealeasedWindowsForms;
        public event KeyPressedString keyRealeasedString;

        private List<KeyState> _keyList;
        private Thread _thread;
        private int _interval = 10;
    }
}
