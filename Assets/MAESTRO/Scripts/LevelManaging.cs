using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManaging : MonoBehaviour
{
    int exp = 0;
    int amount = 0;
    int level;
    TextMeshProUGUI _levelText;
    Slider _slider;
    [SerializeField] private int[] levelUpEXPAmount = new int[4];
    TextMeshProUGUI _amountText;

    private void Awake()
    {
        _levelText = transform.Find("Level").GetComponent<TextMeshProUGUI>();
        _slider = transform.Find("ExpressBar").GetComponent<Slider>();
        _amountText =
        GameObject.Find("ScrollCanvas/Scroll View/PowerUpPanel/UpgradeBtn/amountTxt").GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        level = 0;
        amount = 0;
    }

    public void Setting(int lv, int exp)
    {
        level += lv;
        _levelText.text = level.ToString();
        _slider.value = exp / levelUpEXPAmount[lv];
    }

    public void BeforeUpgradeVirtual(int price, int ex)
    {
        amount += price;
        exp += ex;
    }

}
