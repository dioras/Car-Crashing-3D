using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class WMG_List<T> : IEnumerable<T>, IEnumerable
{
	public WMG_List()
	{
		this.list = new List<T>();
	}

	public List<T> list { get; private set; }

	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<bool, bool, bool, int> Changed;



	public void SetList(IEnumerable<T> collection)
	{
		List<T> list = new List<T>(this.list);
		this.list = new List<T>(collection);
		if (list.Count == this.list.Count)
		{
			bool flag = false;
			int num = -1;
			for (int i = 0; i < list.Count; i++)
			{
				T t = list[i];
				if (!t.Equals(this.list[i]))
				{
					if (flag)
					{
						this.Changed(false, false, false, -1);
						return;
					}
					num = i;
					flag = true;
				}
			}
			if (num != -1)
			{
				this.Changed(false, false, true, num);
			}
		}
		else
		{
			this.Changed(false, true, false, -1);
		}
	}

	public void SetListNoCb(IEnumerable<T> collection, ref List<T> _list)
	{
		this.list = new List<T>(collection);
		_list = new List<T>(collection);
	}

	public int Count
	{
		get
		{
			return this.list.Count;
		}
	}

	public IEnumerator<T> GetEnumerator()
	{
		return this.list.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.list.GetEnumerator();
	}

	public void Add(T item)
	{
		this.list.Add(item);
		this.Changed(false, true, false, -1);
	}

	public void AddNoCb(T item, ref List<T> _list)
	{
		this.list.Add(item);
		_list.Add(item);
	}

	public void Remove(T item)
	{
		this.list.Remove(item);
		this.Changed(false, true, false, -1);
	}

	public void RemoveAt(int index)
	{
		this.list.RemoveAt(index);
		this.Changed(false, true, false, -1);
	}

	public void RemoveAtNoCb(int index, ref List<T> _list)
	{
		this.list.RemoveAt(index);
		_list.RemoveAt(index);
	}

	public void AddRange(IEnumerable<T> collection)
	{
		this.list.AddRange(collection);
		this.Changed(false, true, false, -1);
	}

	public void RemoveRange(int index, int count)
	{
		this.list.RemoveRange(index, count);
		this.Changed(false, true, false, -1);
	}

	public void Clear()
	{
		this.list.Clear();
		this.Changed(false, true, false, -1);
	}

	public void Sort()
	{
		this.list.Sort();
		this.Changed(false, false, false, -1);
	}

	public void Sort(Comparison<T> comparison)
	{
		this.list.Sort(comparison);
		this.Changed(false, false, false, -1);
	}

	public void Insert(int index, T item)
	{
		this.list.Insert(index, item);
		this.Changed(false, true, false, -1);
	}

	public void InsertRange(int index, IEnumerable<T> collection)
	{
		this.list.InsertRange(index, collection);
		this.Changed(false, true, false, -1);
	}

	public void RemoveAll(Predicate<T> match)
	{
		this.list.RemoveAll(match);
		this.Changed(false, true, false, -1);
	}

	public void Reverse()
	{
		this.list.Reverse();
		this.Changed(false, false, false, -1);
	}

	public void Reverse(int index, int count)
	{
		this.list.Reverse(index, count);
		this.Changed(false, false, false, -1);
	}

	public T this[int index]
	{
		get
		{
			return this.list[index];
		}
		set
		{
			this.list[index] = value;
			this.Changed(false, false, true, index);
		}
	}

	public void SetValNoCb(int index, T val, ref List<T> _list)
	{
		this.list[index] = val;
		_list[index] = val;
	}

	public void SizeChangedViaEditor()
	{
		this.Changed(true, true, false, -1);
	}

	public void ValueChangedViaEditor(int index)
	{
		this.Changed(true, false, true, index);
	}

	public void SetListViaEditor(IEnumerable<T> collection)
	{
		this.list = new List<T>(collection);
	}

	public void SetValViaEditor(int index, T val)
	{
		this.list[index] = val;
	}
}
