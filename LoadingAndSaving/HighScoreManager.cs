using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class HighScoreManager : MonoBehaviour
{
    public List<int> HighScores = new List<int>();
    public int[] defaultHighScores = new int[10];

    //Run first, initalization of this component
    void Awake()
    {
        LoadHighScores();
    }
    //Setup of things that depend on other components
    void Start()
    {
        
    }

    private void LoadHighScores()
    {
        Debug.Log("Highscores:");
        for (int i = 0; i < 10; i++)
        {
            HighScores.Add(PlayerPrefs.GetInt("HighScore" + i, defaultHighScores[i]));
        }
    }

    public bool AddScore(int score)
    {
        HighScores.Add(score);
        HighScores.Sort();
        HighScores.Reverse();

        if (HighScores[HighScores.Count-1] == score) //Still last score, not new highscore
        {
            HighScores.RemoveAt(HighScores.Count-1); //Remove last
            return false;
        }
        else
        {
            HighScores.RemoveAt(HighScores.Count-1); //Remove last
            SaveHighScores();
            return true;
        }
    }

    private void SaveHighScores()
    {
        Debug.Log("Saved Highscores:");
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, HighScores[i]);
        }

        PlayerPrefs.Save();
    }
}
