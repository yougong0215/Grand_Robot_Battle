using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private int idx;
    int price;
    int exp;
    [SerializeField] Image _checkImage;
    Image _ingredientImage;
    TextMeshProUGUI _nameTxt;
    TextMeshProUGUI _countTxt;
    LevelManaging _lm;

    GameObject _counting;

    bool isChecking;

    public void SettingInfredient(IngredientValue iv)
    {
        _checkImage = (Image)transform.Find("CheckImage").GetComponent("Image");
        _ingredientImage = (Image)transform.Find("IngredientImage").GetComponent("Image");
        _nameTxt = transform.Find("NameTxt").GetComponent<TextMeshProUGUI>();
        _countTxt = transform.Find("StatPanel/CountTxt").GetComponent<TextMeshProUGUI>();
        _lm = (LevelManaging)GameObject.Find("LevelManaging").GetComponent("LevelManaging");
        _counting = transform.Find("CheckImage/CountImage").gameObject;
        _checkImage.enabled = false;

        price = iv.price;
        _ingredientImage.sprite = iv.sprite;
        _nameTxt.text = iv.name;
    }

    public void SettingCountValue(int value, int ex)
    {
        _countTxt.text = value.ToString();
        exp = ex;
    }

    public void ClickThisObject()
    {
        _checkImage = (Image)transform.Find("CheckImage").GetComponent("Image");
        _checkImage.enabled = !isChecking;
        _counting.SetActive(!isChecking);

        if(isChecking)
        {
            _lm.BeforeUpgradeVirtual(-price, -exp);
        }
        else
        {
            _lm.BeforeUpgradeVirtual(price, exp);
        }

        isChecking = !isChecking;
    }
}
