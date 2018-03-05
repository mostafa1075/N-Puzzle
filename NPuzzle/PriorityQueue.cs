using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPuzzle
{
    class PriorityQueue
    {
        #region properties
        private double[] keys;
        private Node[] values;
        private int capacity;
        private int length;
        #endregion

        #region Constructors
        public PriorityQueue()
        {
            capacity = 20000;
            keys = new double[capacity];
            values = new Node[capacity];
            length = 0;
        }
        public PriorityQueue(int capacity)
        {
            this.capacity = capacity;
            keys = new double[capacity];
            values = new Node[capacity];
            length = 0;
        }
        #endregion

        #region methods
        public void Enqueue(double key, Node Val)
        {
            length++;
            if (length == capacity)
                resize();
            keys[length] = key;
            values[length] = Val;
            siftUp(length);
        }
        public Node Dequeue()
        {
            Node min = values[1];
            keys[1] = keys[length];
            values[1] = values[length];
            length--;
            siftDown(1);
            return min;
        }
        private void resize() // resize the arrays if the capacity is reached
        {
            capacity *= 10;
            double[] Nkeys = new double[capacity];
            Node[] Nvalues = new Node[capacity];
            keys.CopyTo(Nkeys, 0);
            values.CopyTo(Nvalues, 0);
            keys = Nkeys;
            values = Nvalues;
        }
        #endregion

        #region helperMethods
        void siftUp(int i)
        {

            if (i > 1 && keys[i / 2] > keys[i])
            {
                SwapInt(i / 2, i);
                siftUp(i / 2);
            }
        }
        void siftDown(int i)
        {
            int left = 2 * i, right = 2 * i + 1, min;
            if (left <= length && keys[i] > keys[left])
                min = left;
            else if (right <= length && keys[i] > keys[right])
                min = right;
            else
                min = i;
            if (left <= length && right <= length)    //if both childs are smaller then choose the smallest
                if (keys[i] > keys[right] && keys[i] > keys[left])
                    if (keys[left] > keys[right])

                        min = right;
                    else
                        min = left;

            if (min != i)
            {
                SwapInt(i, min);
                siftDown(min);
            }
        }
        void SwapInt(int X, int Y)
        {
            Node tmpv;
            double tmpk;
            tmpk = keys[X];
            keys[X] = keys[Y];
            keys[Y] = tmpk;

            tmpv = values[X];
            values[X] = values[Y];
            values[Y] = tmpv;
        }
        #endregion
    }
}
