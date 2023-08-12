using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private int idx;
    int count;
    [SerializeField] Image _checkImage;
    Image _ingredientImage;
    TextMeshProUGUI _nameTxt;
    TextMeshProUGUI _countTxt;

    bool isChecking;

    public void SettingInfredient(IngredientValue iv)
    {
        _checkImage = (Image)transform.Find("CheckImage").GetComponent("Image");
        _ingredientImage = (Image)transform.Find("IngredientImage").GetComponent("Image");
        _nameTxt = transform.Find("NameTxt").GetComponent<TextMeshProUGUI>();
        _countTxt = transform.Find("StatPanel/CountTxt").GetComponent<TextMeshProUGUI>();
        Debug.Log(_checkImage);
        _checkImage.enabled = false;

        _ingredientImage.sprite = iv.sprite;
        _nameTxt.text = iv.name;
    }

    public void SettingCountValue(int value)
    {
        _countTxt.text = value.ToString();
    }

    public void ClickThisObject()
    {
        _checkImage = (Image)transform.Find("CheckImage").GetComponent("Image");
        _checkImage.enabled = !isChecking;
        isChecking = !isChecking;
    }
}
