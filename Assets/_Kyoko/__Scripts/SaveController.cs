using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveController
{
    public static int HighScore { get { return PlayerPrefs.GetInt("SC_HIGHSCORE", 0); } set { PlayerPrefs.SetInt("SC_HIGHSCORE", value); } }

    static private BallSO ballSO = null;
    static public BallSO BallSettings
    {
        get
        {
            if (ballSO == null)
            {
                ballSO = Resources.Load<BallSO>("BallSettings");
                if (ballSO == null)
                {
                    throw new System.Exception("Ball settings is missing!");
                }
                Load();
            }
            return ballSO;
        }
    }


    public static void Save()
    {
        string text = JsonUtility.ToJson(ballSO.Value);
        File.WriteAllText(Application.persistentDataPath + "/BallSettings.json", text);
    }
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/BallSettings.json"))
        {
            string file = File.ReadAllText(Application.persistentDataPath + "/BallSettings.json");
            if (!string.IsNullOrEmpty(file))
            {
                ballSO.Value = JsonUtility.FromJson<BallSettings>(file);
                Debug.LogError("Succ");
                Debug.LogError(ballSO.Value.scale);
            }
        }
    }
}