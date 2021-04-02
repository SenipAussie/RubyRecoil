using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sens_Ruby_Recoil
{
    class Hotkeys
    {
        delegate void RegisterHotKeyDelegate(IntPtr hwnd, int id, uint modifiers, uint key);

        [DllImport("user32", SetLastError = true)] private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        private static int _id = 0;

        public static int RegisterHotKey(Keys key, KeyModifiers modifiers)
        {
            _windowReadyEvent.WaitOne();
            int id = Interlocked.Increment(ref _id);
            _wnd.Invoke(new RegisterHotKeyDelegate(RegisterHotKeyInternal), _hwnd, id, (uint)modifiers, (uint)key);
            return id;
        }

        private static void RegisterHotKeyInternal(IntPtr hwnd, int id, uint modifiers, uint key)
        {
            RegisterHotKey(hwnd, id, modifiers, key);
        }

        public static event EventHandler<HotKeyEventArgs> HotKeyPressed;

        private static void OnHotKeyPressed(HotKeyEventArgs e)
        {
            Hotkeys.HotKeyPressed?.Invoke(null, e);
        }

        public static bool isEnabled;
        private static int scopeIndex;
        private static int barrelIndex;
        public static void HotKeys_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.Tab:
                    isEnabled = !isEnabled;
                    break;
                case Keys.NumPad1:
                    Weapons.ChangeWeapon(1);
                    break;
                case Keys.NumPad2:
                    Weapons.ChangeWeapon(2);
                    break;
                case Keys.NumPad3:
                    Weapons.ChangeWeapon(3);
                    break;
                case Keys.NumPad4:
                    Weapons.ChangeWeapon(4);
                    break;
                case Keys.NumPad5:
                    Weapons.ChangeWeapon(5);
                    break;
                case Keys.NumPad6:
                    Weapons.ChangeWeapon(6);
                    break;
                case Keys.NumPad7:
                    Weapons.ChangeWeapon(7);
                    break;
                case Keys.NumPad8:
                    Weapons.ChangeWeapon(8);
                    break;
                case Keys.NumPad9:
                    Weapons.ChangeWeapon(9);
                    break;
                case Keys.Left:
                    Weapons.setRandomness(-1);
                    break;
                case Keys.Right:
                    Weapons.setRandomness(1);
                    break;
                case Keys.Up:
                    Weapons.setSensitivity(1);
                    break;
                case Keys.Down:
                    Weapons.setSensitivity(-1);
                    break;
                case Keys.Add:
                    scopeIndex++;
                    switch (scopeIndex)
                    {
                        case 0: // None
                            Weapons.ChangeScope(0);
                            break;
                        case 1: // Simple
                            Weapons.ChangeScope(1);
                            break;
                        case 2: // Holo 
                            Weapons.ChangeScope(2);
                            break;
                        case 3: // 8x
                            Weapons.ChangeScope(3);
                            break;
                        case 4: // 16x
                            Weapons.ChangeScope(4);
                            scopeIndex = -1;
                            break;
                    }
                    break;
                case Keys.Enter:
                    barrelIndex++;
                    switch (barrelIndex)
                    {
                        case 0: // None
                            Weapons.ChangeBarrel(0);
                            break;
                        case 1: // Suppressor
                            Weapons.ChangeBarrel(1);
                            break;
                        case 2: // Muzzle Boost
                            Weapons.ChangeBarrel(2);
                            break;
                        case 3: // Muzzle Break
                            Weapons.ChangeBarrel(3);
                            barrelIndex = -1;
                            break;
                    }
                    break;                    
            }
            Console.Clear();
            Program.DisplayInterface();
        }

        private static volatile MessageWindow _wnd;
        private static volatile IntPtr _hwnd;
        private static ManualResetEvent _windowReadyEvent = new ManualResetEvent(false);

        static Hotkeys()
        {
            Thread messageLoop = new Thread(delegate ()
            {
                Application.Run(new MessageWindow());
            });
            messageLoop.Name = "HotKeyMessageThread";
            messageLoop.IsBackground = true;
            messageLoop.Start();
        }

        private class MessageWindow : Form
        {
            public MessageWindow()
            {
                _wnd = this;
                _hwnd = this.Handle;
                _windowReadyEvent.Set();
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_HOTKEY)
                {
                    HotKeyEventArgs e = new HotKeyEventArgs(m.LParam);
                    Hotkeys.OnHotKeyPressed(e);
                }

                base.WndProc(ref m);
            }

            protected override void SetVisibleCore(bool value)
            {
                base.SetVisibleCore(false);
            }

            private const int WM_HOTKEY = 0x312;
        }
    }

    [Flags]
    public enum KeyModifiers
    {
        Control = 2
    }

    public class HotKeyEventArgs : EventArgs
    {
        public readonly Keys Key;
        public readonly KeyModifiers Modifiers;

        public HotKeyEventArgs(IntPtr hotKeyParam)
        {
            uint param = (uint)hotKeyParam.ToInt64();
            Key = (Keys)((param & 0xffff0000) >> 16);
            Modifiers = (KeyModifiers)(param & 0x0000ffff);
        }
    }
}
