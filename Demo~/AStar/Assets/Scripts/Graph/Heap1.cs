//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lumpn.Graph
{
    /// Minimum heap
    internal sealed class Heap1<T>
    {
        public struct Entry
        {
            public readonly T value;
            public readonly float cost;

            public Entry(T value, float cost)
            {
                this.value = value;
                this.cost = cost;
            }
        }

        private readonly List<Entry> heap;

        public int Count
        {
            get { return heap.Count; }
        }

        public int LastIndex
        {
            get { return Count - 1; }
        }

        public int Capacity
        {
            get { return heap.Capacity; }
            set { heap.Capacity = value; }
        }

        public Heap1(int initialCapacity)
        {
            this.heap = new List<Entry>(initialCapacity);
        }

        //public void Push(IEnumerable<KeyValuePair<T, float>> items)
        //{
        //    int prevCount = Count;
        //    heap.AddRange(items);

        //    // heapify
        //    for (int i = prevCount; i < Count; i++)
        //    {
        //        BubbleUp(i);
        //    }
        //}

        public void Push(T item, float cost)
        {
            var entry = new Entry(item, cost);
            heap.Add(entry);
            BubbleUp(LastIndex);
        }

        public Entry Pop()
        {
            var result = heap[0];
            Swap(LastIndex, 0);
            heap.RemoveAt(LastIndex);
            BubbleDown(0);
            return result;
        }

        public T Peek()
        {
            return heap[0].value;
        }

        private void BubbleUp(int i)
        {
            if (i == 0) return;

            var parent = Parent(i);
            if (heap[i].cost >= heap[parent].cost) return;

            Swap(i, parent);
            BubbleUp(parent);
        }

        private void BubbleDown(int i)
        {
            var childA = FirstChild(i);
            var childB = childA + 1;
            if (childA >= Count) return; // no children

            var minChild = (childB >= Count || (heap[childA].cost < heap[childB].cost)) ? childA : childB;
            if (heap[minChild].cost >= heap[i].cost) return;

            Swap(i, minChild);
            BubbleDown(minChild);
        }

        private void Swap(int i, int j)
        {
            var tmp = heap[i];
            heap[i] = heap[j];
            heap[j] = tmp;
        }

        private static int Parent(int i)
        {
            return (i + 1) / 2 - 1;
        }

        private static int FirstChild(int i)
        {
            return (i + 1) * 2 - 1;
        }

        public void Clear()
        {
            heap.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return heap.Select(p => p.value).GetEnumerator();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Heap(");
            foreach (var item in heap)
            {
                sb.Append(item);
                sb.Append(", ");
            }
            sb.Append("_)");
            return sb.ToString();
        }
    }
}
