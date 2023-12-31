﻿namespace UtilityLibrary
{
    public class SimplexNoise
    {
        private const double Div6 = 1.0 / 6.0;
        private const double Div3 = 1.0 / 3.0;

        private static readonly int[][] Grad3 = new[]
        {
            new[] { 1, 1, 0 }, new[] { -1, 1, 0 }, new[] { 1, -1, 0 }, new[] { -1, -1, 0 },
            new[] { 1, 0, 1 }, new[] { -1, 0, 1 }, new[] { 1, 0, -1 }, new[] { -1, 0, -1 },
            new[] { 0, 1, 1 }, new[] { 0, -1, 1 }, new[] { 0, 1, -1 }, new[] { 0, -1, -1 }
        };

        private static readonly int[] P =
        {
            151, 160, 137, 91, 90, 15, 131, 13, 201, 95, 96, 53, 194, 233, 7, 225, 140, 36, 103,
            30, 69, 142, 8, 99, 37, 240, 21, 10, 23, 190, 6, 148, 247, 120, 234, 75, 0, 26, 197,
            62, 94, 252, 219, 203, 117, 35, 11, 32, 57, 177, 33, 88, 237, 149, 56, 87, 174, 20,
            125, 136, 171, 168, 68, 175, 74, 165, 71, 134, 139, 48, 27, 166, 77, 146, 158, 231,
            83, 111, 229, 122, 60, 211, 133, 230, 220, 105, 92, 41, 55, 46, 245, 40, 244, 102,
            143, 54, 65, 25, 63, 161, 1, 216, 80, 73, 209, 76, 132, 187, 208, 89, 18, 169, 200,
            196, 135, 130, 116, 188, 159, 86, 164, 100, 109, 198, 173, 186, 3, 64, 52, 217, 226,
            250, 124, 123, 5, 202, 38, 147, 118, 126, 255, 82, 85, 212, 207, 206, 59, 227, 47, 16,
            58, 17, 182, 189, 28, 42, 223, 183, 170, 213, 119, 248, 152, 2, 44, 154, 163, 70,
            221, 153, 101, 155, 167, 43, 172, 9, 129, 22, 39, 253, 19, 98, 108, 110, 79, 113, 224,
            232, 178, 185, 112, 104, 218, 246, 97, 228, 251, 34, 242, 193, 238, 210, 144, 12,
            191, 179, 162, 241, 81, 51, 145, 235, 249, 14, 239, 107, 49, 192, 214, 31, 181, 199,
            106, 157, 184, 84, 204, 176, 115, 121, 50, 45, 127, 4, 150, 254, 138, 236, 205, 93,
            222, 114, 67, 29, 24, 72, 243, 141, 128, 195, 78, 66, 215, 61, 156, 180
        };

        private static readonly int[] Perm = new int[512];
        private static readonly double Sqrt3 = Math.Sqrt(3);
        private static readonly double F2 = 0.5 * (Sqrt3 - 1.0);
        private static readonly double G2 = (3.0 - Sqrt3) * Div6;

        static SimplexNoise()
        {
            for (int i = 0; i < 512; i++)
            {
                Perm[i] = P[i & 255];
            }

            Singleton = new SimplexNoise();
        }

        public static SimplexNoise Singleton { get; private set; }

        public static double Noise01(double x, double y) => (Noise(x, y) + 1) * 0.5;

        public static double MultiNoise(int octaves, double x, double y)
        {
            double value = 0.0;
            float mul = 1;
            for (int i = 0; i < octaves; i++)
            {
                value += Noise((x + 10) * mul, (y + 15) * mul) / mul;

                mul *= 2;
            }
            return value;
        }

        public static double MultiNoise01(int octaves, double x, double y) => (MultiNoise(octaves, x, y) + 1.0) * 0.5;

        public static double RidgedMulti(int octaves, double x, double y)
        {
            double value = 0.0;
            double mul = 1;
            for (int i = 0; i < octaves; i++)
            {
                double added = Noise(x * mul, y * mul) / mul;
                value += Math.Abs(added);

                mul *= 2.18387276;
            }
            return value;
        }

        public static double Noise(double xin, double yin)
        {
            double n0, n1, n2;

            double s = (xin + yin) * F2;

            int i = FastFloor(xin + s);
            int j = FastFloor(yin + s);

            double t = (i + j) * G2;
            double x0p = i - t; 
            double y0p = j - t;
            double x0 = xin - x0p;
            double y0 = yin - y0p;

            int i1, j1;
            if (x0 > y0)
            {
                i1 = 1;
                j1 = 0;
            }
            else
            {
                i1 = 0;
                j1 = 1;
            }

            double x1 = x0 - i1 + G2; 
            double y1 = y0 - j1 + G2;
            double x2 = x0 - 1.0 + 2.0 * G2; 
            double y2 = y0 - 1.0 + 2.0 * G2;

            int ii = i & 255;
            int jj = j & 255;
            int gi0 = Perm[ii + Perm[jj]] % 12;
            int gi1 = Perm[ii + i1 + Perm[jj + j1]] % 12;
            int gi2 = Perm[ii + 1 + Perm[jj + 1]] % 12;

            double t0 = 0.5 - x0 * x0 - y0 * y0;
            if (t0 < 0)
            {
                n0 = 0.0;
            }
            else
            {
                t0 *= t0;
                n0 = t0 * t0 * Dot(Grad3[gi0], x0, y0);
            }
            double t1 = 0.5 - x1 * x1 - y1 * y1;
            if (t1 < 0)
            {
                n1 = 0.0;
            }
            else
            {
                t1 *= t1;
                n1 = t1 * t1 * Dot(Grad3[gi1], x1, y1);
            }
            double t2 = 0.5 - x2 * x2 - y2 * y2;
            if (t2 < 0)
            {
                n2 = 0.0;
            }
            else
            {
                t2 *= t2;
                n2 = t2 * t2 * Dot(Grad3[gi2], x2, y2);
            }

            return 70.0 * (n0 + n1 + n2);
        }

        public double Multi01(int octaves, double x, double y, double z)
        {
            return (Multi(octaves, x, y, z) + 1) * 0.5;
        }

        public double Multi(int octaves, double x, double y, double z)
        {
            double value = 0.0;
            double mul = 1;
            for (int i = 0; i < octaves; i++)
            {
                double added = Noise(x * mul, y * mul, z * mul) / mul;
                value += added;
                mul *= 2;
            }
            return value;
        }

        public static double Noise01(double x, double y, double z)
        {
            double val = Noise(x, y, z);
            return (val + 1) * 0.5;
        }

        public static double RidgedMulti(int octaves, double x, double y, double z)
        {
            double value = 0.0;
            double mul = 1;
            for (int i = 0; i < octaves; i++)
            {
                double added = Noise(x * mul, y * mul, z * mul) / mul;
                value += Math.Abs(added);
                mul *= 2;
            }
            return value;
        }

        public static double Noise(double xin, double yin, double zin)
        {
            double n0, n1, n2, n3;

            double s = (xin + yin + zin) * Div3;
            int i = FastFloor(xin + s);
            int j = FastFloor(yin + s);
            int k = FastFloor(zin + s);

            double t = (i + j + k) * Div6;
            double ax0 = i - t; 
            double ay0 = j - t;
            double az0 = k - t;
            double x0 = xin - ax0;
            double y0 = yin - ay0;
            double z0 = zin - az0;

            int i1, j1, k1; 
            int i2, j2, k2;
            if (x0 >= y0)
            {
                if (y0 >= z0)
                {
                    i1 = 1;
                    j1 = 0;
                    k1 = 0;
                    i2 = 1;
                    j2 = 1;
                    k2 = 0;
                }
                else if (x0 >= z0)
                {
                    i1 = 1;
                    j1 = 0;
                    k1 = 0;
                    i2 = 1;
                    j2 = 0;
                    k2 = 1;
                }
                else
                {
                    i1 = 0;
                    j1 = 0;
                    k1 = 1;
                    i2 = 1;
                    j2 = 0;
                    k2 = 1;
                }
            }
            else
            {
                if (y0 < z0)
                {
                    i1 = 0;
                    j1 = 0;
                    k1 = 1;
                    i2 = 0;
                    j2 = 1;
                    k2 = 1;
                }
                else if (x0 < z0)
                {
                    i1 = 0;
                    j1 = 1;
                    k1 = 0;
                    i2 = 0;
                    j2 = 1;
                    k2 = 1;
                }
                else
                {
                    i1 = 0;
                    j1 = 1;
                    k1 = 0;
                    i2 = 1;
                    j2 = 1;
                    k2 = 0;
                }
            }

            double x1 = x0 - i1 + Div6;
            double y1 = y0 - j1 + Div6;
            double z1 = z0 - k1 + Div6;
            double x2 = x0 - i2 + 2.0 * Div6; 
            double y2 = y0 - j2 + 2.0 * Div6;
            double z2 = z0 - k2 + 2.0 * Div6;
            double x3 = x0 - 1.0 + 3.0 * Div6; 
            double y3 = y0 - 1.0 + 3.0 * Div6;
            double z3 = z0 - 1.0 + 3.0 * Div6;

            int ii = i & 255;
            int jj = j & 255;
            int kk = k & 255;
            int gi0 = Perm[ii + Perm[jj + Perm[kk]]] % 12;
            int gi1 = Perm[ii + i1 + Perm[jj + j1 + Perm[kk + k1]]] % 12;
            int gi2 = Perm[ii + i2 + Perm[jj + j2 + Perm[kk + k2]]] % 12;
            int gi3 = Perm[ii + 1 + Perm[jj + 1 + Perm[kk + 1]]] % 12;

            double t0 = 0.6 - x0 * x0 - y0 * y0 - z0 * z0;
            if (t0 < 0)
            {
                n0 = 0.0;
            }
            else
            {
                t0 *= t0;
                n0 = t0 * t0 * Dot(Grad3[gi0], x0, y0, z0);
            }
            double t1 = 0.6 - x1 * x1 - y1 * y1 - z1 * z1;
            if (t1 < 0)
            {
                n1 = 0.0;
            }
            else
            {
                t1 *= t1;
                n1 = t1 * t1 * Dot(Grad3[gi1], x1, y1, z1);
            }
            double t2 = 0.6 - x2 * x2 - y2 * y2 - z2 * z2;
            if (t2 < 0)
            {
                n2 = 0.0;
            }
            else
            {
                t2 *= t2;
                n2 = t2 * t2 * Dot(Grad3[gi2], x2, y2, z2);
            }
            double t3 = 0.6 - x3 * x3 - y3 * y3 - z3 * z3;
            if (t3 < 0)
            {
                n3 = 0.0;
            }
            else
            {
                t3 *= t3;
                n3 = t3 * t3 * Dot(Grad3[gi3], x3, y3, z3);
            }

            return 32.0 * (n0 + n1 + n2 + n3);
        }

        private static int FastFloor(double x) =>  x > 0 ? (int)x : (int)x - 1;

        private static double Dot(int[] g, double x, double y) => g[0] * x + g[1] * y;

        private static double Dot(int[] g, double x, double y, double z) => g[0] * x + g[1] * y + g[2] * z;
    }
}