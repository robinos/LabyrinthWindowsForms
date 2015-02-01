using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthWindowsForms
{
	/// <summary>
	/// A interface for a generic IPriorityQueue that implements the generic version of
	/// IEnumerable and requires T to be of a type that implements the generic IComparable.
	/// </summary>
	/// <typeparam name="T">a type that implements IComparable(T)"/></typeparam>
	interface IPriorityQueue<T> : IEnumerable<T> where T : IComparable<T>
	{
        bool IsEmpty { get; }
        void Enqueue(T item);
        T Dequeue();
        T Peek();
	}
}
