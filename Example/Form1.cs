using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KeyboardCapture;

namespace Example
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            keyboard = new KBCapture(true);
            keyboard.keyPressedWindowsForms += Keyboard_keyPressedWindowsForms;
            keyboard.keyRealeasedWindowsForms += Keyboard_keyRealeasedWindowsForms;
            keyboard.keyPressedString += Keyboard_keyPressedString;
        }

        private void Keyboard_keyPressedString(string @char)
        {
            Action action = new Action(() =>
            {
                richTextBox1.AppendText(@char);
            });
            Invoke(action);
        }

        private void Keyboard_keyPressedWindowsForms(Keys key)
        {
            Action action = new Action(() =>
            {
                label1.Text = $"[DOWN]{(int)key} — {key}";
            });
            Invoke(action);
        }

        private void Keyboard_keyRealeasedWindowsForms(Keys key)
        {
            Action action = new Action(() =>
            {
                label1.Text = $"[UP]{(int)key} — {key}";
            });
            Invoke(action);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            keyboard.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            keyboard.Stop();
        }


        private KBCapture keyboard;
    }
}
