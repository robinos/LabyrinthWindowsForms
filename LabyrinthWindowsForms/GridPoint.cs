using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthWindowsForms
{
	/// <summary>
	/// The GridPoint class represents a point of the maze grid with methods for
	/// determining direction and which other GridPoint (if any) can be found in
	/// the directions UP, RIGHT, DOWN and LEFT.
	/// </summary>
	public class GridPoint
	{
		public enum Direction { UP, RIGHT, DOWN, LEFT, ERROR }

		private int row = 0, col = 0;

		/// <summary>
		/// The constructor of GridPoint.
		/// </summary>
		/// <param name="row">the row of the point on the grid</param>
		/// <param name="col">the column of the point on the grid </param>
		public GridPoint(int row, int col)
		{
			this.row = row;
			this.col = col;
		}

		public int Row { get { return row; } set { row = value; } }
		public int Column { get { return col; } set { col = value; } }

		/// <summary>
		/// GetDirection gets the direction to another point which is assumed to
		/// be at perpendicular offset from this object.
		/// </summary>
		/// <param name="target">the target GridPoint to compare with</param>
		/// <returns></returns>
		public GridPoint.Direction GetDirection(GridPoint target)
		{
			if (target.row < row) return Direction.UP;
			else if (target.row > row) return Direction.DOWN;
			else if (target.col < col) return Direction.LEFT;
			else if (target.col > col) return Direction.RIGHT;
			else
				return Direction.ERROR;
		}

		/// <summary>
		/// Moves the GridPoint one step in the given direction.
		/// </summary>
		/// <param name="d">a direction as defined by the Direction enum</param>
		public void Move(Direction d)
		{
			switch (d)
			{
				case Direction.UP: row--; break;
				case Direction.RIGHT: col++; break;
				case Direction.DOWN: row++; break;
				case Direction.LEFT: col--; break;
			}
		}
	}
}
