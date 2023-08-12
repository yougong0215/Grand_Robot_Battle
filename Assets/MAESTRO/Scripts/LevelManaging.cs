using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManaging : MonoBehaviour
{
    TextMeshProUGUI _levelText;
    Slider _slider;
    [SerializeField] private int[] levelUpEXPAmount = new int[4];

    private void Awake()
    {
        _levelText = transform.Find("Level").GetComponent<TextMeshProUGUI>();
        _slider = transform.Find("ExpressBar").GetComponent<Slider>();
    }

    public void Setting(int lv, int exp)
    {
        _levelText.text = lv.ToString();
        _slider.value = exp / levelUpEXPAmount[lv];
    }
}
