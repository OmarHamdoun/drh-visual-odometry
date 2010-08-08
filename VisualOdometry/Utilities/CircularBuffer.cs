using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace VisualOdometry.Utilities
{
	public class CircularBuffer<T>
	{
		private int m_Size;
		private T[] m_History;
		private int m_IndexForNextValue = 0;
		private int m_Count = 0;

		public CircularBuffer(int size)
		{
			m_Size = size;
			m_History = new T[m_Size];
		}

		public int Size
		{
			get { return m_Size; }
		}

		public int Count
		{
			get { return m_Count; }
		}

		public virtual void Add(T value)
		{
			if (m_Count < m_Size)
			{
				m_Count++;
			}

			m_History[m_IndexForNextValue] = value;
			m_IndexForNextValue = (m_IndexForNextValue + 1) % m_Size;
		}

		public bool IsFull
		{
			get { return (m_Count == m_Size); }
		}

		public virtual T this[int index]
		{
			get
			{
				int internalIndex = (m_Size + m_IndexForNextValue - m_Count + index) % m_Size;
				return m_History[internalIndex];
			}
		}

		internal virtual void PrintContent(string heading)
		{
			Debug.WriteLine(heading);
			for (int i = 0; i < m_Size; i++)
			{
				Debug.WriteLine("{0}: {1}", i, this[i]);
			}
			Debug.WriteLine("Is full: " + this.IsFull.ToString());
		}
	}
}
