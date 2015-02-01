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
	public class PathSquareFilledEventArgs : EventArgs
	{
		public GridPoint GP { get; set; }
	}
}
