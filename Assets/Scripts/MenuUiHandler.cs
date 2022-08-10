using TMPro;
using UnityEngine;

public class MenuUiHandler : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TMP_InputField PlayernameInputField;

    private void Start()
    {
        ScoreText.text = "Best Score: " + GameManager.Instance.BestScore;
    }

    public void StartNew()
    {
        GameManager.Instance.Name = string.IsNullOrEmpty(PlayernameInputField.text) ? "Playername" : PlayernameInputField.text;

        GameManager.Instance.StartNew();
    }

    public void Exit()
    {
        GameManager.Instance.Exit();
    }
}
