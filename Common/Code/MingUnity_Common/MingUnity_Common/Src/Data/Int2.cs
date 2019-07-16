﻿using System;

namespace MingUnity.Common
{
    [Serializable]
    public struct Int2
    {
        public int x;

        public int y;

        public Int2(int x, int y)
        {
            this.x = x;

            this.y = y;
        }

        public override bool Equals(object obj)
        {
            bool res = false;

            if (obj is Int2)
            {
                Int2 target = (Int2)obj;

                res = x == target.x && y == target.y;
            }

            return res;
        }

        public static bool operator ==(Int2 a, Int2 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Int2 a, Int2 b)
        {
            return !a.Equals(b);
        }

        public static Int2 operator +(Int2 a, Int2 b)
        {
            return new Int2(a.x + b.x, a.y + b.y);
        }

        public static Int2 operator -(Int2 a, Int2 b)
        {
            return new Int2(a.x - b.x, a.y - b.y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"[Int2 x:{x} y:{y}]";
        }
    }
}
