using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Player player;
    private int startingLives = 3;

    private int livesRemaining;
    public int LivesRemaining { get { return livesRemaining; } }
    private int score;
    [SerializeField] UICanvas uiCanvas;

    private void Awake() {
        player = FindObjectOfType<Player>();
        if (player == null) {
            Debug.LogError("No Player Object found by GameManager.");
        }
        this.livesRemaining = this.startingLives;
    }

    private void Start() {
    }

    public void IncreaseScore(int val) {
        score += val;
        uiCanvas.UpdateScore(score);
    }

    public void OnPlayerDeath() {
        this.livesRemaining--;
        uiCanvas.UpdateLivesRemainingDisplay(this.livesRemaining);
        this.player.gameObject.SetActive(false);
        if (this.livesRemaining <= 0) {
            GameOver();
        } else {
            Invoke(nameof(RespawnPlayer), 2.0f);
        }
    }

    public void Play() {
        livesRemaining = startingLives;
        score = 0;
        uiCanvas.StartPlay();
        RespawnPlayer();
    }

    private void GameOver() {
        uiCanvas.OnGameOver();
    }

    private void RespawnPlayer() {
        this.player.gameObject.layer = LayerMask.NameToLayer("SafeRespawn");
        this.player.gameObject.SetActive(true);
        this.player.Respawn();
    }

}
