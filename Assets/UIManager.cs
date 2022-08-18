using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startText;

    [SerializeField] private GameObject winPanel, losePanel;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(GameManager.Instance.State == GameState.Initial)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.State = GameState.Playing;

                startText.gameObject.SetActive(false);
            }
        }
    }

    public void WinSequence()
    {
        winPanel.SetActive(true);
    }
    public void LoseSequence()
    {
        losePanel.SetActive(true);
    }

    public void NextLevelButton()
    {

    }
    public void RetryButton()
    {

    }
}
