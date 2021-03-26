using System;

namespace Sens_Ruby_Recoil
{
    class Weapons
    {
        private static int[,] ActiveRecoil { get; set; } = { { 0, 0 } };
        private static string ActiveWeapon { get; set; } = "None";
        private static bool Automatic { get; set; } = false;
        private static int ShootingMS { get; set; } = 0;
        private static int Ammo { get; set; } = 0;
        private static string Scope { get; set; } = "None";
        private static double ScopeMulitplier { get; set; } = 1.0;
        private static string Barrel { get; set; } = "None";
        private static double BarrelMultiplier { get; set; } = 1.0;
        private static double Randomness { get; set; } = 5.0;
        private static double Sensitivity { get; set; } = 1.0;
        private static bool MuzzleBoost { get; set; } = false;

        private static readonly int[,] AssaultRifle = { { -69, 100 }, { 10, 92 }, { -110, 83 }, { -85, 75 }, { 0, 67 }, { 33, 57 }, { 58, 48 }, { 76, 39 }, { 84, 29 }, { 85, 19 }, { 76, 20 }, { 60, 37 }, { 34, 50 }, { 2, 59 }, { -30, 65 }, { -55, 67 }, { -74, 64 }, { -86, 59 }, { -92, 49 }, { -91, 34 }, { -84, 17 }, { -70, 10 }, { -49, 28 }, { -22, 42 }, { 24, 51 }, { 72, 56 }, { 97, 57 }, { 98, 51 }, { 77, 43 } };

        private static readonly double[] AKControlTime = { 121.96149709966872, 92.6333814724611, 138.60598637206294, 113.37874368443146, 66.25151186427745, 66.29530438019354, 75.9327831420658, 85.05526144256157, 89.20256669256554, 86.68010184667988, 78.82145888317788, 70.0451048111144, 60.85979604582978, 59.51642457624619, 71.66762996283607, 86.74060009403034, 98.3363599080854, 104.34161954944257, 104.09299204005345, 97.58780746901739, 85.48062700875559, 70.4889202349561, 56.56417811530545, 47.386907899993936, 56.63787408680247, 91.5937793023631, 112.38667610336424, 111.39338971888095, 87.5067801164596 };

        private static readonly int[,] LR300AssaultRifle = { { 0, 50 }, { -11, 60 }, { -22, 67 }, { -28, 59 }, { -31, 50 }, { -29, 42 }, { -9, 38 }, { -9, 30 }, { 23, 25 }, { 36, 24 }, { 35, 13 }, { 40, 19 }, { 18, 6 }, { 0, 17 }, { -13, 6 }, { -16, 5 }, { -19, 6 }, { -34, 12 }, { -31, 2 }, { -29, 5 }, { -28, 0 }, { -21, 5 }, { -12, 13 }, { -7, 0 }, { 19, 5 }, { 3, 11 }, { 61, 0 }, { 73, 0 }, { 54, 6 }, { 0, 8 }, { 50, 0 } };

        private static readonly int[,] SemiAssultRifle = { { 0, 0 } };

        private static readonly int[,] CustomSMG = { { -28, 52 }, { -10, 53 }, { 0, 53 }, { 11, 44 }, { 20, 45 }, { 22, 42 }, { 17, 35 }, { 7, 30 }, { -9, 27 }, { -13, 28 }, { -23, 22 }, { -21, 21 }, { -15, 24 }, { 0, 13 }, { 20, 14 }, { 16, 12 }, { 29, 19 }, { 7, 6 }, { 11, 10 }, { -4, 8 }, { -8, 13 }, { -7, 2 }, { -13, 14 } };

        private static readonly int[,] MP5A4 = { { 0, 43 }, { 0, 58 }, { 0, 65 }, { 25, 66 }, { 59, 58 }, { 63, 42 }, { 46, 27 }, { 3, 23 }, { -37, 19 }, { -47, 18 }, { -40, 18 }, { -8, 7 }, { 16, 12 }, { 28, 11 }, { 35, 9 }, { 34, 8 }, { 25, 6 }, { 12, 0 }, { -4, 2 }, { -6, 2 }, { -18, 0 }, { -27, 5 }, { -26, 0 }, { -27, 0 }, { -20, 0 }, { -32, 0 }, { -12, 0 }, { -25, 0 }, { -4, 0 }, { 0, 0 }, { 43, 0 } };

        private static readonly int[,] Thompson = { { -29, 63 }, { -12, 61 }, { 9, 61 }, { 21, 55 }, { 25, 52 }, { 21, 43 }, { 5, 32 }, { -16, 33 }, { -24, 25 }, { -24, 26 }, { -14, 21 }, { 7, 17 }, { 16, 18 }, { 23, 16 }, { 25, 17 }, { 8, 16 }, { -5, 5 }, { -13, 15 }, { -14, 8 } };

        private static readonly int[,] M92 = { { 0, 0 } };

        private static readonly int[,] M39 = { { 0, 0 } };

        private static readonly int[,] M249 = { { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 },
        { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 },
        { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 },
        { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 },
        { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 },
        { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 },
        { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 },
        { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 },
        { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 },
        { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 }, { 0, 60 } };

