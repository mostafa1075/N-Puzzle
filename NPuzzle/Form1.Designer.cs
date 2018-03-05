namespace NPuzzle
{
    partial class Puzzle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Solve = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.manhattan = new System.Windows.Forms.RadioButton();
            this.hamming = new System.Windows.Forms.RadioButton();
            this.NextMove = new System.Windows.Forms.Button();
            this.PreviousMove = new System.Windows.Forms.Button();
            this.playAllMoves = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Solve
            // 
            this.Solve.Font = new System.Drawing.Font("Bodoni MT Condensed", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Solve.Location = new System.Drawing.Point(139, 104);
            this.Solve.Name = "Solve";
            this.Solve.Size = new System.Drawing.Size(100, 40);
            this.Solve.TabIndex = 1;
            this.Solve.Text = "Solve";
            this.Solve.UseVisualStyleBackColor = true;
            this.Solve.Click += new System.EventHandler(this.Solve_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(139, 62);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(108, 23);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(139, 19);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(108, 23);
            this.textBox2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Number of moves";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Book Antiqua", 11.25F);
            this.label2.Location = new System.Drawing.Point(6, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Elapsed Time";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.manhattan);
            this.groupBox1.Controls.Add(this.hamming);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Solve);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(15, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 160);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // manhattan
            // 
            this.manhattan.AutoSize = true;
            this.manhattan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manhattan.Location = new System.Drawing.Point(10, 126);
            this.manhattan.Name = "manhattan";
            this.manhattan.Size = new System.Drawing.Size(93, 21);
            this.manhattan.TabIndex = 7;
            this.manhattan.TabStop = true;
            this.manhattan.Text = "Manhattan";
            this.manhattan.UseVisualStyleBackColor = true;
            // 
            // hamming
            // 
            this.hamming.AutoSize = true;
            this.hamming.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hamming.Location = new System.Drawing.Point(10, 99);
            this.hamming.Name = "hamming";
            this.hamming.Size = new System.Drawing.Size(85, 21);
            this.hamming.TabIndex = 6;
            this.hamming.TabStop = true;
            this.hamming.Text = "Hamming";
            this.hamming.UseVisualStyleBackColor = true;
            // 
            // NextMove
            // 
            this.NextMove.Enabled = false;
            this.NextMove.Location = new System.Drawing.Point(92, 215);
            this.NextMove.Name = "NextMove";
            this.NextMove.Size = new System.Drawing.Size(74, 30);
            this.NextMove.TabIndex = 7;
            this.NextMove.Text = ">>";
            this.NextMove.UseVisualStyleBackColor = true;
            this.NextMove.Click += new System.EventHandler(this.nextMove_Click);
            // 
            // PreviousMove
            // 
            this.PreviousMove.Enabled = false;
            this.PreviousMove.Location = new System.Drawing.Point(12, 215);
            this.PreviousMove.Name = "PreviousMove";
            this.PreviousMove.Size = new System.Drawing.Size(74, 30);
            this.PreviousMove.TabIndex = 8;
            this.PreviousMove.Text = "<<";
            this.PreviousMove.UseVisualStyleBackColor = true;
            this.PreviousMove.Click += new System.EventHandler(this.PreviousMove_Click);
            // 
            // playAllMoves
            // 
            this.playAllMoves.Enabled = false;
            this.playAllMoves.Location = new System.Drawing.Point(172, 215);
            this.playAllMoves.Name = "playAllMoves";
            this.playAllMoves.Size = new System.Drawing.Size(103, 30);
            this.playAllMoves.TabIndex = 9;
            this.playAllMoves.Text = "Play";
            this.playAllMoves.UseVisualStyleBackColor = true;
            this.playAllMoves.Click += new System.EventHandler(this.playAllMoves_Click);
            // 
            // Puzzle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::NPuzzle.Properties.Resources.blue_science_fiction_background;
            this.ClientSize = new System.Drawing.Size(604, 362);
            this.Controls.Add(this.playAllMoves);
            this.Controls.Add(this.PreviousMove);
            this.Controls.Add(this.NextMove);
            this.Controls.Add(this.groupBox1);
            this.Name = "Puzzle";
            this.Text = "NPuzzle";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Solve;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button NextMove;
        private System.Windows.Forms.Button PreviousMove;
        private System.Windows.Forms.Button playAllMoves;
        private System.Windows.Forms.RadioButton manhattan;
        private System.Windows.Forms.RadioButton hamming;
    }
}

