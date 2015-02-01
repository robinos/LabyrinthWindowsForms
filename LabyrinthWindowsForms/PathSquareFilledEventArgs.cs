using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthWindowsForms
{
	/// <summary>
	/// The PathSquareFilledEventArgs class inherits from EventArgs and is used
	/// with the PathSegmentDrawn event in MazeLogic and received by the FillPathSquare
	/// method in LabyrinthGameGridForm.
	/// It contains a GridPoint that can be passed as data to an observer method.
	/// </summary>
	public class PathSquareFilledEventArgs : EventArgs
	{
		public GridPoint GP { get; set; }
	}
}
