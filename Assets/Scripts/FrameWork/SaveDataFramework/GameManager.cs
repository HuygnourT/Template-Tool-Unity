using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DataInGame dataInGame;

    private DataMode currentDataMode;

    public DataGameMode CurrentDataGameMode = DataGameMode.Single_Player;

    public Text currentModeText;
    public Text highestScoresText;
    public Text currentScoresText;

    private int currentScore = 0;

    

    private void Awake()
    {
        LoadData();
        OnGameInitialized();

        //string dataTest = PlayerPrefs.GetString("TestClass", string.Empty);

        //if (!string.IsNullOrEmpty(dataTest))
        //{
        //    TestClass t = JsonUtility.FromJson<TestClass>(dataTest);

        //    Debug.Log(t.name + " " + t.score);
        //}


        //TestClass testClass = new TestClass("Huy", 10);
        //string toJson = JsonUtility.ToJson(testClass);

        //PlayerPrefs.SetString("TestClass", toJson);

    }

    public void Bam()
    {   
        currentScoresText.text = "Current Scores : "+(++currentDataMode.currentScore);
        if (currentDataMode.currentScore > currentDataMode.bestScore)
            currentDataMode.bestScore = currentDataMode.currentScore;
        highestScoresText.text = "Highest Scores : " + currentDataMode.bestScore;
    }

    public void Save()
    {
        if (currentDataMode.currentScore > currentDataMode.bestScore)
            currentDataMode.bestScore = currentDataMode.currentScore;

        currentDataMode.currentScore = 0;


        dataInGame.Save();


        highestScoresText.text = "Highest Scores : " + currentDataMode.bestScore;

        currentScoresText.text = "Current Scores : " + (currentDataMode.currentScore);


    }   

    public void SetGameMode(int indexGameMode)
    {
        switch (indexGameMode)
        {
            case 0:
                CurrentDataGameMode = DataGameMode.Single_Player;
                break;
            case 1:
                CurrentDataGameMode = DataGameMode.Two_Player;
                break;
        }
        SetupGameMode();
    }

    private void SetupGameMode()
    {
        switch (CurrentDataGameMode)
        {
            case DataGameMode.Single_Player:
                currentModeText.text = "Current Mode : Single Player";
                break;
            case DataGameMode.Two_Player:
                currentModeText.text = "Current Mode : Two Player";
                break;
        }

        currentDataMode = dataInGame.FindDataMode(CurrentDataGameMode);

        highestScoresText.text = "Highest Scores : "+currentDataMode.bestScore;
    }

    private void OnGameInitialized()
    {
        SetupGameMode();
    }

    

    private void LoadData()
    {
        string data = PlayerPrefs.GetString("DataInGame", string.Empty);

        if (string.IsNullOrEmpty(data))
        {
            dataInGame.Load();
        }
        else
        {
            dataInGame.jsonGameMode = data;
            dataInGame.Load();

        }
    }
    
    private void PlayGame()
    {
        currentDataMode = dataInGame.FindDataMode(CurrentDataGameMode);
    }

    public void SaveDataMode()
    {
        dataInGame.SaveDataMode(currentDataMode);

        string data = JsonUtility.ToJson(dataInGame);
        PlayerPrefs.SetString("DataInGame", data);
    }

}

public class TestClass
{
    public string name;
    public int score;

    public TestClass(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

}
