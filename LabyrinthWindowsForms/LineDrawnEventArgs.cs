using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthWindowsForms
{
	/// <summary>
	/// The LineDrawnEventArgs class inherits from EventArgs and is used with the
	/// SquareWallRemoved event in MazeLogic and received by the RemoveLine method
	/// in LabyrinthGameGridForm.
	/// It contains a four integers for 2 column and row pairs and a color
	/// that can be passed as data to an observer method. The line is meant to be
	/// drawn from C1,R1 coordinates to C2,R2 coordinates in the provided color.
	/// </summary>
	public class LineDrawnEventArgs : EventArgs
	{
		public int C1 { get; set; }
		public int R1 { get; set; }
		public int C2 { get; set; }
		public int R2 { get; set; }
		public System.Drawing.Color color { get; set; }
	}
}
