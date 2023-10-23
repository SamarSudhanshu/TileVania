using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour {

    [SerializeField] private int playerLives = 3; 
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoresText;
    [SerializeField] private int score = 0;
    private void Awake() {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        livesText.text = playerLives.ToString();
        scoresText.text = score.ToString();
    }
    public void ProcessPlayerDeath() {
        if (playerLives > 1)
            TakeLive();
        else
            ResetGameSession();
    }

    private void ResetGameSession() {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLive() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        --playerLives;
        livesText.text = playerLives.ToString();
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void AddToScore(int pointsToAdd) {
        score += pointsToAdd;
        scoresText.text = score.ToString();
    }
}
