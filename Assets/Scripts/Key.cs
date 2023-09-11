using System;
using UnityEngine;

public class Key
{
	public static Key MakeKey(string name, KeyType type)
	{
		return new Key
		{
			Name = name,
			Type = type
		};
	}

	public static Key MakeKey(string name, long lastRefresh, bool uploaded, KeyType type)
	{
		return new Key
		{
			Name = name,
			Type = type,
			LastRefresh = lastRefresh,
			Uploaded = uploaded
		};
	}

	public void Populate()
	{
		Key key = Key.ValueFromPlayerPrefs(this);
		this.Int = key.Int;
		this.Float = key.Float;
		this.Bool = key.Bool;
		this.Long = key.Long;
		this.String = key.String;
	}

	public string ValueAsString()
	{
		string result = string.Empty;
		switch (this.Type)
		{
		case KeyType.String:
			result = this.String;
			break;
		case KeyType.Int:
			result = this.Int.ToString();
			break;
		case KeyType.Bool:
			result = this.Bool.ToString();
			break;
		case KeyType.Long:
			result = this.Long.ToString();
			break;
		case KeyType.Float:
			result = this.Float.ToString();
			break;
		}
		return result;
	}

	public static Key ValueFromPlayerPrefs(Key key)
	{
		switch (key.Type)
		{
		case KeyType.String:
			key.String = PlayerPrefs.GetString(key.Name, "DIDNOTEXIST");
			if (key.String == "DIDNOTEXIST")
			{
				key.Existed = false;
				key.String = null;
			}
			break;
		case KeyType.Int:
			key.Float = (float)PlayerPrefs.GetInt(key.Name, -99999);
			if ((float)key.Int == -100000f)
			{
				key.Int = 0;
				key.Existed = false;
			}
			break;
		case KeyType.Bool:
		{
			int @int = PlayerPrefs.GetInt(key.Name, -99999);
			if (@int == -99999)
			{
				key.Existed = false;
			}
			else
			{
				key.Bool = (@int == 1);
			}
			break;
		}
		case KeyType.Long:
		{
			string @string = PlayerPrefs.GetString(key.Name, "DIDNOTEXIST");
			if (@string == "DIDNOTEXIST")
			{
				key.Existed = false;
			}
			else
			{
				key.Float = float.Parse(@string);
			}
			break;
		}
		case KeyType.Float:
			key.Float = PlayerPrefs.GetFloat(key.Name, -100000f);
			if (key.Float == -100000f)
			{
				key.Float = 0f;
				key.Existed = false;
			}
			break;
		}
		return key;
	}

	public string Name = string.Empty;

	public KeyType Type;

	public long LastRefresh;

	public long Version;

	public bool Uploaded;

	public long Long;

	public int Int;

	public string String = string.Empty;

	public float Float;

	public bool Bool;

	public bool Existed;
}
