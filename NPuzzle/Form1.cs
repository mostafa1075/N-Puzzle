using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NPuzzle
{
    public partial class Puzzle : Form
    {
        static Node root;
        static Label[,] puzzleLabels;
        const double c = 0.00000000001;
        static Stack<string> nextMove = new Stack<string>();  // to hold moves left to reach goal
        static Stack<string> previousMove = new Stack<string>();  // to hold moves left to reach start
        static Point Vacant = new Point();
        public Puzzle()
        {
            InitializeComponent();
            #region initializePuzzle
            Reading_From_File();
            if (root.ArraySize > 10)
                return;

            for (int i = 0; i < root.ArraySize; i++)
                for (int j = 0; j < root.ArraySize; j++)
                    if (root.Arr[i, j] == 0)
                    {
                        Vacant = new Point(i, j);
                        break;
                    }


            puzzleLabels = new Label[root.ArraySize, root.ArraySize];
            Panel p = new Panel();
            int sideLength = 75;
            int PanelLocX = 285;
            int PanelLocY = 30;
            p.Height = 10 + sideLength * (root.ArraySize);
            p.Width = 10 + sideLength * (root.ArraySize);
            p.Location = new Point(PanelLocX, PanelLocY);
            p.BackColor = Color.Gray;
            p.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

            for (int i = 0; i < root.ArraySize; i++)
            {             // drawing the puzzle labels and assigning each label its position 
                for (int j = 0; j < root.ArraySize; j++)
                {
                    puzzleLabels[i, j] = new Label();
                    puzzleLabels[i, j].Font = new System.Drawing.Font(puzzleLabels[i, j].Font.FontFamily.Name, 20);
                    puzzleLabels[i, j].Location = new Point(PanelLocX + 2 + j * (sideLength + 2), PanelLocY + 2 + (i) * (sideLength + 2));
                    puzzleLabels[i, j].BackColor = Color.White;
                    puzzleLabels[i, j].ForeColor = Color.Black;
                    puzzleLabels[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    puzzleLabels[i, j].Height = sideLength;
                    puzzleLabels[i, j].Width = sideLength;
                    if (root.Arr[i, j] == 0)
                    {
                        puzzleLabels[i, j].BackColor = Color.Gray;
                    }
                    else
                    {
                        puzzleLabels[i, j].Text = root.Arr[i, j].ToString();
                        puzzleLabels[i, j].BorderStyle = BorderStyle.FixedSingle;
                    }
                    this.Controls.Add(puzzleLabels[i, j]);
                }
            }
            this.Controls.Add(p);
            #endregion
        }

        #region Reading from file
        public void Reading_From_File()
        {
            int Len;
            FileStream f = new FileStream("Puzzle.txt", FileMode.Open);
            StreamReader r = new StreamReader(f);
            Len = int.Parse(r.ReadLine());
            int[,] Arr = new int[Len, Len];
            string[] s2;
            for (int i = 0; i < Len;)
            {
                string s = r.ReadLine();

                s2 = s.Split(' ');

                for (int j = 0; j < Len; j++)
                {
                    if (s2[i] == "")
                    {
                        --i;
                        break;
                    }
                    Arr[i, j] = int.Parse(s2[j]);
                }

                if (++i == Len)
                {
                    break;
                }
            }
            root = new Node(Arr, Len, 0, "manhattan");
            r.Close();
        }
        #endregion


        #region AstarHelperMethods
        static string arrToString(int[,] arr) // flatten the 2d array and convert it to string
        {
            string s = "";
            for (int i = 0; i < root.ArraySize; i++)
                for (int j = 0; j < root.ArraySize; j++)
                {
                    s += arr[i, j].ToString();
                }
            return s;
        }
        static double getKey(int[,] arr) // get the key which is unique to each Node
        {
            double key = 0;
            int cnt = 1;
            for (int i = 0; i < root.ArraySize; i++)
                for (int j = 0; j < root.ArraySize; j++)
                {
                    key += arr[i, j] * cnt++;
                }
            double d = Math.Pow(10, (int)(Math.Log10(key)) + 1);
            key = d - key;
            key /= d;
            return key;
        }
        #endregion
        static Node AStarAlgorithm(Node Start, Node Goal)
        {
            PriorityQueue PriorityQueue = new PriorityQueue();
            Dictionary<string, Node> Removed = new Dictionary<string, Node>();
            Dictionary<string, Node> openList = new Dictionary<string, Node>();
            double d = c;
            Node current;
            PriorityQueue.Enqueue(Start.Priority, Start);
            openList.Add(arrToString(Start.Arr), Start);
            while (true)
            {
                current = PriorityQueue.Dequeue();
                string s = arrToString(current.Arr);
                openList.Remove(s);
                if (current.Equals(Goal))
                    return current;


                if (!Removed.ContainsKey(s))
                    Removed.Add(s, current);

                current.NodeDiscovered();
                foreach (Node Child in current.AddChildNodes())
                {
                    s = arrToString(Child.Arr);
                    if ((!Removed.ContainsKey(s)) && (!openList.ContainsKey(s)))
                    {
                        double key = getKey(Child.Arr);
                        PriorityQueue.Enqueue(Child.Priority + key + d, Child);
                        openList.Add(s, Child);
                        d += c;
                    }
                }
            }
        }

        private void Solve_Click(object sender, EventArgs e)
        {
            if (hamming.Checked) // assign priority type according to radio buttons
                root.PriorityType = "hamming";
            else if (manhattan.Checked)
                root.PriorityType = "manhattan";

            int[,] G = new int[root.ArraySize, root.ArraySize];
            if (!root.IsSolvable())
            {
                MessageBox.Show("Not Solvable");
                return;
            }
            int cnt = 0;
            for (int i = 0; i < root.ArraySize; i++)
            {
                for (int j = 0; j < root.ArraySize; j++)
                {
                    G[i, j] = ++cnt;
                }
            }
            G[root.ArraySize - 1, root.ArraySize - 1] = 0;
            Node Goal = new Node(G, root.ArraySize, 0, root.PriorityType);
            long timeBefore = System.Environment.TickCount;
            Goal = AStarAlgorithm(root, Goal);
            long timeAfter = System.Environment.TickCount;

            nextMove = Goal.getPath();
            if (root.ArraySize > 10)
            {
                string s = "";
                while (nextMove.Count != 0)
                {
                    s += reverseMove(nextMove.Pop());
                    s += ",";
                }
                MessageBox.Show(s);
            }
            else
            {
                NextMove.Enabled = true;
                playAllMoves.Enabled = true;
            }
            textBox1.Text = (timeAfter - timeBefore).ToString() + " ms";
            textBox2.Text = Goal.Moves.ToString();
            GC.Collect();
        }

        #region Animation
        private void nextMove_Click(object sender, EventArgs e)
        {
            moveLabel(nextMove.Peek());

            previousMove.Push(reverseMove(nextMove.Pop()));
            PreviousMove.Enabled = true;
            if (nextMove.Count == 0)
            {
                movesTimer.Stop();
                playAllMoves.Enabled = false;
            }
            if (nextMove.Count == 0)
                NextMove.Enabled = false;
        }
        private void PreviousMove_Click(object sender, EventArgs e)
        {
            moveLabel(previousMove.Peek());
            nextMove.Push(reverseMove(previousMove.Pop()));
            NextMove.Enabled = true;
            playAllMoves.Enabled = true;
            if (previousMove.Count == 0)
                PreviousMove.Enabled = false;
        }
        Timer movesTimer;
        private void playAllMoves_Click(object sender, EventArgs e)
        {
            movesTimer = new Timer();
            movesTimer.Tick += new EventHandler(nextMove_Click);
            movesTimer.Interval = 300;
            movesTimer.Enabled = true;
            movesTimer.Start();
            playAllMoves.Enabled = false;
        }
        #region AnimationHelperMethods

        private void moveLabel(string newMove)
        {

            if (newMove == "up")
                swapWithVacant(Vacant.X + 1, Vacant.Y);
            else if (newMove == "down")
                swapWithVacant(Vacant.X - 1, Vacant.Y);
            else if (newMove == "left")
                swapWithVacant(Vacant.X, Vacant.Y + 1);
            else if (newMove == "right")
                swapWithVacant(Vacant.X, Vacant.Y - 1);
        }
        private void swapWithVacant(int X, int Y) //move label from current position to the vacant position
        {
            int DY = Vacant.X - X, DX = Vacant.Y - Y;
            puzzleLabels[X, Y].Location = new Point(puzzleLabels[X, Y].Location.X + DX * 77, puzzleLabels[X, Y].Location.Y + DY * 77);
            puzzleLabels[Vacant.X, Vacant.Y].Location = new Point(puzzleLabels[Vacant.X, Vacant.Y].Location.X - DX * 77, puzzleLabels[Vacant.X, Vacant.Y].Location.Y - DY * 77);
            swapLabels(ref puzzleLabels[X, Y], ref puzzleLabels[Vacant.X, Vacant.Y]);
            Vacant = new Point(X, Y);
        }
        private string reverseMove(string move)
        {
            if (move == "up")
                return "down";
            else if (move == "down")
                return "up";
            else if (move == "left")
                return "right";
            else if (move == "right")
                return "left";
            return "none";
        }
        private void swapLabels(ref Label l1, ref Label l2)
        {
            Label tmp = l1;
            l1 = l2;
            l2 = tmp;
        }
        #endregion
        #endregion
    }
}
