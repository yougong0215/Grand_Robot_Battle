using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IngredientValue
{
    public GameObject igObj;
    public Ingredient ig;
    public string name;
    public int count = 0;
    public Sprite sprite;
}

public class IngredientManager : MonoBehaviour
{
    public List<IngredientValue> _haveIngredientList = new List<IngredientValue>(3);
    [SerializeField] private GameObject _content;
    private void Start()
    {
        for(int i = 0; i < _haveIngredientList.Count; i++)
        {
            _haveIngredientList[i].ig.SettingInfredient(_haveIngredientList[i]);
        }
    }

    private void OnEnable()
    {
        AddIgSource();
    }

    private void OnDisable()
    {
        Clear();
    }

    public void AddIgSource()
    {
        for(int i = 0; i < _haveIngredientList.Count; i++)
        {
            if(_haveIngredientList[i].count != 0)
            {
                Instantiate(_haveIngredientList[i].igObj, _content.transform);
            }
        }
    }

    public void Clear()
    {
        GameObject deletfrom;
        for(int i = 0; i < _content.transform.childCount; i++)
        {
            deletfrom = _content.transform.GetChild(i).gameObject;
            Destroy(deletfrom);
        }
    }

    public void SetIngredientValue(int idx, int value)
    {
        _haveIngredientList[idx].count += value;
        _haveIngredientList[idx].ig.SettingCountValue(value);
    }

}
