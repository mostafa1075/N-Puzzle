using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NPuzzle
{
    public class Node
    {

        #region Properties
        private int[,] arr;
        private int priority;
        private string priorityType;
        private int moves;
        private int arraySize;
        private Node Predecessor;
        private string previousMove; // move made by predecessor to reach this node
        private string Colour;

        #endregion Properties

        #region Constructors

        public Node(int[,] arr, int arraySize, int moves, string priorityType)
        {
            this.arr = arr;
            this.moves = moves;
            this.arraySize = arraySize;
            priority = Manhattan() + moves;
            this.priorityType = priorityType;
            Colour = "Grey";
        }
        public Node(int[,] arr, int priority, Node Predecessor, string previousMove)
        {
            this.arr = arr;
            this.moves = Predecessor.moves + 1;
            this.arraySize = Predecessor.arraySize;
            this.priority = priority + moves;
            this.priorityType = Predecessor.priorityType;
            this.Predecessor = Predecessor;
            this.previousMove = previousMove;
            Colour = "Grey";
        }
        #endregion Constructors

        #region getters/setters

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        public int Moves
        {
            get { return moves; }
            set { moves = value; }
        }
        public int ArraySize
        {
            get { return arraySize; }
            set { arraySize = value; }
        }
        public int[,] Arr
        {
            get { return arr; }
            set { arr = value; }
        }
        public string PriorityType //after being set the priority is recalculated using the new property
        {
            get { return priorityType; }
            set
            {
                priorityType = value;
                if (priorityType == "hamming")
                    priority = hamming() + moves;
                else if (priorityType == "manhattan")
                    priority = Manhattan() + moves;
            }
        }
        #endregion

        #region heuristics
        public int hamming()
        {
            int cnt = 0;
            int ham = 0;
            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    if (arr[i, j] != ++cnt && arr[i, j] != 0)
                    {
                        ham++;
                    }
                }
            }
            return ham;
        }
        public int Manhattan()
        {
            int X = 0, Y = 0, Total = 0;
            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    getCoordinates(arr[i, j], ref X, ref Y);
                    Total += calcDistance(i, j, X, Y);
                }
            }
            return Total;
        }
        #region ManhattanHelperMethods
        int calcDistance(int X1, int Y1, int X2, int Y2)  // calculate distace between two positions                                                        
        {                                                  // on the N Puzzle
            return Math.Abs(X1 - X2) + Math.Abs(Y1 - Y2);
        }
        void getCoordinates(int N, ref int X, ref int Y) // to get the original position 
        {
            X = (N - 1) / arraySize; Y = ((N - 1) % arraySize);
        }
        #endregion

        #endregion heuristics

        #region ClassMethods
        public void NodeDiscovered()
        {
            Colour = "Black";
        }
        public List<Node> AddChildNodes()
        {
            List<Node> Children = new List<Node>();
            int VacantX = 0, VacantY = 0;
            getVacant(ref VacantX, ref VacantY);
            int[,] arrUp = (int[,])this.arr.Clone(), arrDown = (int[,])this.arr.Clone(),
                arrLeft = (int[,])this.arr.Clone(), arrRight = (int[,])this.arr.Clone();
            int Diff = 0;
            if (VacantX > 0)
            {
                arrDown = SwapInt(arrDown, VacantX, VacantY, VacantX - 1, VacantY);
                Diff = calculateDiff(arrDown, VacantX - 1, VacantY, VacantX, VacantY);
                Node NodeDown = new Node(arrDown, (this.priority - Diff) - this.moves, this, "down");
                Children.Add(NodeDown);
            }
            if (VacantX < arraySize - 1)
            {
                arrUp = SwapInt(arrUp, VacantX, VacantY, VacantX + 1, VacantY);
                Diff = calculateDiff(arrUp, VacantX + 1, VacantY, VacantX, VacantY);
                Node NodeUp = new Node(arrUp, (this.priority - Diff) - this.moves, this, "up");
                Children.Add(NodeUp);
            }
            if (VacantY > 0)
            {
                arrRight = SwapInt(arrRight, VacantX, VacantY, VacantX, VacantY - 1);
                Diff = calculateDiff(arrRight, VacantX, VacantY - 1, VacantX, VacantY);
                Node NodeRight = new Node(arrRight, (this.priority - Diff) - this.moves, this, "right");
                Children.Add(NodeRight);
            }
            if (VacantY < arraySize - 1)
            {
                arrLeft = SwapInt(arrLeft, VacantX, VacantY, VacantX, VacantY + 1);
                Diff = calculateDiff(arrLeft, VacantX, VacantY + 1, VacantX, VacantY);
                Node NodeLeft = new Node(arrLeft, (this.priority - Diff) - this.moves, this, "left");
                Children.Add(NodeLeft);
            }
            return Children;
        }
        #region AddChildrenHelperMethods
        int calculateDiff(int[,] Narr, int X, int Y, int NX, int NY)   //calculate difference in priority   
        {                                                              // between the node and its child
            if (priorityType == "hamming")
                return calcualteDiffHamming(Narr, X, Y, NX, NY);
            else if (priorityType == "manhattan")
                return calculateDiffManhattan(Narr, X, Y, NX, NY);
            return 0;
        }
        int calculateDiffManhattan(int[,] Narr, int X, int Y, int NX, int NY)
        {
            int X2 = 0, Y2 = 0;
            getCoordinates(arr[X, Y], ref X2, ref Y2);
            int D1 = calcDistance(X, Y, X2, Y2);
            getCoordinates(Narr[NX, NY], ref X2, ref Y2);
            int D2 = calcDistance(NX, NY, X2, Y2);
            return D1 - D2;
        }
        int calcualteDiffHamming(int[,] Narr, int X, int Y, int NX, int NY)
        {
            int X2 = 0, Y2 = 0;
            getCoordinates(arr[X, Y], ref X2, ref Y2);
            if (X == X2 && Y == Y2)
                return -1;
            else if (NX == X2 && NY == Y2)
                return 1;
            else
                return 0;
        }
        #endregion
        public Stack<string> getPath()
        {
            Stack<string> Path = new Stack<string>();
            Node current = this;
            while (current.moves != 0)
            {
                Path.Push(current.previousMove);
                current = current.Predecessor;
            }
            return Path;
        }
        public bool IsSolvable()
        {
            if (arraySize % 2 == 0)
                return IsSolvableEven();
            else
                return IsSolvableOdd();
        }
        #region IsSolvableHelperMethods
        bool IsSolvableOdd()
        {
            int[] flatArr = new int[(arraySize * arraySize) - 1];
            flattenArray(flatArr);
            int inversions = NumOfAdjacentSwaps(flatArr, flatArr.Length);
            if (inversions % 2 == 0)
                return true;
            else
                return false;
        }
        bool IsSolvableEven()
        {
            int[] flatArr = new int[(arraySize * arraySize) - 1];
            flattenArray(flatArr);
            int inversions = NumOfAdjacentSwaps(flatArr, flatArr.Length);
            int VX = 0, VY = 0;
            getVacant(ref VX, ref VY);
            if ((inversions + VX) % 2 == 0)
                return false;
            else
                return true;
        }
        void flattenArray(int[] flatArr)
        {
            for (int i = 0, cnt = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    if (arr[i, j] == 0)
                        continue;
                    flatArr[cnt++] = arr[i, j];
                }
            }
        }

        static int c;
        int NumOfAdjacentSwaps(int[] A, int N)
        {
            c = 0;
            List<int> list = new List<int>(A);
            list = mergeSort(list);
            return c;
        }  // to calculate num of inversions using merge sort

        List<int> mergeSort(List<int> A)
        {
            if (A.Count <= 1)
                return A;

            List<int> left = new List<int>();
            List<int> right = new List<int>();
            int mid = A.Count / 2;

            for (int i = 0; i < mid; i++)
                left.Add(A[i]);
            for (int i = mid; i < A.Count; i++)
                right.Add(A[i]);

            left = mergeSort(left);
            right = mergeSort(right);
            return merge(left, right);
        }

        List<int> merge(List<int> left, List<int> right)
        {
            List<int> result = new List<int>();
            int i = 0, j = 0;
            while (i < left.Count && j < right.Count)
            {
                if (left[i] < right[j])
                {
                    result.Add(left[i]);
                    i++;
                    c += j;
                }
                else if (left[i] > right[j])
                {
                    result.Add(right[j]);
                    j++;
                }
            }
            while (i < left.Count)
            {
                result.Add(left[i]);
                i++;
                c += j;
            }
            while (j < right.Count)
            {
                result.Add(right[j]);
                j++;
            }
            return result;
        }



        #endregion

        #endregion

        #region opertorOverloading
        public override bool Equals(object obj)
        {
            return Equals(obj as Node);
        }
        public bool Equals(Node obj)
        {
            return EqualArray(this.arr, obj.arr);
        }
        #endregion opertorOverloading

        #region HelperMethods
        static bool EqualArray(int[,] A, int[,] B)
        {
            for (int i = 0; i < Math.Sqrt(A.Length); i++)
                for (int j = 0; j < Math.Sqrt(A.Length); j++)
                    if (A[i, j] != B[i, j])
                        return false;
            return true;
        }
        static int[,] SwapInt(int[,] Arr, int X1, int Y1, int X2, int Y2)
        {
            int tmp;
            tmp = Arr[X1, Y1];
            Arr[X1, Y1] = Arr[X2, Y2];
            Arr[X2, Y2] = tmp;

            return Arr;
        }
        void getVacant(ref int VX, ref int VY)
        {

            for (int i = 0; i < this.arraySize; i++)
            {
                for (int j = 0; j < this.arraySize; j++)
                {
                    if (this.arr[i, j] == 0)
                    {
                        VX = i;
                        VY = j;
                    }
                }
            }
        }
        #endregion HelperMethods
    }
}
