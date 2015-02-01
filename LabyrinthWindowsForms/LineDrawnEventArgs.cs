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
	public class LineDrawnEventArgs : EventArgs
	{
		public int C1 { get; set; }
		public int R1 { get; set; }
		public int C2 { get; set; }
		public int R2 { get; set; }
		public System.Drawing.Color color { get; set; }
	}
}
