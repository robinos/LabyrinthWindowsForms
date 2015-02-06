namespace LabyrinthWindowsForms
{
	partial class LabyrinthMazeGridForm
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
			this.MazePanel = new System.Windows.Forms.Panel();
			this.SearchButton = new System.Windows.Forms.Button();
			this.NewButton = new System.Windows.Forms.Button();
			this.RowBox = new System.Windows.Forms.TextBox();
			this.ColumnBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// MazePanel
			// 
			this.MazePanel.Location = new System.Drawing.Point(226, 3);
			this.MazePanel.Name = "MazePanel";
			this.MazePanel.Size = new System.Drawing.Size(401, 401);
			this.MazePanel.TabIndex = 0;
			// 
			// SearchButton
			// 
			this.SearchButton.Enabled = false;
			this.SearchButton.Location = new System.Drawing.Point(72, 224);
			this.SearchButton.Name = "SearchButton";
			this.SearchButton.Size = new System.Drawing.Size(83, 23);
			this.SearchButton.TabIndex = 1;
			this.SearchButton.Text = "Sök";
			this.SearchButton.UseVisualStyleBackColor = true;
			this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
			// 
			// NewButton
			// 
			this.NewButton.Location = new System.Drawing.Point(72, 195);
			this.NewButton.Name = "NewButton";
			this.NewButton.Size = new System.Drawing.Size(83, 23);
			this.NewButton.TabIndex = 2;
			this.NewButton.Text = "Skapa ny";
			this.NewButton.UseVisualStyleBackColor = true;
			this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
			// 
			// RowBox
			// 
			this.RowBox.Location = new System.Drawing.Point(129, 169);
			this.RowBox.MaxLength = 2;
			this.RowBox.Name = "RowBox";
			this.RowBox.Size = new System.Drawing.Size(26, 20);
			this.RowBox.TabIndex = 3;
			this.RowBox.Text = "20";
			this.RowBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.RowBox.TextChanged += new System.EventHandler(this.RowBox_TextChanged);
			this.RowBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RowBox_KeyPress);
			// 
			// ColumnBox
			// 
			this.ColumnBox.Location = new System.Drawing.Point(129, 143);
			this.ColumnBox.MaxLength = 2;
			this.ColumnBox.Name = "ColumnBox";
			this.ColumnBox.Size = new System.Drawing.Size(26, 20);
			this.ColumnBox.TabIndex = 4;
			this.ColumnBox.Text = "20";
			this.ColumnBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.ColumnBox.TextChanged += new System.EventHandler(this.ColumnBox_TextChanged);
			this.ColumnBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ColumnBox_KeyPress);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(69, 146);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Kolumner:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(84, 172);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Rader:";
			// 
			// LabyrinthGameGridForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 407);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ColumnBox);
			this.Controls.Add(this.RowBox);
			this.Controls.Add(this.NewButton);
			this.Controls.Add(this.SearchButton);
			this.Controls.Add(this.MazePanel);
			this.Name = "LabyrinthGameGridForm";
			this.Text = "Labyrinth";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel MazePanel;
		private System.Windows.Forms.Button SearchButton;
		private System.Windows.Forms.Button NewButton;
		private System.Windows.Forms.TextBox RowBox;
		private System.Windows.Forms.TextBox ColumnBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}

