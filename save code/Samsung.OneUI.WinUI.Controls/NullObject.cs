using System.ComponentModel;

namespace Samsung.OneUI.WinUI.Controls;

public struct NullObject<T>
{
	[DefaultValue(true)]
	private readonly bool isnull;

	public T Item { get; private set; }

	private NullObject(T item, bool isnull)
	{
		this = default(NullObject<T>);
		this.isnull = isnull;
		Item = item;
	}

	public NullObject(T item)
		: this(item, item == null)
	{
	}

	public static NullObject<T> Null()
	{
		return default(NullObject<T>);
	}

	public bool IsNull()
	{
		return isnull;
	}

	public static implicit operator T(NullObject<T> nullObject)
	{
		return nullObject.Item;
	}

	public static implicit operator NullObject<T>(T item)
	{
		return new NullObject<T>(item);
	}

	public override string ToString()
	{
		if (Item == null)
		{
			return "NULL";
		}
		return Item.ToString();
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return IsNull();
		}
		if (!(obj is NullObject<T> nullObject))
		{
			return false;
		}
		if (IsNull())
		{
			return nullObject.IsNull();
		}
		if (nullObject.IsNull())
		{
			return false;
		}
		return Item.Equals(nullObject.Item);
	}

	public override int GetHashCode()
	{
		if (isnull)
		{
			return 0;
		}
		int num = Item.GetHashCode();
		if (num >= 0)
		{
			num++;
		}
		return num;
	}
}
