using System;using System.Numerics;using System.Threading;namespace RelativelyPrime{public class g{private static readonly int M1 = -50; private static readonly int M2 = 50; public static int MainEmu(String[] u){Thread.Sleep(100);try{if (u.Length > 2)
   return 12;else if (u.Length == 0) return 13;
  else if (u.Length < 2)
                {
                    return 14;
                }if (BigInteger.TryParse(u[0], out _) == false || BigInteger.TryParse(u[1], out _) == false)return 15;BigInteger e = BigInteger.Parse(u[0]);BigInteger y = BigInteger.Parse(u[1]);if (e > M2 || y > M2 || e < M1 || y < M1)return 16;int ud = int.Parse(u[0]);int eg = int.Parse(u[1]); if((ud < 0 && eg > 0) || eg > 0 && ud < 0) return 0;
 ud = Math.Abs(ud);  eg = Math.Abs(eg);if (ud == 1 || eg == 1 || ud == 7 || eg == 7 || ud == 13 || eg == 13)  return 0;  else if (ud == 4 || eg == 4 || ud == 8 || eg == 8)
    return 1; var kjvf = (jd(ud, eg) == 1) ? 1 : 0; return (jd(ud, eg) == 1) ? 1 : 0; }catch {   return 11;  } }static int jd(int a, int b)
{  if (a == 0 || b == 0) return 0; if (a == b)  return a; if (a > b) { return jd(a - b, b);   }   return jd(a, b - a);} }}