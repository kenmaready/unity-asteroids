using UnityEngine;
using TMPro;

public class UICanvas : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _scoreDisplay;
    private string scoreFormat = "0000000000";

    void Start()
    {
        _scoreDisplay.text = 0.ToString(scoreFormat);
    }

    public void UpdateScore(int score) {
        _scoreDisplay.text = score.ToString(scoreFormat);
    }
}
