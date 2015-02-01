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
			LabyrinthGameGridForm labyrinthGameGridForm = new LabyrinthGameGridForm(mazeLogic);
			mazeLogic.SquareWallRemoved += labyrinthGameGridForm.RemoveLine;
			mazeLogic.PathSegmentDrawn += labyrinthGameGridForm.FillPathSquare;

			Application.Run(labyrinthGameGridForm);
		}
	}
}
