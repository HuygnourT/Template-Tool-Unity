using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum DataGameMode
{
    Single_Player,
    Two_Player
}

[System.Serializable]
public class DataInGame
{
    public List<DataMode> dataGameModes = new List<DataMode>();
    //[HideInInspector]
    public string jsonGameMode;

    public void Save()
    {
        //for (int i = 0; i < dataGameModes.Count; i++)
        //{
        //    dataGameModes[i].Save();
        //}
        jsonGameMode = JsonArray.ToJson<DataMode>(dataGameModes.ToArray());
        PlayerPrefs.SetString("DataInGame", jsonGameMode);

        //DataMode[] dataSaved = JsonArray.FromJson<DataMode>(PlayerPrefs.GetString("DataInGame"));
        //Debug.Log(dataSaved);


    }
    public void SaveDataMode(DataMode dataMode)
    {
        dataMode.Save();
        Save();
    }

    public void Load()
    {
        Debug.Log("After Load Data :"+ jsonGameMode);
        dataGameModes.Clear();


        if (!string.IsNullOrEmpty(jsonGameMode))
        {   

            DataMode[] datas = JsonArray.FromJson<DataMode>(jsonGameMode);
            for (int i = 0; i < datas.Length; i++)
            {
                dataGameModes.Add(datas[i]);
            }
        }
        else
        {
            foreach (var enumValue in Enum.GetValues(typeof(DataGameMode)))
            {
                DataMode mode = new DataMode();
                mode.dataGameMode = (DataGameMode)enumValue;
                mode.bestScore = 0;
                mode.currentScore = 0;
                dataGameModes.Add(mode);
            }
            Save();
        }
    }


    public DataMode FindDataMode(DataGameMode dataGameMode)
    {
        return dataGameModes.Find(x => x.dataGameMode == dataGameMode);
    }
}

[System.Serializable]
public class DataMode
{
    public DataGameMode dataGameMode;
    public int currentScore;
    public int bestScore;

    public void Save()
    {

    }

    public void ClearData()
    {
        currentScore = 0;
    }

}
