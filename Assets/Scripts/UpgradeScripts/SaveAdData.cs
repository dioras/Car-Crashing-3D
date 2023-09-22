using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SaveAdData : MonoBehaviour
{
    public static SaveAdData Instance { get; private set; }

    public AdDataList adDataList = new ();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadData();
    }

    private void OnDestroy()
    {
        SaveData();
    }

    private void SaveData()
    {
        var data = JsonUtility.ToJson(adDataList);
        print(data);
        PlayerPrefs.SetString("AdData", data);
        PlayerPrefs.Save();
    }

    public int GetValue(string id)
    {
        foreach (var adData in adDataList.list)
        {
            if (adData.id == id) return adData.adCount;
        }
        
        var tempData = new AdData
        {
            id = id,
            adCount = 0
        };
        adDataList.list.Add(tempData);

        return 0;
    }

    public void SetKey(string id, int adCountToWatch)
    {
        print(adDataList.list);
        
        foreach (var adData in adDataList.list)
        {
            if (adData.id != id) continue;
            
            adData.adCount++;

            return;
        }
    }
    
    public void ResetKey(string id)
    {
        foreach (var adData in adDataList.list)
        {
            if (adData.id != id) continue;
            
            adData.adCount = 0;

            return;
        }
    }

    private void LoadData()
    {
        var dataTemp = PlayerPrefs.GetString("AdData", "");
        print(dataTemp);
        var data = JsonUtility.FromJson<AdDataList>(dataTemp);
        if (data != null)
            adDataList = data;
        else
        {
            adDataList.list = new List<AdData>();
        }
    }
}

[Serializable]
public class AdDataList
{
    public List<AdData> list;
}

[Serializable]
public class AdData
{
    public string id;
    public int adCount;
}


