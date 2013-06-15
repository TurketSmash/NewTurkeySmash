﻿using System;
using System.Threading;

namespace TurkeySmash
{
        public static class RandomProvider
        {
            private static int seed = Environment.TickCount;

            private static ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(() =>
                new Random(Interlocked.Increment(ref seed))
            );

            public static Random GetRandom()
            {
                return randomWrapper.Value;
            }
        }
}
