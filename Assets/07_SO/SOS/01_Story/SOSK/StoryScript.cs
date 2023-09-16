using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StoryScript : MonoBehaviour
{
    [SerializeField] StoryScriptSO SO;
    [SerializeField] public SceneEnum Next;
    UIDocument _doc;

    int index = 0;
    bool _touch = false;

    Button _panelInput;

    Coroutine _co;
    
    void OnEnable()
    {
        _doc = GetComponent<UIDocument>();



        _panelInput.clicked += () => Input();
    }

    void Input()
    {
        if(_touch==false)
        {
            _touch = true;
            if (_co != null)
                StopCoroutine(_co);
            _panelInput.text = SO.Script[index].Script;
            return;
        }
        else
        {
            _touch = false;
            index += 1;
            _co = StartCoroutine(Scripting());
        }
    }
    


    IEnumerator Scripting()
    {
        _touch = false;
        string ht = "";
        for (int i =0;i < SO.Script[index].Script.Length; i++)
        {
            ht += SO.Script[index].Script[i];
            yield return new WaitForSeconds(0.05f);
        }
        _touch = true;
    }
}
