using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using UnityEngine;

public class DataHandler : MonoBehaviour
{

    public static DataHandler Instance;

    public string Name = "Player";
    public int BestScore = 0;
    
    public List<SaveData> ScoreList = new List<SaveData>(10);

    string path;

    private void Awake()
    {
        path = Application.persistentDataPath + "/savefiletable.json";
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        if(gameObject.activeInHierarchy)
            DontDestroyOnLoad(gameObject);
    }

    [Serializable]
    public class SaveData
    {
        public int HighScore;
        public string PlayerName;
    }

    public void Save()
    {
        SaveScoreToList();
        SaveScoreToFile();
    }

    /// <summary>
    /// This function saves data into a list and sorting it by descending score
    /// </summary>
    public void SaveScoreToList()
    {
        // creating a new player data container instance
        SaveData data = new SaveData(); 

        // saving player's data
        data.HighScore = BestScore;
        data.PlayerName = Name;

        // adding player's data into a list if it's not overfilled
        if (ScoreList.Count < 10)
        {
            ScoreList.Add(data);
        }
        // but if it is, and the new score exceeds the last one:
        else if (ScoreList.Count == 10 && BestScore > ScoreList.Last().HighScore)
        {
            // we look for a smallest score in the list
            int index = 0;
            int[] scores = new int[10];
            foreach (SaveData dataset in ScoreList)
            {
                scores[index] = dataset.HighScore;
                index += 1;
            }
            
            // there it is!
            int min_index = Array.IndexOf(scores, scores.Min());

            // removing it and adding a new dataset to the list
            ScoreList.RemoveAt(min_index);
            ScoreList.Add(data);
            
            
        }

        // sorting the list
        ScoreList = ScoreList.OrderByDescending(x => x.HighScore).ToList();


        /* For those who might review this piece of art (you know what i mean..)
         * I was struggling between two options: sort the list, remove the last(minimum)
         * players score, adding a new entry to the list and then sorting it again
         * or to do what I did. I didn't found any info abt a big O of OrderBy()
         * sorting method, so I'm curious if twice-sorting actually faster then iterating
         * especially on a large data sets. Please share your opinion on that
         * */
    }

    /// <summary>
    /// This function saves data into a json file from the list entry by entry
    /// </summary>
    public void SaveScoreToFile()
    {
        string json = "";
        foreach (SaveData dataset in ScoreList)
        {
            json += JsonUtility.ToJson(dataset);
            json += '\n';
        }
        Debug.Log(json);
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// This function reads data from a save file back into the list and
    /// gets the best players name and score into the separate variables
    /// </summary>
    public void Load()
    {
        if (File.Exists(path))
        {
            string[] json = File.ReadAllLines(path);
            
            ScoreList.Clear();
            
            foreach(string line in json)
            {
                SaveData data = JsonUtility.FromJson<SaveData>(line);
                
                if (line == json[0])
                {
                    Name = data.PlayerName;
                    BestScore = data.HighScore;
                }

                ScoreList.Add(data);
            }
        }
    }

    public void ResetScore()
    {
        File.Delete(path);
        ScoreList.Clear();
        Name = "Player";
        BestScore = 0;
    }

}
