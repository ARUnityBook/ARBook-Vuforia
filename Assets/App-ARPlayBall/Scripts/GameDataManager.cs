using UnityEngine;

public class GameDataManager : MonoBehaviour {

    private PlayerProgress playerProgress;

    public void Awake() {
        LoadPlayerProgress();
    }

    public void SubmitNewPlayerScore(int newScore) {
        if (newScore > playerProgress.highScore) {
            playerProgress.highScore = newScore;
            SavePlayerProgress();
        }
    }

    public int GetHighestPlayerScore() {
        return playerProgress.highScore;
    }

    private void LoadPlayerProgress() {
        playerProgress = new PlayerProgress();

        if (PlayerPrefs.HasKey("highScore")) {
            playerProgress.highScore = PlayerPrefs.GetInt("highScore");
        }
    }

    private void SavePlayerProgress() {
        PlayerPrefs.SetInt("highScore", playerProgress.highScore);
    }
}
