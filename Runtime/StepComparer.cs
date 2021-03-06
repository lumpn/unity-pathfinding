//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;

namespace Lumpn.Pathfinding
{
    internal sealed class StepComparer : IComparer<Step>
    {
        private static readonly IComparer<float> costComparer = Comparer<float>.Default;

        public int Compare(Step a, Step b)
        {
            return costComparer.Compare(a.estimate, b.estimate);
        }
    }
}
