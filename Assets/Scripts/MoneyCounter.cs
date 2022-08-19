using System.Collections;
using UnityEngine;
using TMPro;

public class MoneyCounter : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        GameManager.Instance.OnCoinCollected += SetMoneyCount;

        text = GetComponent<TextMeshProUGUI>();

        SetMoneyCount(PlayerPrefs.GetInt("Coin"));
    }
    public void SetMoneyCount(int value)
    {
        text.text = PlayerPrefs.GetInt("Coin").ToString();
    }
}
