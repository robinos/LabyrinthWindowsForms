using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabyrinthWindowsForms
{
	/// <summary>
	/// ExtendedGraph is a subclass of Graph that adds the methods of GetPath and
	/// GetPathRecursive. GetPath is a public version that is called with a desired
	/// integer while GetPathRecursive is a private recursive method that returns the
	/// optimal path in the form of a list of integers.
	/// 
	/// *Note that the Vertex class is defined within Graph.cs
	/// 
	/// @author Robin Osborne and Emma Dirnberger
	/// @version 2012-05-11
	/// 
	/// 
	/// C# conversion:
	/// Small syntax changes and the reversal of the returned list by GetPath.
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
	public class ExtendedGraph<T> : Graph<T>
	{
		/// <summary>
		/// GetPath calls GetPathRecursive to receive the optimal path through the
		/// labyrinth, which it then returns.
		/// </summary>
		/// <param name="destName">the desired destination to get a path to</param>
		/// <returns>a list of integers (squares) that form that path</returns>
		public List<T> GetPath(T destName)
		{
			List<T> path = null;          
			Vertex<T> vertex = vertexMap[destName];    	
    	
			if( vertex == null )
				throw new Exception( "Destination vertex not found" );    		
			else if( vertex.dist == INFINITY )
				Console.WriteLine(destName + " is unreachable");
			else
			{
				//Calls GetPathRecursive for teh path
				path = GetPathRecursive(vertex);
			}

			//The list comes back reversed, so turn it back around
			path.Reverse();

    		return path;
		}

		/// <summary>
		/// GetPathRecursive is a recursive method that gets a list of the integers
		/// (squares) that make up the path to a destination vertex, and then
		/// returns the final list.
		/// Basically, this starts at the destination and finds a path backwards to
		/// the start.
		/// </summary>
		/// <param name="destName">the vertex destination to get the path to</param>
		/// <returns>a list of integers (squares) that form a path to he given vertex</returns>
		private List<T> GetPathRecursive(Vertex<T> dest)
		{
			List<T> path = new List<T>();

			path.Add(dest.name);

			//Calls itself until the previous destination is null
			if (dest.prev != null)
			{
				path.AddRange(GetPathRecursive(dest.prev));
			}

			return path;
		}    
	}
}
