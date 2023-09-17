using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StoryScript : MonoBehaviour
{
    [SerializeField] StoryScriptSO SO;
    [SerializeField] public SceneEnum Next;
    UIDocument _doc;
    VisualElement _root;

    int index = 0;
    bool _touch = false;

    VisualElement _panelInput;
    Label _text;
    Label _name;

    Coroutine _co;
    
    void OnEnable()
    {
        SO = StoryLoadResource.Instance.ReturnStorySO();
        StoryLoadResource.Instance.RemoveStory();

        _doc = GetComponent<UIDocument>();
        _root = _doc.rootVisualElement;
        _panelInput = _root.Q<VisualElement>("TextPanel");

        _name = _root.Q<Label>("Name");
        _text = _panelInput.Q<Label>("Text");

        _panelInput.RegisterCallback<ClickEvent>((evt) =>
        {
            Input();
        });
        //_panelInput.clicked += () => Input();
    }

    void Input()
    {
        if (SO.Script[index].IsSay == false)
        {

            _name.text = $"{SO.Script[index].Ch.names} | {SO.Script[index].Ch.Exname}";
        }
        else 
        {
            _name.text = $"{SO.Script[index].TwoCh.names} | {SO.Script[index].TwoCh.Exname}";
        }

        _text.text = "";

        if(_touch==false)
        {
            _touch = true;
            if (_co != null)
                StopCoroutine(_co);
            _text.text = SO.Script[index].Script;
            return;
        }
        else
        {
            _touch = false;
            index += 1;
            if (index < SO.Script.Count)
            {

                _co = StartCoroutine(Scripting());
            }
            else
            {
                SceneManager.LoadScene((int)StoryLoadResource.Instance.NextScene());
            }
        }
    }
    


    IEnumerator Scripting()
    {
        _touch = false;
        string ht = "";
        for (int i =0;i < SO.Script[index].Script.Length; i++)
        {
            ht += SO.Script[index].Script[i];
            _text.text = ht;
            yield return new WaitForSeconds(0.05f);
        }
        _touch = true;
    }
}
