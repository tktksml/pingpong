using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;

    [SerializeField] private Ball ball = null;

    [Header("UI content")]
    [SerializeField] private TextMeshProUGUI highScoreText = null;
    [SerializeField] private TextMeshProUGUI myScoreText = null;
    [SerializeField] private TextMeshProUGUI enemyScoreText = null;


    private int myScore = 0, enemyScore = 0;


    public void Awake()
    {
        //TODO remove singl
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }


    public static void LoseZoneTriggered(bool isMasterWin)
    {
        Instance.OnLoseZoneTriggered(isMasterWin);
    }
    public void OnLoseZoneTriggered(bool isMasterWin)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            myScore += isMasterWin ? 1 : 0;
            enemyScore += !isMasterWin ? 1 : 0;
        }
        else
        {
            myScore += !isMasterWin ? 1 : 0;
            enemyScore += isMasterWin ? 1 : 0;
        }

        //Beating highscore
        if (myScore > SaveController.HighScore)
        {
            SaveController.HighScore = myScore;
            UpdateHighScoreText();
        }

        UpdateGameScoreText();
        ResetBall();
    }


    public void OnEnable()
    {
        PrepareGame();
    }


    /// <summary>
    /// Prepare content to start (reset text + score)
    /// </summary>
    public void PrepareGame()
    {
        myScore = 0;
        enemyScore = 0;
        UpdateHighScoreText();
        UpdateGameScoreText();
        ResetBall();
    }


    /// <summary>
    /// Update current game score (text updating)
    /// </summary>
    public void UpdateGameScoreText()
    {
        myScoreText.text = "You: " + myScore.ToString();
        enemyScoreText.text = "Enemy: " + enemyScore.ToString();
    }


    /// <summary>
    /// Update user high score (text updating)
    /// </summary>
    public void UpdateHighScoreText()
    {
        highScoreText.text = "Highscore: " + SaveController.HighScore.ToString();
    }


    /// <summary>
    /// Move ball to center with new settings
    /// </summary>
    public void ResetBall()
    {
        bool isMaster = PhotonNetwork.IsMasterClient;
        if (!isMaster)
        {
            return;
        }
        ball.PhotonView.RPC("Init", RpcTarget.All, SaveController.BallSettings.Value);
    }
}
