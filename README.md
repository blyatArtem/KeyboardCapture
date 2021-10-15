## KeyboardCapture.dll
Сборка отсылает события KeyDown и KeyUp.
Эти события отсылают System.Windows.Forms.Keys или строку с символом либо кнопкой.
```cs
            keyboard = new KBCapture(true);
            keyboard.keyPressedWindowsForms += Keyboard_keyPressedWindowsForms;
            keyboard.keyRealeasedWindowsForms += Keyboard_keyRealeasedWindowsForms;
            keyboard.keyPressedString += Keyboard_keyPressedString;
```
