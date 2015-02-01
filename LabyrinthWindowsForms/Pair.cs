using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthWindowsForms
{
	/// <summary>
	/// Pair represents a generic pair of values. This could probably have been done
	/// with the C# Tuple class, but Pair was part of the original Java code.
	/// </summary>
	/// <typeparam name="A">type A</typeparam>
	/// <typeparam name="B">type B</typeparam>
	public class Pair<A, B>
	{
		public Pair(A first, B second)
		{
			this.first = first;
			this.second = second;
		}

		public A first;
		public B second;
	}
}
