using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthWindowsForms
{
	/// <summary>
	/// An implementation of a generic priority queue where the generic type must implement
	/// the generic IComparable.
	/// 
	/// PriorityQueue comes from C# example code by Leon van Bokhorst at
	/// http://www.remondo.net/generic-priority-queue-example-csharp/ and was chosen for
	/// similarity to the previous linked list implementation.
	/// 
	/// This code is untested since the Dijkstra's algorithm method in Graph that would use
	/// it is not actually used in this project.
	/// </summary>
	/// <typeparam name="T">a type that implements IComparable</typeparam>
	public class PriorityQueue<T> : IPriorityQueue<T> where T : IComparable<T>
	{
        private readonly LinkedList<T> _items;
 
        public PriorityQueue()
        {
            _items = new LinkedList<T>();
        }
 
        #region IPriorityQueue<T> Members
 
        public void Enqueue(T item)
        {
            if (IsEmpty)
            {
                _items.AddFirst(item);
                return;
            }
 
            LinkedListNode<T> existingItem = _items.First;
 
            while (existingItem != null && existingItem.Value.CompareTo(item) < 0)
            {
                existingItem = existingItem.Next;
            }
 
            if (existingItem == null)
                _items.AddLast(item);
            else
            {
                _items.AddBefore(existingItem, item);
            }
        }
 
        public T Dequeue()
        {
            T value = _items.First.Value;
            _items.RemoveFirst();
 
            return value;
        }
 
        public T Peek()
        {
            return _items.First.Value;
        }
 
        public bool IsEmpty
        {
            get { return _items.Count == 0; }
        }
 
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
 
        #endregion
	}
}
