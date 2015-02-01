using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthWindowsForms
{
	/// <summary>
	/// Graph class: evaluate shortest paths.
	///
	/// CONSTRUCTION: with no parameters.
	///
	/// ******************PUBLIC OPERATIONS**********************
	/// void addEdge( int v, int w, double cvw )
	///                              --> Add additional edge
	/// void printPath( int w )   --> Print path after alg is run
	/// void unweighted( int s )  --> Single-source unweighted
	/// void dijkstra( int s )    --> Single-source weighted
	/// ******************ERRORS*********************************
	/// Some error checking is performed to make sure graph is ok,
	/// and to make sure graph satisfies properties needed by each
	/// algorithm.  Exceptions are thrown if errors are detected.
	/// 
	/// C# conversion:
	/// As priority queues are not standard structures in c# at the writing of this
	/// program, a version from x has been implemented.
	/// Minor syntax changes from Java to C# were also required.
	/// 
	/// The Edge, Vertex, and SearchPath classes can be found after the Graph class
	/// in this file.
	/// 
	/// @conversion author: Robin Osborne
	/// @version: 0.1.0
	/// @date: 2015-01-30
	/// </summary>	
	public class Graph<T>
	{
		public const double INFINITY = double.MaxValue;
		protected Dictionary<T, Vertex<T>> vertexMap = new Dictionary<T, Vertex<T>>();

		/// <summary>
		/// AddEdge adds an Edge to the Graph. The names of vertices are integers.
		/// </summary>
		/// <param name="sourceName">the name of the source Vertex</param>
		/// <param name="destName">the name of the destination Vertex</param>
		/// <param name="cost">the cost of moving from source to destination</param>
		public void AddEdge(T sourceName, T destName, double cost)
		{
			Vertex<T> v = GetVertex(sourceName);
			Vertex<T> w = GetVertex(destName);
			v.adj.AddLast(new Edge<T>(w, cost));
		}

		/// <summary>
		/// GetVertex gets a vertex from the Dictionary map of all vertices, or creates
		/// a new one if it does not exist within the Dictionary vertexMap.
		/// </summary>
		/// <param name="vertexName">the integer name of the Vertex to get</param>
		/// <returns>a Vertex (vertexName from vertexMap or a new Vertex)</returns>
		private Vertex<T> GetVertex(T vertexName)
		{
			Vertex<T> v = null;

			if (vertexMap.ContainsKey(vertexName))
				v = vertexMap[vertexName];

			if(v == null)
			{
				v = new Vertex<T>(vertexName);
				vertexMap.Add(vertexName, v);
			}

			return v;
		}

		/// <summary>
		/// Routine to handle unreachables and print total cost.
		/// It calls a recursive routine to print the shortest
		/// path to destNode after a shortest path alogirthm has run.
		/// </summary>
		/// <param name="destName"></param>
		public void PrintPath(T destName)
		{
			if (!vertexMap.ContainsKey(destName))
				throw new Exception("Destination not found!");

			Vertex<T> w = vertexMap[destName];

			if( w == null )
				throw new Exception("Destination vertex not found");
			else if( w.dist == INFINITY )
				Console.WriteLine(destName + " is unreachable");
			else
			{
				Console.Write("(Cost is: " + w.dist + ") ");
				PrintPathRecursive(w);
				Console.WriteLine();
			}			
		}

		/// <summary>
		/// Recursive routine to print shortest path to dest after running shortest
		/// path algorithm. The path is known to exist.
		/// </summary>
		/// <param name="dest">the destination Vertex to print a path to</param>
		private void PrintPathRecursive(Vertex<T> dest)
		{
			if( dest.prev != null )
			{
				PrintPathRecursive(dest.prev);
				Console.Write(" to ");
			}
			Console.Write(dest.name);
		}

		/// <summary>
		/// Initializes the vertex output info prior to running any shortest path
		/// algorithm.
		/// </summary>
		private void ClearAll()
		{
			foreach(Vertex<T> v in vertexMap.Values)
				v.Reset( );
		}

		/// <summary>
		/// Single-source unweighted shortest-path algorithm.
		/// This is the algorithm used by Labyrinth to calculate shortest path by using
		/// a queue structure.
		/// </summary>
		/// <param name="startName">the integer name of the starting Vertex</param>
		public void Unweighted(T startName)
		{
			ClearAll( );

			if (!vertexMap.ContainsKey(startName))
				throw new Exception("Start not found!");

			Vertex<T> start = vertexMap[startName];
			if( start == null )
				throw new Exception("Start vertex not found");

			Queue<Vertex<T>> q = new Queue<Vertex<T>>( );
			q.Enqueue(start);
			start.dist = 0;

			while(q.Count!=0)
			{
				Vertex<T> v = q.Dequeue();

				foreach(Edge<T> e in v.adj)
				{
					Vertex<T> w = e.dest;
					if(w.dist == INFINITY)
					{
						w.dist = v.dist + 1;
						w.prev = v;
						q.Enqueue(w);
					}
				}
			}
		}

		/// <summary>
		/// Single-source weighted shortest-path algorithm.
		/// The Dijkstra algorithm is not used by Labyrinth but was included in the
		/// previous code and has been updated to use a custom priority queue.
		/// 
		/// This code conversion is untested.
		/// </summary>
		/// <param name="startName">the integer name of the starting Vertex</param>
		public void Dijkstra(T startName)
		{
			PriorityQueue<SearchPath<T>> pq = new PriorityQueue<SearchPath<T>>( );

			if (!vertexMap.ContainsKey(startName))
				throw new Exception("Start not found!");

			Vertex<T> start = vertexMap[startName];

			if( start == null )
				throw new Exception( "Start vertex not found" );

			ClearAll( );
			pq.Enqueue(new SearchPath<T>(start, 0));
			start.dist = 0;
        
			int nodesSeen = 0;
			while(!pq.IsEmpty && nodesSeen < vertexMap.Count)
			{
				SearchPath<T> vrec = pq.Dequeue();
				Vertex<T> v = vrec.dest;
				if( v.scratch != 0 )  // already processed v
					continue;
                
				v.scratch = 1;
				nodesSeen++;

				foreach(Edge<T> e in v.adj)
				{
					Vertex<T> w = e.dest;
					double cvw = e.cost;
                
					if( cvw < 0 )
						throw new Exception( "Graph has negative edges" );
                    
					if( w.dist > v.dist + cvw )
					{
						w.dist = v.dist +cvw;
						w.prev = v;
						pq.Enqueue(new SearchPath<T>( w, w.dist));
					}
				}
			}
		}


	}

	/// <summary>
	/// The Edge class represents an edge in the graph - a destination and the cost
	/// associated with reaching it.
	/// </summary>
	public class Edge<T>
	{
		public Vertex<T> dest;
		public double cost;

		/// <summary>
		/// The constructor for Edge.
		/// </summary>
		/// <param name="dest">the destination Vertex</param>
		/// <param name="cost">the cost value associated with that destination</param>
		public Edge(Vertex<T> dest, double cost)
		{
			this.dest = dest;
			this.cost = cost;
		}
	}


	/// <summary>
	/// The Vertex class represents a Vertex with name (as an int), a LinkedList of connected
	/// edges, a distance value, a previous Vertex value, and a scratch value.
	/// </summary>
	public class Vertex<T>
	{
		public T name;
		public LinkedList<Edge<T>> adj;
		public double dist;
		public Vertex<T> prev;
		public int scratch;

		/// <summary>
		/// The constructor for Vertex sets the integer name for that Vertex, creates
		/// an empty LinkedList for adding connected Edges and resets the Vertex.
		/// </summary>
		/// <param name="nm">an integer name for the Vertex</param>
		public Vertex(T nm)
		{
			name = nm;
			adj = new LinkedList<Edge<T>>();
			Reset();
		}

		/// <summary>
		/// Reset sets the default distance value as the max value for a double (defined in
		/// the Graph class at the beginning), sets the previous Vertex to null and the 
		/// scratch value to 0.
		/// </summary>
		public void Reset()
		{
			dist = Graph<T>.INFINITY;
			prev = null;
			scratch = 0;
		}
	}

	/// <summary>
	/// The SearchPath class represents an entry in the priority queue for Dijkstra's
	/// algorithm.  It implements the IComparable interface for itself.
	/// </summary>
	public class SearchPath<T> : IComparable<SearchPath<T>>
	{
		public Vertex<T> dest;
		public double cost;

		/// <summary>
		/// The constructor for SearchPath.
		/// </summary>
		/// <param name="d">a Vertex as a destination</param>
		/// <param name="c">a double as a cost value</param>
		public SearchPath(Vertex<T> d, double c)
		{
			dest = d;
			cost = c;
		}

		/// <summary>
		/// CompareTo is required for the implementation of IComparable.
		/// It allows comparing one SearchPath with another based on cost.
		/// </summary>
		/// <param name="rhs">the right hand side SearchPath</param>
		/// <returns>-1 for lesser cost, 0 for same, or 1 for greater cost</returns>
		public int CompareTo(SearchPath<T> rhs)
		{
			double otherCost = rhs.cost;
			return cost < otherCost ? -1 : cost > otherCost ? 1 : 0;
		}
	}
}