        public static void ChangeWeapon(int WeaponIndex)
        {
            switch (WeaponIndex)
            {
                case 1:
                    FillWeaponInfo(AssaultRifle, "Assault Rifle", true, 133, 29);
                    break;
                case 2:
                    FillWeaponInfo(LR300AssaultRifle, "LR-300 Assault Rifle", true, 120, 29);
                    break;
                case 3:
                    FillWeaponInfo(SemiAssultRifle, "Semi-Automatic Rifle [DISABLED]", false, 175, 1);
                    break;
                case 4:
                    FillWeaponInfo(CustomSMG, "Custom SMG", true, 100, 29);
                    break;
                case 5:
                    FillWeaponInfo(MP5A4, "MP5A4", true, 120, 29);
                    break;
                case 6:
                    FillWeaponInfo(Thompson, "Thompson", true, 130, 19);
                    break;
                case 7:
                    FillWeaponInfo(M92, "M92 [DISABLED]", false, 100, 14);
                    break;
                case 8:
                    FillWeaponInfo(M39, "M39 [DISABLED]", false, 200, 19);
                    break;
                case 9:
                    FillWeaponInfo(M249, "M249", true, 120, 99);
                    break;
            }
        }

        public static void ChangeScope(int ScopeIndex)
        {
            switch (ScopeIndex)
            {
                case 0: // None
                    Scope = "None";
                    ScopeMulitplier = 1.0;
                    break;
                case 1: // Simple
                    Scope = "Simple scope";
                    ScopeMulitplier = 0.8;
                    break;
                case 2: // Holo 
                    Scope = "Holo scope";
                    ScopeMulitplier = 1.2;
                    break;
                case 3: // 8x
                    Scope = "8x scope";
                    ScopeMulitplier = 3.84;
                    break;
                case 4: // 16x
                    Scope = "16x scope";
                    ScopeMulitplier = 7.68;
                    break;
            }
        }

        public static void ChangeBarrel(int BarrelIndex)
        {
            switch (BarrelIndex)
            {
                case 0: // None
                    Barrel = "None";
                    BarrelMultiplier = 1.0;
                    break;
                case 1: // Suppressor
                    Barrel = "Suppressor";
                    BarrelMultiplier = 0.8;
                    break;
                case 2: // Muzzle Booster
                    Barrel = "Muzzle boost";
                    BarrelMultiplier = 1.0;
                    MuzzleBoost = true;
                    break;
                case 3: // Muzzle Break
                    Barrel = "Muzzle break";
                    BarrelMultiplier = 0.5;
                    MuzzleBoost = false;
                    break;
            }
        }

        public static void ChangeRandomness(int RandomnessIndex)
        {
            switch (RandomnessIndex)
            {
                case -1:
                    if (0 < Randomness)
                        Randomness--;
                    break;
                case 1:
                    if (Randomness < 10)
                        Randomness++;
                    break;
            }
        }

        public static void ChangeSensitivity(int SensitivityIndex)
        {
            switch (SensitivityIndex)
            {
                case -1:
                    if (0.2 < Sensitivity)
                        Sensitivity -= 0.1;
                    break;
                case 1:
                    if (Sensitivity < 2.0)
                        Sensitivity += 0.1;
                    break;
            }
        }

        public static double ShotDelay(int Bullet)
        {
            if (ActiveWeapon == "Assault Rifle")
                return AKControlTime[Bullet];
            else
                return ShootingMS;
        }

        public static int getRecoilX(int Bullet)
        {
            return ActiveRecoil[Bullet, 0];
        }

        public static int getRecoilY(int Bullet)
        {
            return ActiveRecoil[Bullet, 1];
        }

        public static string getActiveWeapon()
        {
            if (!String.IsNullOrEmpty(ActiveWeapon))
                return ActiveWeapon;
            else return "None";
        }

        public static string getActiveScope()
        {
            if (!String.IsNullOrEmpty(Scope) && Scope != "None")
                return Scope;
            else return "None";
        }

        public static string getActiveBarrel()
        {
            if (!String.IsNullOrEmpty(Barrel) && Barrel != "None")
                return Barrel;
            else return "None";
        }

        public static double getRandomness()
        {
            return Randomness;
        }

        public static double getSensitivity()
        {
            return Sensitivity;
        }

        public static int getShootingMS()
        {
            return ShootingMS;
        }

        public static int getAmmo()
        {
            return Ammo;
        }

        public static double getScopeMulitplier()
        {
            return ScopeMulitplier;
        }

        public static double getBarrelMultiplier()
        {
            return BarrelMultiplier;
        }

        public static bool isAutomatic()
        {
            return Automatic;
        }

        public static double isMuzzleBoost(double MS)
        {
            if (MuzzleBoost)
                return (MS - (MS * 0.1f));
            else
                return MS;
        }

        private static void FillWeaponInfo(int[,] AR, string AW, bool A, int MS, int AM)
        {
            ActiveRecoil = AR;
            ActiveWeapon = AW;
            Automatic = A;
            ShootingMS = MS;
            Ammo = AM;
        }
    }
}
