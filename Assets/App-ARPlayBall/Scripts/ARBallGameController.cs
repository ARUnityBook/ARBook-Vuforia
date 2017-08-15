using UnityEngine;
using UnityEngine.UI;

public class ARBallGameController : MonoBehaviour {
    public int hitPoints = 10;
    public int missPoints = -2;

    public BallGame[] BallGames;
    public Text scoreDisplay;

    public Text highScoreDisplay;

    private GameDataManager gameDataManager;
    private int playerScore;
    private int currentGameIndex;
    private bool isInitialized;

    void Start() {
        gameDataManager = FindObjectOfType<GameDataManager>();
        highScoreDisplay.text = gameDataManager.GetHighestPlayerScore().ToString();

        for (int i = 0; i < BallGames.Length; i++) {
            BallGames[i].OnGoalWon.AddListener(WonGoal);
            BallGames[i].OnGoalLost.AddListener(LostGoal);
            BallGames[i].Deactivate();
        }

        isInitialized = true;
    }

    public void StartGame() {
        SetRandomGame();
    }

    void WonGoal() {
        ChangeScore(hitPoints);
        SetRandomGame();
    }
    private void OnEnable() {
        if (isInitialized)
            SetRandomGame();
    }

    void LostGoal() {
        ChangeScore(missPoints);
    }

    void ChangeScore(int points) {
        playerScore = playerScore + points;
        if (playerScore < 0) playerScore = 0;
        scoreDisplay.text = playerScore.ToString();

        gameDataManager.SubmitNewPlayerScore(playerScore);
        highScoreDisplay.text = gameDataManager.GetHighestPlayerScore().ToString();
    }

    private void SetRandomGame() {
        //clears the last game
        BallGames[currentGameIndex].Deactivate();
        //sets a new game
        currentGameIndex = Random.Range(0, BallGames.Length);
        BallGames[currentGameIndex].Activate();
    }

}
