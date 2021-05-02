using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static Sens_Ruby_Recoil.Weapons;

namespace Sens_Ruby_Recoil
{
    class Program
    {
        public static void DisplayInterface()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ____          _              ____                    _  _ ");
            Console.WriteLine(" |  _ \\  _   _ | |__   _   _  |  _ \\  ___   ___  ___  (_)| |");
            Console.WriteLine(" | |_) || | | || '_ \\ | | | | | |_) |/ _ \\ / __|/ _ \\ | || |");
            Console.WriteLine(" |  _ < | |_| || |_) || |_| | |  _ <|  __/| (__| (_) || || |");
            Console.WriteLine(" |_| \\_\\ \\__,_||_.__/  \\__, | |_| \\_\\"+ "\\___| \\___|\\___/ |_||_|");
            Console.WriteLine("                       |___/                                ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" -----------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(" NUMPAD_1: Ak-47    NUMPAD_2: LR-300   NUMPAD_3: SAR ");
            Console.WriteLine(" NUMPAD_4: Custom   NUMPAD_5: MP5      NUMPAD_6: Thompson ");
            Console.WriteLine(" NUMPAD_7: M39      NUMPAD_8: M92      NUMPAD_9: M249 ");
            Console.WriteLine();
            Console.WriteLine(" NUMPAD_+: Cycle sights [Simple / Holo / 8x / 16x / None]");
            Console.WriteLine(" NUMPAD_Enter: Cycle Barrels [Supp / Boost / Break / None]");
            Console.WriteLine();
            Console.WriteLine(" Arrow Keys Randomness: [<- Less Random] [-> More Random]");
            Console.WriteLine(" Arrow Keys In-game Sens [^ Higher Sens] [v Lower Sens]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" -----------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(" Cheat Enabled: " + Hotkeys.isEnabled) ;
            Console.WriteLine(" Current Weapon: " + getActiveWeapon());
            Console.WriteLine(" Current Scope: " + getActiveScope());
            Console.WriteLine(" Current Barrel: " + getActiveBarrel());
            Console.WriteLine(" Current Randomness: {0:0.0}", getRandomness());
            Console.WriteLine(" Current Sensitivity: {0:0.0}", getSensitivity());
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" -----------------------------------------------------------");
            Console.Write(" Version 1.1 | Made by: Sen | Built on: 05/01/21 | Type: PUB");
            Console.ResetColor();
        }

        private static void HighlightText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ResetColor();
        }

        private static void InitiateHotKeys()
        {
            Hotkeys.RegisterHotKey(Keys.Tab, KeyModifiers.Control);
            Hotkeys.RegisterHotKey(Keys.NumPad1, KeyModifiers.Control); // AK
            Hotkeys.RegisterHotKey(Keys.NumPad2, KeyModifiers.Control); // LR300
            Hotkeys.RegisterHotKey(Keys.NumPad3, KeyModifiers.Control); // Semi
            Hotkeys.RegisterHotKey(Keys.NumPad4, KeyModifiers.Control); // Custom
            Hotkeys.RegisterHotKey(Keys.NumPad5, KeyModifiers.Control); // MP5
            Hotkeys.RegisterHotKey(Keys.NumPad6, KeyModifiers.Control); // Thompson
            Hotkeys.RegisterHotKey(Keys.NumPad7, KeyModifiers.Control); // M39
            Hotkeys.RegisterHotKey(Keys.NumPad8, KeyModifiers.Control); // M92
            Hotkeys.RegisterHotKey(Keys.NumPad9, KeyModifiers.Control); // M249
            Hotkeys.RegisterHotKey(Keys.Left, KeyModifiers.Control); // Smoothness Down
            Hotkeys.RegisterHotKey(Keys.Right, KeyModifiers.Control); // Smoothness Up
            Hotkeys.RegisterHotKey(Keys.Up, KeyModifiers.Control); // Randomness Up
            Hotkeys.RegisterHotKey(Keys.Down, KeyModifiers.Control); // Randomness Down
            Hotkeys.RegisterHotKey(Keys.Add, KeyModifiers.Control); // Scopes
            Hotkeys.RegisterHotKey(Keys.Enter, KeyModifiers.Control); // Barrels
            Hotkeys.HotKeyPressed += new EventHandler<HotKeyEventArgs>(Hotkeys.HotKeys_HotKeyPressed);
        }
        [DllImport("user32.dll")] public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, UIntPtr dwExtraInfo);
        [DllImport("user32.dll")] private static extern ushort GetAsyncKeyState(int vKey);

        public static bool IsKeyDown(Keys key)
        {
            return 0 != (GetAsyncKeyState((int)key) & 0x8000);
        }

        private static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private static int GetRandomSleep()
        {
            Random random = new Random();
            return random.Next(36, 50);
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(61, 29);
            Console.SetBufferSize(61, 29);
            Console.Title = "Ruby Recoil";
            DisplayInterface();
            InitiateHotKeys();
            while (true)
            {
                if (Hotkeys.isEnabled && !string.IsNullOrEmpty(getActiveWeapon()))
                {
                    while (IsKeyDown(Keys.LButton) && IsKeyDown(Keys.RButton))
                    {
                        for (int i = 0; i <= getAmmo() - 1; i++)
                        {
                            if (!IsKeyDown(Keys.LButton)) break;
                            Smoothing(isMuzzleBoost(getShootingMS()),
                            isMuzzleBoost(getShotDelay(i)),
                            (int)((((getRecoilX(i) + GetRandomNumber(0.0, getRandomness())) / 4) / getSensitivity()) * getScopeMulitplier() * getBarrelMultiplier()),
                            (int)((((getRecoilY(i) + GetRandomNumber(0.0, getRandomness())) / 4) / getSensitivity()) * getScopeMulitplier() * getBarrelMultiplier()));
                            mouse_event(0x0001, 0, 0, 0, UIntPtr.Zero);
                        }
                    }
                }
                Thread.Sleep(1);
            }
        }

        static void Smoothing(double MS, double ControlledTime, int X, int Y)
        {
            int oldX = 0, oldY = 0, oldT = 0;
            for (int i = 1; i <= (int)ControlledTime; ++i)
            {
                int newX = i * X / (int)ControlledTime;
                int newY = i * Y / (int)ControlledTime;
                int newTime = i * (int)ControlledTime / (int)ControlledTime;
                mouse_event(1, newX - oldX, newY - oldY, 0, UIntPtr.Zero);
                PerciseSleep(newTime - oldT);
                oldX = newX; oldY = newY; oldT = newTime;
            }
            PerciseSleep((int)MS - (int)ControlledTime);
        }

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        static void PerciseSleep(int ms) // Sleep / Delay
        {
            QueryPerformanceFrequency(out long timerResolution);
            timerResolution /= 1000;

            QueryPerformanceCounter(out long currentTime);
            long wantedTime = currentTime / timerResolution + ms;
            currentTime = 0;
            while (currentTime < wantedTime)
            {
                QueryPerformanceCounter(out currentTime);
                currentTime /= timerResolution;
            }
        }
    }
}
