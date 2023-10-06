using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICanvas : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _mainTextDisplay;
    [SerializeField] private TextMeshProUGUI _scoreDisplay;
    [SerializeField] private GameObject _livesRemainingDisplay;
    [SerializeField] private Image[] _livesRemainingSprites;
    [SerializeField] private Button _playButton;

    private string _mainText = "Asteroids";
    private string scoreFormat = "0000000000";
    private GameManager gm;

    private void Awake() {
        gm = FindObjectOfType<GameManager>();
        _livesRemainingSprites = GetComponentsInChildren<Image>();
    }

    void Start()
    {
        _mainTextDisplay.text = _mainText;
        _mainTextDisplay.gameObject.SetActive(true);
        _playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play";
        _playButton.gameObject.SetActive(true);
    }

    public void StartPlay() {
        UpdateDisplaysForNewGame();
    }

    private void UpdateDisplaysForNewGame() {
        _mainTextDisplay.gameObject.SetActive(false);
        _playButton.gameObject.SetActive(false);
        _scoreDisplay.text = 0.ToString(scoreFormat);
        _playButton.gameObject.SetActive(false);
        UpdateLivesRemainingDisplay(gm.LivesRemaining);
    }

    public void OnGameOver() {
        _mainText = "Game Over";
        _mainTextDisplay.text = _mainText;
        _mainTextDisplay.gameObject.SetActive(true);
        _playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play Again";
        _playButton.gameObject.SetActive(true);
    }

    public void UpdateScore(int score) {
        _scoreDisplay.text = score.ToString(scoreFormat);
    }

    public void UpdateLivesRemainingDisplay(int livesRemaining) {
        for (int i = 0; i < _livesRemainingSprites.Length; i++) {
            if (i >= livesRemaining) {
                _livesRemainingSprites[i].enabled = false;
            } else {
                _livesRemainingSprites[i].enabled = true;
            }
        }
    }

    public void OnPlayButtonClick() {
        gm.Play();
    }
}
