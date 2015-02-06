using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabyrinthWindowsForms
{
	/// <summary>
	/// The LabyrinthProgram runs a WindowsForms application where the user can enter row and
	/// column and a random maze is created that the program can then be instructed to solve
	/// for the shortest path.
	/// 
	/// This is a C# adaptation of a school project written in Java. The Java versions of
	/// MazeLogic and ExtendedGraph were written by Robin Osborne and Emma Dirnberger.
	/// DisjointSets, Graph, GridPoint and Pair were part of example Java code provided by
	/// Mark Allen Weiss and Uno Holmer.
	/// 
	/// IPriorityQueue and PriorityQueue are untested implementations of C# example code by
	/// Leon van Bokhorst at
	/// http://www.remondo.net/generic-priority-queue-example-csharp/ which was chosen for
	/// similarity to the previous linked list implementation.
	/// 
	/// All C# adaptations were done by Robin Osborne. The LineDrawnEventArgs,
	/// PathSquareFilledEventArgs and LabyrinthGameGridForm are unique to the C# version and
	/// written by Robin Osborne.
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
	static class LabyrinthProgram
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			MazeLogic mazeLogic = new MazeLogic();
			LabyrinthMazeGridForm labyrinthGameGridForm = new LabyrinthMazeGridForm(mazeLogic);
			mazeLogic.SquareWallRemoved += labyrinthGameGridForm.RemoveLine;
			mazeLogic.PathSegmentDrawn += labyrinthGameGridForm.FillPathSquare;

			Application.Run(labyrinthGameGridForm);
		}
	}
}
