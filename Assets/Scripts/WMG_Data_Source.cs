using System;
using System.Collections.Generic;
using UnityEngine;

public class WMG_Data_Source : MonoBehaviour
{
	private void Start()
	{
		if (this.variableNames == null)
		{
			this.variableNames = new List<string>();
		}
		this.parseStrings();
	}

	private void parseStrings()
	{
		this.varNames1.Clear();
		this.varNames2.Clear();
		for (int i = 0; i < this.variableNames.Count; i++)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			this.parseString(this.variableNames[i], ref empty, ref empty2);
			this.varNames1.Add(empty);
			this.varNames2.Add(empty2);
		}
		this.parseString(this.variableName, ref this.varName1, ref this.varName2);
	}

	private void parseString(string inputString, ref string stringPart1, ref string stringPart2)
	{
		string[] array = inputString.Split(new char[]
		{
			'.'
		});
		stringPart1 = string.Empty;
		stringPart2 = string.Empty;
		if (array.Length < 2)
		{
			return;
		}
		stringPart1 = array[0];
		stringPart2 = array[1];
	}

	public void setVariableNames(List<string> variableNames)
	{
		this.variableNames.Clear();
		foreach (string item in variableNames)
		{
			this.variableNames.Add(item);
		}
		this.parseStrings();
	}

	public void setVariableName(string variableName)
	{
		this.variableName = variableName;
		this.parseString(variableName, ref this.varName1, ref this.varName2);
	}

	public void addVariableNameToList(string variableName)
	{
		this.variableNames.Add(variableName);
		this.parseStrings();
	}

	public void removeVariableNameFromList(string variableName)
	{
		this.variableNames.Remove(variableName);
		this.parseStrings();
	}

	public void setDataProviders<T>(List<T> dataProviderList)
	{
		this.dataProviders.Clear();
		foreach (T t in dataProviderList)
		{
			this.dataProviders.Add(t);
		}
	}

	public void setDataProvider<T>(T dataProvider)
	{
		this.dataProvider = dataProvider;
	}

	public void addDataProviderToList<T>(T dataProvider)
	{
		this.dataProviders.Add(dataProvider);
	}

	public bool removeDataProviderFromList<T>(T dataProvider)
	{
		return this.dataProviders.Remove(dataProvider);
	}

	public List<T> getData<T>()
	{
		if (this.dataSourceType == WMG_Data_Source.WMG_DataSourceTypes.Multiple_Objects_Single_Variable)
		{
			List<T> list = new List<T>();
			foreach (object dp in this.dataProviders)
			{
				list.Add(this.getDatum<T>(dp, this.variableName, this.variableType, this.varName1, this.varName2));
			}
			return list;
		}
		if (this.dataSourceType == WMG_Data_Source.WMG_DataSourceTypes.Single_Object_Multiple_Variables)
		{
			List<T> list2 = new List<T>();
			for (int i = 0; i < this.variableNames.Count; i++)
			{
				string text = this.variableNames[i];
				WMG_Data_Source.WMG_VariableTypes varType = WMG_Data_Source.WMG_VariableTypes.Not_Specified;
				if (i < this.variableTypes.Count)
				{
					varType = this.variableTypes[i];
				}
				if (i >= this.varNames1.Count)
				{
					this.parseStrings();
				}
				list2.Add(this.getDatum<T>(this.dataProvider, text, varType, this.varNames1[i], this.varNames2[i]));
			}
			return list2;
		}
		if (this.dataSourceType == WMG_Data_Source.WMG_DataSourceTypes.Single_Object_Single_Variable)
		{
			try
			{
				return (List<T>)WMG_Reflection.GetField(this.dataProvider.GetType(), this.variableName).GetValue(this.dataProvider);
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log("Field: " + this.variableName + " not found. " + ex.Message);
				return new List<T>();
			}
		}
		return new List<T>();
	}

	public T getDatum<T>()
	{
		if (this.dataSourceType == WMG_Data_Source.WMG_DataSourceTypes.Single_Object_Single_Variable)
		{
			return this.getDatum<T>(this.dataProvider, this.variableName, this.variableType, this.varName1, this.varName2);
		}
		UnityEngine.Debug.Log("getDatum() is not supported for dataSourceType not equal to Single_Object_Single_Variable.");
		return default(T);
	}

	private T getDatum<T>(object dp, string variableName, WMG_Data_Source.WMG_VariableTypes varType, string vName1, string vName2)
	{
		if (varType == WMG_Data_Source.WMG_VariableTypes.Field)
		{
			try
			{
				return (T)((object)WMG_Reflection.GetField(dp.GetType(), variableName).GetValue(dp));
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log("Field: " + variableName + " not found. " + ex.Message);
				return default(T);
			}
		}
		if (varType == WMG_Data_Source.WMG_VariableTypes.Property)
		{
			try
			{
				return (T)((object)WMG_Reflection.GetProperty(dp.GetType(), variableName).GetValue(dp, null));
			}
			catch (Exception ex2)
			{
				UnityEngine.Debug.Log("Property: " + variableName + " not found. " + ex2.Message);
				return default(T);
			}
		}
		if (varType == WMG_Data_Source.WMG_VariableTypes.Property_Field)
		{
			try
			{
				object value = WMG_Reflection.GetProperty(dp.GetType(), vName1).GetValue(dp, null);
				return (T)((object)WMG_Reflection.GetField(value.GetType(), vName2).GetValue(value));
			}
			catch (Exception ex3)
			{
				UnityEngine.Debug.Log("property.field: " + variableName + " not found. " + ex3.Message);
				return default(T);
			}
		}
		if (varType == WMG_Data_Source.WMG_VariableTypes.Field_Field)
		{
			try
			{
				object value2 = WMG_Reflection.GetField(dp.GetType(), vName1).GetValue(dp);
				return (T)((object)WMG_Reflection.GetField(value2.GetType(), vName2).GetValue(value2));
			}
			catch (Exception ex4)
			{
				UnityEngine.Debug.Log("field.field: " + variableName + " not found. " + ex4.Message);
				return default(T);
			}
		}
		if (varType == WMG_Data_Source.WMG_VariableTypes.Not_Specified)
		{
			try
			{
				return (T)((object)WMG_Reflection.GetField(dp.GetType(), variableName).GetValue(dp));
			}
			catch
			{
				try
				{
					return (T)((object)WMG_Reflection.GetProperty(dp.GetType(), variableName).GetValue(dp, null));
				}
				catch
				{
					try
					{
						object value3 = WMG_Reflection.GetProperty(dp.GetType(), vName1).GetValue(dp, null);
						return (T)((object)WMG_Reflection.GetField(value3.GetType(), vName2).GetValue(value3));
					}
					catch
					{
						try
						{
							object value4 = WMG_Reflection.GetField(dp.GetType(), this.varName1).GetValue(dp);
							return (T)((object)WMG_Reflection.GetField(value4.GetType(), vName2).GetValue(value4));
						}
						catch (Exception ex5)
						{
							UnityEngine.Debug.Log("field, property, property.field, or field.field: " + variableName + " not found. " + ex5.Message);
							return default(T);
						}
					}
				}
			}
		}
		return default(T);
	}

	public WMG_Data_Source.WMG_DataSourceTypes dataSourceType;

	public List<object> dataProviders = new List<object>();

	public object dataProvider;

	public List<WMG_Data_Source.WMG_VariableTypes> variableTypes = new List<WMG_Data_Source.WMG_VariableTypes>();

	public WMG_Data_Source.WMG_VariableTypes variableType;

	public List<string> variableNames;

	public string variableName;

	private List<string> varNames1 = new List<string>();

	private string varName1;

	private List<string> varNames2 = new List<string>();

	private string varName2;

	public enum WMG_DataSourceTypes
	{
		Single_Object_Multiple_Variables,
		Multiple_Objects_Single_Variable,
		Single_Object_Single_Variable
	}

	public enum WMG_VariableTypes
	{
		Not_Specified,
		Field,
		Property,
		Property_Field,
		Field_Field
	}
}
