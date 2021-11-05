//----------------------------------------
// MIT License
// Copyright(c) 2019 Jonas Boetel
//----------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace Lumpn.Graph
{
    /// Minimum heap
    internal sealed class Heap<T>
    {
        private readonly IComparer<T> comparer;
        private T[] heap;
        private int count;

        public int Count
        {
            get { return count; }
        }

        public int Capacity
        {
            get { return heap.Length; }
        }

        public Heap(IComparer<T> comparer, int initialCapacity)
        {
            this.comparer = comparer;
            this.heap = new T[initialCapacity];
            this.count = 0;
        }

        public void Push(IEnumerable<T> items)
        {
            int oldCount = count;
            foreach (var item in items)
            {
                Add(item);
            }

            // heapify
            for (int i = oldCount; i < count; i++)
            {
                BubbleUp(i);
            }
        }

        public void Push(T item)
        {
            Add(item);
            BubbleUp(count - 1);
        }

        public T Pop()
        {
            T result = heap[0];
            Swap(count - 1, 0);
            RemoveLast();
            BubbleDown(0);
            return result;
        }

        public T Peek()
        {
            return heap[0];
        }

        private void BubbleUp(int i)
        {
            if (i == 0) return;

            var parent = Parent(i);
            if (comparer.Compare(heap[i], heap[parent]) >= 0) return;

            Swap(i, parent);
            BubbleUp(parent);
        }

        private void BubbleDown(int i)
        {
            var childA = FirstChild(i);
            if (childA >= count) return; // no children

            var childB = childA + 1;
            var minChild = (childB >= count || comparer.Compare(heap[childA], heap[childB]) < 0) ? childA : childB;

            if (comparer.Compare(heap[minChild], heap[i]) >= 0) return;

            Swap(i, minChild);
            BubbleDown(minChild);
        }

        private void Swap(int i, int j)
        {
            T tmp = heap[i];
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
            count = 0;
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

        private void Add(T item)
        {
            var capacity = heap.Length;
            if (count >= capacity)
            {
                Array.Resize(ref heap, capacity * 2);
            }

            heap[count++] = item;
        }

        private void RemoveLast()
        {
            count--;
        }
    }
}
