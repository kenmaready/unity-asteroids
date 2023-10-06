using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Player player;
    private int startingLives = 3;

    private int currentLives;
    private int score;
    [SerializeField] UICanvas uiCanvas;

    private void Awake() {
        player = FindObjectOfType<Player>();
        if (player == null) {
            Debug.LogError("No Player Object found by GameManager.");
        }
    }

    private void Start() {
        this.currentLives = this.startingLives;
    }

    public void IncreaseScore(int val) {
        score += val;
        Debug.Log("Score is now: " + score);
        uiCanvas.UpdateScore(score);
    }

    public void OnPlayerDeath() {
        this.currentLives--;
        this.player.gameObject.SetActive(false);
        if (this.currentLives <= 0) {
            GameOver();
        } else {
            Invoke(nameof(RespawnPlayer), 2.0f);
        }
    }

    private void GameOver() {

    }

    private void RespawnPlayer() {
        this.player.gameObject.layer = LayerMask.NameToLayer("SafeRespawn");
        this.player.gameObject.SetActive(true);
        this.player.Respawn();
    }

}
