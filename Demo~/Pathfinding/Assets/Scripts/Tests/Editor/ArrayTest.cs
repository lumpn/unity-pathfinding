//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Lumpn.Pathfinding.Demo
{
    [TestFixture]
    public sealed class ArrayTest
    {
        private const int length = 32 * 1024 * 1024;

        [Test]
        public void TestClear()
        {
            var items = new int[length];

            var watch = new Stopwatch();
            watch.Start();

            Array.Clear(items, 0, length);

            watch.Stop();

            UnityEngine.Debug.LogFormat("Clear took {0} ms", watch.ElapsedMilliseconds);
        }

        [Test]
        public void TestFill()
        {
            var items = new int[length];

            var watch = new Stopwatch();
            watch.Start();

            Fill(items, -1, 0, length);

            watch.Stop();

            UnityEngine.Debug.LogFormat("Fill took {0} ms", watch.ElapsedMilliseconds);
        }

        private static void Fill<T>(T[] array, T value, int startIndex, int count)
        {
            int endIndex = startIndex + count;
            for (int i = startIndex; i < endIndex; i++)
            {
                array[i] = value;
            }
        }
    }
}
