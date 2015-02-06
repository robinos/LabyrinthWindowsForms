using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabyrinthWindowsForms
{
	/// <summary>
	/// LabyrinthGameGridForm contains all code that directly interfaces with the form of
	/// the same name.
	/// The RemoveLine method is an observer to the EventHandler SquareWallRemoved in the
	/// MazeLogic class. This means that event fires for each wall that the algorithm
	/// calculates must be destroyed and RemoveLine then reacts by erasing that line/wall.
	/// FillPathSquare is an observer to the EventHandler PathSegmentDrawn, also in the MazeLogic
	/// class. For each square that forms the path out of the maze, the PathSegmentDrawn event is
	/// fired and the FillPathSquare method reacts by putting a red circle in that square.
	/// 
	/// -constructor-
	/// LabyrinthMazeGridForm(MazeLogic mazeLogic) - initialise variables, graphics and form
	/// 
	/// -methods-
	/// InitializeGraphics - re-initialises the maze grid before each new maze is created 
	/// DrawGrid - draws the grid squares
	/// RemoveLine - removes a wall of a square in an up, down, left or right direction
	///		(watches SquareWallRemoved in MazeLogic)
	/// DrawLine - draws a line from a column, row coordinate to another, with the given color
	/// FillPathSquare - fills a square with a red circle (watches PathSegmentDrawn in MazeLogic)
	/// MazePanel_Paint - paints the maze graphics onto the maze panel with the same graphics
	///		object used to create the panel
	/// SearchButton_Click - When the search button is clicked, the Search() method in MazeLogic
	///		is called.
	///	NewButton_Click - When the new button is clicked, the Create() method in MazeLogic is
	///		called.
	/// ColumnBox_TextChanged - When a value is entered in the ColumnBox textbox the minimum is
	///		enforced as 1 and the maximum as 20
	/// RowBox_TextChanged - As above, but for RowBox.
	/// ColumnBox_KeyPress - Calls the AcceptOnlyNumbers method.
	/// RowBox_KeyPress - As above, but for RowBox.
	/// AcceptOnlyNumbers - A method called by ColumnBox_KeyPress and RowBox_KeyPress to ensure
	///		only numbers can be entered into the textbox.
	/// 
	/// @author: Robin Osborne
	/// @version: 0.1.0
	/// @date: 2015-01-30
	/// 
	/// Copyright (C) 2015 Robin Osborne
	/// This program is free software: you can redistribute it and/or modify
	/// it under the terms of the GNU General Public License as published by
	/// the Free Software Foundation, either version 3 of the License, or
	/// (at your option) any later version.

	/// This program is distributed in the hope that it will be useful,
	/// but WITHOUT ANY WARRANTY; without even the implied warranty of
	/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	/// GNU General Public License for more details.
	/// You should have received a copy of the GNU General Public License
	/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
	/// </summary>
	public partial class LabyrinthMazeGridForm : Form
	{
		private Bitmap mazeImage;
		private Graphics mazeGraphics;
		private MazeLogic mazeLogic;
		private int maxSquares;
		private int squareSize;
		private int startColumn = 0;
		private int startRow = 0;
		private int maxRows;
		private int maxColumns;
		private int maxRowSize;
		private int maxColumnSize;
		private Color backgroundColor = Color.White;
		private Color lineColor = Color.Black;
		private Color pathColor = Color.Red;

		/// <summary>
		/// The constructor for LabyrinthGameGridForm
		/// </summary>
		/// <param name="mazeLogic">the MazeLogic object that will run logical operations for
		///		this form</param>
		public LabyrinthMazeGridForm(MazeLogic mazeLogic)
		{
			this.mazeLogic = mazeLogic;

			//set some variable defaults from mazeLogics properties
			maxRows = mazeLogic.MaxRows;
			maxColumns = mazeLogic.MaxColumns;
			maxSquares = mazeLogic.MaxSquares;
			squareSize = mazeLogic.SquareSize;

			//calculate the total size of the maze grid
			maxRowSize = maxRows * squareSize;
			maxColumnSize = maxColumns * squareSize;

			//initialise the form
			InitializeComponent();

			//At this point mazeImage is null.
			//This will assign new values using the size of the panel
			mazeImage = new Bitmap(MazePanel.Width, MazePanel.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			//Set the mazeImage as a background image to the panel
			MazePanel.BackgroundImage = mazeImage;

			//create the mazeGraphics graphics object from the mazeImage
			mazeGraphics = Graphics.FromImage(mazeImage);

			//Fills the images we just created with white
			mazeGraphics.Clear(backgroundColor);
		}

		/// <summary>
		/// InitializeGraphics starts a new grid with the start and end line segments removed.
		/// </summary>
		private void InitializeGraphics()
		{
			//draw the game grid
			DrawGrid();
			//create the entrance
			DrawLine(startColumn, startRow, startColumn, squareSize, backgroundColor);
			//create the exit
			DrawLine(maxColumnSize, maxRowSize - squareSize, maxColumnSize, maxRowSize, backgroundColor);
		}

		/// <summary>
		/// DrawGrid draws the basic game grid with rectangles.
		/// </summary>
		private void DrawGrid()
		{
			for (int x = startColumn; x < maxColumnSize; x += squareSize)
			{
				for (int y = startRow; y < maxRowSize; y += squareSize)
				{
					mazeGraphics.DrawRectangle(new Pen(lineColor), new Rectangle(x, y, squareSize, squareSize));
				}
			}

			this.Refresh();
		}

		/// <summary>
		/// RemoveLine is an observer of the SquareWallRemoved event in MazeLogic.
		/// It calls DrawLine to remove the correct line.
		/// </summary>
		/// <param name="sender">the sending object (MazeLogic)</param>
		/// <param name="e">LineDrawnEvenArgs which contains start and end column and row
		///		with desired color</param>
		public void RemoveLine(object sender, LineDrawnEventArgs e)
		{
			DrawLine(e.C1, e.R1, e.C2, e.R2, e.color);
		}

		/// <summary>
		/// DrawLine draws a line from c1, r1 to c2, r2 in the given color.
		/// </summary>
		/// <param name="c1">column 1 position</param>
		/// <param name="r1">row 1 position</param>
		/// <param name="c2">column 2 position</param>
		/// <param name="r2">row 2 position</param>
		/// <param name="color">the desired line color</param>
		public void DrawLine(int c1, int r1, int c2, int r2, Color color)
		{
			mazeGraphics.DrawLine(new Pen(color), new Point(c1,r1), new Point(c2,r2));
			this.Refresh();
		}

		/// <summary>
		/// FillPathSquare is an observer of the PathSegmentDrawn event in MazeLogic.
		/// It draws a circle using the path color at the corrdinates given by the 
		/// pathSquareFilledArgs GridPoint within a rectangular bounding box.
		/// </summary>
		/// <param name="sender">the sending object (MazeLogic)</param>
		/// <param name="pathSquareFilledArgs">the arguments containing the GridPoint</param>
		public void FillPathSquare(object sender, PathSquareFilledEventArgs pathSquareFilledArgs)
		{
			GridPoint gridPoint = pathSquareFilledArgs.GP;
			int columnCoord = gridPoint.Column * squareSize;
			int rowCoord = gridPoint.Row * squareSize;

			mazeGraphics.FillEllipse(new SolidBrush(pathColor), new Rectangle(columnCoord, rowCoord, squareSize, squareSize));

			this.Refresh();
		}

		/// <summary>
		/// MazePanel_Paint draws the maze image onto the MazePanel using the same Graphics
		/// object used to paint the MazePanel. Alternately MazePanel.CreateGraphics() could
		/// have been used, but there is no reason not to use the PaintEventArgs.
		/// </summary>
		/// <param name="sender">the sending object (MazePanel)</param>
		/// <param name="e">the paint event arguments</param>
		private void MazePanel_Paint(object sender, PaintEventArgs e)
		{
			 e.Graphics.DrawImageUnscaled(mazeImage, new Point(startColumn, startRow));
		}

		/// <summary>
		/// SearchButton_Click starts a search for a path through the current maze, disabling
		/// itself. Once finished it enables the new maze button and the text boxes for custom
		/// column and row.
		/// </summary>
		/// <param name="sender">the sending object (SearchButton)</param>
		/// <param name="e">the event arguments</param>
		private void SearchButton_Click(object sender, EventArgs e)
		{
			SearchButton.Enabled = false;
			mazeLogic.Search();
			NewButton.Enabled = true;
			ColumnBox.Enabled = true;
			RowBox.Enabled = true;
		}

		/// <summary>
		/// NewButton_Click initialises graphics for a new maze using the current values for column and
		/// row, disabling itself and the text boxes. It calls the mazeLogic object to initialise and
		/// create the maze. Once finished it enables the search button.
		/// </summary>
		/// <param name="sender">the sending object (NewButton)</param>
		/// <param name="e">the event arguments</param>
		private void NewButton_Click(object sender, EventArgs e)
		{
			mazeGraphics.Clear(backgroundColor);
			InitializeGraphics();
			NewButton.Enabled = false;
			ColumnBox.Enabled = false;
			RowBox.Enabled = false;
			mazeLogic.InitializeMaze();
			mazeLogic.Create();
			SearchButton.Enabled = true;
		}

		/// <summary>
		/// ColumnBox_TextChanged changes the maximum column value depending
		/// upon the value entered into the text box.
		/// </summary>
		/// <param name="sender">the sender object (ColumnBox)</param>
		/// <param name="e">the event arguments</param>
		private void ColumnBox_TextChanged(object sender, EventArgs e)
		{
			int col;
			if(int.TryParse(ColumnBox.Text, out col))
			{
				//If a value is higher than 20, it becomes 20.
				if (col > 20)
				{
					ColumnBox.Text = "20";
					col = 20;
				}
				//If a value is lower than 1, it becomes 1.
				else if(col < 1)
				{
					ColumnBox.Text = "1";
					col = 1;
				}

				mazeLogic.MaxColumns = col;
				maxColumns = col;
				maxColumnSize = col * squareSize;
			}
		}

		/// <summary>
		/// RowBox_TextChanged changes the maximum row value depending
		/// upon the value entered into the text box.
		/// </summary>
		/// <param name="sender">the sender object (RowBox)</param>
		/// <param name="e">the event arguments</param>
		private void RowBox_TextChanged(object sender, EventArgs e)
		{
			int row;
			if (int.TryParse(RowBox.Text, out row))
			{
				//If a value is higher than 20, it becomes 20.
				if (row > 20)
				{
					RowBox.Text = "20";
					row = 20;
				}
				//If a value is lower than 1, it becomes 1.
				else if (row < 1)
				{
					RowBox.Text = "1";
					row = 1;
				}

				mazeLogic.MaxRows = row;
				maxRows = row;
				maxRowSize = row * squareSize;
			}
		}

		/// <summary>
		/// ColumnBox_KeyPress calls upon the AcceptOnlyNumbers method to
		/// hinder the user from entering anything but numeric characters.
		/// </summary>
		/// <param name="sender">the sender object (ColumnBox)</param>
		/// <param name="e">the key press event arguments</param>
		private void ColumnBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			AcceptOnlyNumbers(e);
		}

		/// <summary>
		/// RowBox_KeyPress calls upon the AcceptOnlyNumbers method to
		/// hinder the user from entering anything but numeric characters.
		/// </summary>
		/// <param name="sender">the sender object (RowBox)</param>
		/// <param name="e">the key press event arguments</param>
		private void RowBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			AcceptOnlyNumbers(e);
		}

		/// <summary>
		/// AcceptOnlyNumbers dissallows all characters that are not numbers.
		/// </summary>
		/// <param name="e">key press event arguments</param>
		private void AcceptOnlyNumbers(KeyPressEventArgs e)
		{
			//Only numbers
			if (!Char.IsDigit(e.KeyChar))
			{
				if (!(e.KeyChar == Convert.ToChar(Keys.Back)))
					e.Handled = true;
			}
			base.OnKeyPress(e);
		}
	}
}
