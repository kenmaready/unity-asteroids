using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICanvas : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _scoreDisplay;
    [SerializeField] private GameObject _livesRemainingDisplay;
    [SerializeField] private Image[] _livesRemainingSprites;
    private string scoreFormat = "0000000000";
    private GameManager gm;

    private void Awake() {
        gm = FindObjectOfType<GameManager>();
        _livesRemainingSprites = GetComponentsInChildren<Image>();
    }

    void Start()
    {
        _scoreDisplay.text = 0.ToString(scoreFormat);
        UpdateLivesRemainingDisplay(gm.LivesRemaining);
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
}
