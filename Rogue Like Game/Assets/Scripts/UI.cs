using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public GameObject[] hearts;
    public TextMeshProUGUI coinText;
    public GameObject keyIcon;
    public TextMeshProUGUI levelText;
    public RawImage map;

    public static UI instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHealth(int health)
    {
        for (int x = 0; x < hearts.Length; x++)
        {
            hearts[x].SetActive(x < health);
        }
    }

    public void UpdateCoinText(int coins)
    {
        coinText.text = coins.ToString();
    }

    public void ToggleKeyIcon(bool toggle)
    {
        keyIcon.SetActive(toggle);
    }

    public void UpdateLevelIndex(int level)
    {
        levelText.text = "Level: " + level;
    }
}
