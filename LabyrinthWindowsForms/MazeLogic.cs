using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LabyrinthWindowsForms
{
	/// <summary>
	/// MazeLogic draws a maze by way of the Create method which uses KnockDownWall to
	/// notify the observer of the event SquareWallRemoved received by the method RemoveLine in
	/// LabyrinthGameGridForm to handle the graphical elements. Likewise the Search method uses
	/// the event PathSegmentDrawn to notify the FillPathSquare method in LabyrinthGameGridForm
	/// for drawing the solution path to the maze.
	///
	/// disjointSet is used for the find/union logic, random is the random generator
	/// for labyrinth path creation, and extendedGraph is the graph used for the search
	/// method.
	///
	/// InitializeMaze initialises a new Maze. 
	/// 
	/// The create method uses find/union disjoint set logic to create a labyrinth while building
	/// up a graph (unweighted). It calls KnockDownWall to handle notifying the UI of the changes.
	///
	/// KnockDownWall(int squareId, GridPoint.Direction dir) calls the other KnockDownWall method.
	/// KnockDownWall(int row, int col, GridPoint.Direction dir) determines the coordinates for
	/// the line to remove and sends the SquareWallRemoved event.
	/// 
	/// The search method uses the unweighted graph algorith, retrieves the path with
	/// GetPath (from the ExtendedGraph class), and then sends the PathSegmentDrawn event for
	/// each path square.
	/// 
	/// GetSquareId gets the integer id of a grid square from the row and
	/// column of a GridPoint. 
	/// 
	/// GetRow gets the row number of a grid square from square id.
	/// 
	/// GetColumn gets the column number of a grid square from square id.
	/// 
	/// IsValid returns if a GridPoint is within the limits of the grid. 
	/// 
	/// The hasValidExit method determines if a point on the grid has a valid exit using
	/// the isValid method from Point, and the unionPossible method.  This is used to
	/// determine when no more exits are possible (the end has been reached).
	///
	/// The validDirection method determines if a randomly chosen direction is valid
	/// using the isValid method from Point, and the unionPossible method.
	/// 
	/// The unionPossible method determines if union between two points is possible.
	///
	/// The calculateUntion method performs the actual union for two points and adds
    /// Edges to extendedGraph for later use by the search method.
  	///
	/// Original Java version:
    /// @author: Robin Osborne and Emma Dirnberger
	/// @version: 2012-05-11
	/// 
	/// C# version:
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
	public class MazeLogic
	{
		private DisjointSets disjointSet;
		private Random random;
		private ExtendedGraph<int> extendedGraph;
		private int maxRows;
		private int maxColumns;
		private int maxSquares;
		private int adjustedSquareSize = 20;
		//private int gridSize = 400;
		//private int rowOffset = (400-20*20)/2 + 2;
		//private int columnOffset = (400-20*20)/2 + 2;
		public EventHandler<LineDrawnEventArgs> SquareWallRemoved = null;
		public EventHandler<PathSquareFilledEventArgs> PathSegmentDrawn = null;
	    private Color backgroundColor = Color.White;

		/// <summary>
		/// The constructor for MazeLogic.
		/// </summary>
		public MazeLogic()
		{
			maxRows = 20;
			maxColumns = 20;
			maxSquares = maxRows * maxColumns;

			//int adjRow = gridSize/maxRows;
			//int adjCol = gridSize/maxColumns;
			//adjustedSquareSize = (adjRow < adjCol ? adjRow : adjCol);
			//rowOffset = (gridSize-maxRows*adjustedSquareSize)/2 + 2; 
			//columnOffset = (gridSize-maxColumns*adjustedSquareSize)/2 + 2; 

			//initialise the disjointSet
			disjointSet = new DisjointSets(maxSquares);
			//initialise the random number generator
			random = new Random();
			//initialise the graph
			extendedGraph = new ExtendedGraph<int>();
		}

		/// <summary>
		/// InitializeMaze initialises a new Maze, creating a new disjointed set, random
		/// generator, and extended graph.
		/// </summary>
		public void InitializeMaze()
		{
			maxSquares = maxRows * maxColumns;

			//initialise the disjointSet
			disjointSet = new DisjointSets(maxSquares);
			//initialise the random number generator
			random = new Random();
			//initialise the graph
			extendedGraph = new ExtendedGraph<int>();
		}

		/// <summary>
		/// The MaxRows property.
		/// </summary>
		public int MaxRows
		{
			get { return maxRows; }
			set { maxRows = value; }
		}

		/// <summary>
		/// The MaxColumns property.
		/// </summary>
		public int MaxColumns
		{
			get { return maxColumns; }
			set { maxColumns = value; }
		}

		/// <summary>
		/// The MaxSquares property.
		/// </summary>
		public int MaxSquares
		{
			get { return maxSquares; }
			set { maxSquares = value; }
		}

		/// <summary>
		/// The SquareSize property.
		/// </summary>
		public int SquareSize
		{
			get { return adjustedSquareSize; }
			set { adjustedSquareSize = value; }
		}

		/// <summary>
		/// KnockDownWall receives the square Id and direction information and
		/// calls a different version KnockDownWall with row and column information.
		/// </summary>
		/// <param name="squareId">the id of the the square</param>
		/// <param name="dir">the direction of the wall to knock down</param>
		private void KnockDownWall(int squareId, GridPoint.Direction dir)
		{
			KnockDownWall(GetRow(squareId), GetColumn(squareId), dir);
		}

		/// <summary>
		/// KnockDownWall receives the row, column and direction information and
		/// calculates the coordinates before sending off the SquareWallRemoved
		/// event with LineDrawnEventArgs.
		/// </summary>
		/// <param name="row">the row of the square</param>
		/// <param name="col">the column of the square</param>
		/// <param name="dir">the direction of the wall to knock down</param>
		private void KnockDownWall(int row, int col, GridPoint.Direction dir)
		{
			// Compute coordinates of line segment to remove
			int c1 = 0, r1 = 0, c2 = 0, r2 = 0;
			if (dir == GridPoint.Direction.UP)
			{
				c1 = col * adjustedSquareSize + 1;
				c2 = (col + 1) * adjustedSquareSize - 1;
				r1 = r2 = row * adjustedSquareSize;
			}
			else if (dir == GridPoint.Direction.RIGHT)
			{
				r1 = row * adjustedSquareSize + 1;
				r2 = (row + 1) * adjustedSquareSize - 1;
				c1 = c2 = (col + 1) * adjustedSquareSize;
			}
			else if (dir == GridPoint.Direction.DOWN)
			{
				c1 = col * adjustedSquareSize + 1;
				c2 = (col + 1) * adjustedSquareSize - 1;
				r1 = r2 = (row + 1) * adjustedSquareSize;
			}
			else if (dir == GridPoint.Direction.LEFT)
			{
				r1 = row * adjustedSquareSize + 1;
				r2 = (row + 1) * adjustedSquareSize - 1;
				c1 = c2 = col * adjustedSquareSize;
			}
			//Notify the observer to erase the line

			LineDrawnEventArgs lineArgs = new LineDrawnEventArgs();
			lineArgs.C1 = c1;
			lineArgs.C2 = c2;
			lineArgs.R1 = r1;
			lineArgs.R2 = r2;
			lineArgs.color = backgroundColor;
			SquareWallRemoved(this, lineArgs);
		}

		/// <summary>
		/// GetSquareId gets the integer id of a grid square from the row and
		/// column of a GridPoint.
		/// </summary>
		/// <param name="p">a grid point object</param>
		/// <returns>an integer id representing square position</returns>
		private int GetSquareId(GridPoint p)
		{
			return p.Row * maxColumns + p.Column;
		}

		/// <summary>
		/// GetRow gets the row number of a grid square from square id.
		/// </summary>
		/// <param name="squareId">the square Id</param>
		/// <returns>the row where that square Id can be found</returns>
		private int GetRow(int squareId)
		{
			return squareId / maxColumns;
		}

		/// <summary>
		/// GetColumn gets the column number of a grid square from square id.
		/// </summary>
		/// <param name="squareId">the square Id</param>
		/// <returns>the column where that square Id can be found</returns>
		private int GetColumn(int squareId)
		{
			return squareId % maxColumns;
		}

		/// <summary>
		/// IsValid returns if a GridPoint is within the limits of the grid. 
		/// </summary>
		/// <param name="p">a grid point</param>
		/// <returns>true if within the grid limits, false if not</returns>
		private bool IsValid(GridPoint p)
		{
			return p.Row >= 0 && p.Row < maxRows && p.Column >= 0 && p.Column < maxColumns;
		}

		/// <summary>
		/// Create builds the maze by cycling through all points in the grid
		/// and determining a random valid exit until no more exits can be found
		/// (which is the final square).
		/// Each GridPoint is connected to another using a union with the disjointSet.
		/// This ensures that separate sections are continually connected, allowing
		/// no duplicate connections, and allowing each GridPoint to be at least connected
		/// to one other GridPoint.
		/// </summary>
		public void Create()
		{
			//Cycle through all points in the grid
			for (int r = 0; r < maxRows; r++)
			{
				for (int c = 0; c < maxColumns; c++)
				{
					//variables
					GridPoint gridPoint = new GridPoint(r, c);
					Pair<int, GridPoint.Direction> pair = null;

					//Set an initial random value and validation check
					int randomNumber = random.Next(4);
					GridPoint newPoint = ValidDirection(randomNumber, gridPoint);

					//Find a random direction that is valid (if one exists)
					while (newPoint == null && HasValidExit(gridPoint))
					{
						randomNumber = random.Next(4);
						newPoint = ValidDirection(randomNumber, gridPoint);
					}

					//The last GridPoint on the grid will not exist, so this
					//skips that particular case
					if (newPoint != null)
					{
						//Perform a union of the involved points and calculate
						//knocking down the wall
						pair = CalculateUnion(gridPoint, newPoint);
						KnockDownWall(GetRow(pair.first), GetColumn(pair.first), pair.second);
					}
				}
			}
		}

		/// <summary>
		/// Search finds the shortest path through the maze using the unweighted graph 
		/// algorthm in Graph and retrieves the path.  The PathSegmentDrawn event is then
		/// sent with PathSquareFilledEventArgs for graphical interpretation.
		/// </summary>
		public void Search()
		{
			if (maxRows == 1 && maxColumns == 1)
			{
				GridPoint gridPoint = new GridPoint(0, 0);
				PathSquareFilledEventArgs pathArgs = new PathSquareFilledEventArgs();
				pathArgs.GP = gridPoint;
				PathSegmentDrawn(this, pathArgs);
			}
			else
			{
				//Runs the unweighted algorithm in Graph with a start GridPoint of cell 0
				extendedGraph.Unweighted(0);
				extendedGraph.PrintPath(maxSquares - 1);
				//Gets the best path to the end square location
				List<int> path = extendedGraph.GetPath(maxSquares - 1);

				//Notifies the observer of the best path through the maze
				if (path != null)
				{
					foreach (int i in path)
					{
						GridPoint gridPoint = new GridPoint(GetRow(i), GetColumn(i));
						PathSquareFilledEventArgs pathArgs = new PathSquareFilledEventArgs();
						pathArgs.GP = gridPoint;
						PathSegmentDrawn(this, pathArgs);
					}
				}
			}
		}

		/// <summary>
		/// HasValidExit determines if a GridPoint on the grid has a valid exit and
		/// returns true if it does.
		/// When the last square has been set, this method should return false.
		/// </summary>
		/// <param name="gridPoint">the GridPoint to check exits from</param>
		/// <returns>true if a valid exit exists, otherwise false</returns>
		private bool HasValidExit(GridPoint gridPoint)
		{
			//Test up
			GridPoint newPoint = new GridPoint(gridPoint.Row, gridPoint.Column);
			newPoint.Move(GridPoint.Direction.UP);

			if ((IsValid(newPoint)) && (UnionPossible(gridPoint, newPoint)))
			{
				return true;
			}

			//Test down        
			newPoint = new GridPoint(gridPoint.Row, gridPoint.Column);
			newPoint.Move(GridPoint.Direction.DOWN);

			if ((IsValid(newPoint)) && (UnionPossible(gridPoint, newPoint)))
			{
				return true;
			}

			//Test left        
			newPoint = new GridPoint(gridPoint.Row, gridPoint.Column);
			newPoint.Move(GridPoint.Direction.LEFT);

			if ((IsValid(newPoint)) && (UnionPossible(gridPoint, newPoint)))
			{
				return true;
			}

			//Test right        
			newPoint = new GridPoint(gridPoint.Row, gridPoint.Column);
			newPoint.Move(GridPoint.Direction.RIGHT);

			if ((IsValid(newPoint)) && (UnionPossible(gridPoint, newPoint)))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// ValidDirection determines if a randomly generated direction from the
		/// initial starting GridPoint is valid, and if the new GridPoint found there can
		/// be connected (union) with the initial GridPoint.
		/// </summary>
		/// <param name="randomNumber">a random number generated to choose direction</param>
		/// <param name="gridPoint">the GridPoint to choose a direction from</param>
		/// <returns>the new GridPoint object if it is valid, otherwise null</returns>
		private GridPoint ValidDirection(int randomNumber, GridPoint gridPoint)
		{
			//Initialises newPoint to the value of GridPoint
			GridPoint newPoint = new GridPoint(gridPoint.Row,gridPoint.Column); 
        
			//Moves the new GridPoint in a direction, depending upon which random
			//direction is indicated
			if(randomNumber == 0) {
				newPoint.Move(GridPoint.Direction.UP); 
			}
			else if(randomNumber == 1) {
				newPoint.Move(GridPoint.Direction.RIGHT); 
			}
			else if(randomNumber == 2) {
				newPoint.Move(GridPoint.Direction.DOWN); 
			}
			else if(randomNumber == 3) {
				newPoint.Move(GridPoint.Direction.LEFT); 
			}          
			else {
				Console.WriteLine("Error in random number");
				return null;
			}
        
			//If the new GridPoint is valid, and union is possible with the first GridPoint
			//return the new GridPoint
			if( (IsValid(newPoint) ) && (UnionPossible(gridPoint, newPoint)) ) {
				return newPoint;
			}
        
			return null;
		}

		/// <summary>
		/// UnionPossible checks if it is possible to join the roots of two points
		/// with the disjointedSet.  If it is possible, the method returns true,
		/// otherwise false.
		/// </summary>
		/// <param name="randomNumber">the first GridPoint</param>
		/// <param name="gridPoint">the second GridPoint</param>
		/// <returns>true if the union was possible, otherwise false</returns>
		private bool UnionPossible(GridPoint gridPoint, GridPoint newPoint)
		{
			int here = GetSquareId(gridPoint);
			int next = GetSquareId(newPoint);
			int hereRoot = 0;
			int nextRoot = 0;

			//find the root of here       
			try
			{
				hereRoot = disjointSet.Find(here);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

			//find the root of next        
			try
			{
				nextRoot = disjointSet.Find(next);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

			//If they are both top roots, return true
			if (hereRoot == 0 && nextRoot == 0)
			{
				return true;
			}

			//if a value of 0 is returned, it is a top root.  Use its square
			//value instead.        
			if (hereRoot == 0)
			{
				hereRoot = here;
			}
			if (nextRoot == 0)
			{
				nextRoot = next;
			}

			//If they do not have the same root, return true
			if (hereRoot != nextRoot)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// CalculateUnion determines the roots involved and performs a union between
		/// two squares values (representing two points in the maze).
		/// On a success it returns a Pair object with the information necessary to later
		/// pass on to observers (a GridPoint, and a direction).  On a failure (which shouldn't
		/// happen) it returns null.
		/// </summary>
		/// <param name="randomNumber">the GridPoint to exit from</param>
		/// <param name="gridPoint">the new GridPoint the exit leads to</param>
		/// <returns>an object of the Pair class, with Integer and GridPoint.Direction values,
		///		or null on failure</returns>
		private Pair<int, GridPoint.Direction> CalculateUnion(GridPoint gridPoint, GridPoint newPoint)
		{           
			//Variables
			int here = GetSquareId(gridPoint); //cell value of GridPoint   
			int next = GetSquareId(newPoint);  //cell value of newPoint   
			int hereRoot = 0;
			int nextRoot = 0;    
          
			//find the root of here
			try {
				hereRoot = disjointSet.Find(here);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
             
			//find the root of next
			try {
				nextRoot = disjointSet.Find(next);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}                        
               
			//if a value of 0 is returned, it is a top root.  Use its square
			//value instead.
			if(hereRoot == 0) {
				hereRoot = here; 
			}
			else if(nextRoot == 0) {
				nextRoot = next;
			}
            
			//attempt union, and return a Pair object if successful
			try {
				disjointSet.Union(nextRoot, hereRoot);  
				//Adds edges to the extended graph
				extendedGraph.AddEdge(here, next, 1);
				extendedGraph.AddEdge(next, here, 1);            
            
				return(new Pair<int,GridPoint.Direction>(here,gridPoint.GetDirection(newPoint)));                  
			}
			catch (Exception e)
			{  
				MessageBox.Show("Union error: "+here+" "+next+" ; "+e.Message);
			}
        
			return null;
		}
	}
}
