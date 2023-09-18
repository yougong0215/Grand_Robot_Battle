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
    private Button _skipBtn;
    Label _text;
    Label _name;
    private VisualElement _single;
    private VisualElement _double;
    private VisualElement _bg;

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
        _single = _root.Q<VisualElement>("Single");
        _double = _root.Q<VisualElement>("Double");
        _skipBtn = _root.Q<Button>("SkipBtn");
        
        _skipBtn.clicked += () => SceneManager.LoadScene((int)StoryLoadResource.Instance.NextScene());
        _panelInput.RegisterCallback<ClickEvent>((evt) =>
        {
            Input();
        });

        _bg = _root.Q<VisualElement>("Background");
        //_panelInput.clicked += () => Input();
        Input();
        
    }

    void Input()
    {
        if(_touch ==true)
            index += 1;
        if (index >= SO.Script.Count)
        {
            SceneManager.LoadScene((int)StoryLoadResource.Instance.NextScene());
            return;
        }
        StoryClass sc = SO.Script[index];
        if (sc.IsSay == false)
        {

            _name.text = $"{sc.Ch.names} | {sc.Ch.Exname}";
        }
        else 
        {
            _name.text = $"{sc.TwoCh.names} | {sc.TwoCh.Exname}";
        }
        StyleBackground one = null;
        StyleBackground two = null;
        if (sc.Position == false)
        {
            if(sc.Ch != null)
                one = sc.Ch.ReturnSprite(sc._faceOne) != null ? new StyleBackground(sc.Ch.ReturnSprite(sc._faceOne)) : null;
            if(sc.TwoCh != null)
                two = sc.TwoCh.ReturnSprite(sc._faceTwo) != null ? new StyleBackground(sc.TwoCh.ReturnSprite(sc._faceTwo)) : null;
        }
        else
        {
            if(sc.TwoCh != null)
                one =  sc.TwoCh.ReturnSprite(sc._faceTwo) != null ? new StyleBackground(sc.TwoCh.ReturnSprite(sc._faceTwo)) : null;
            if(sc.Ch != null)
                two = sc.Ch.ReturnSprite(sc._faceOne) != null ? new StyleBackground(sc.Ch.ReturnSprite(sc._faceOne)) : null;
        }
        
       

        _single.style.backgroundImage = one != null ? one : null;
        _double.style.backgroundImage = two != null ? two : null;

        _bg.style.backgroundImage = sc.BG != null ? new StyleBackground(sc.BG) : new StyleBackground(SO.DefaultBackGround);

        _text.text = "";

        if(_touch==false)
        {
            _touch = true;
            if (_co != null)
                StopCoroutine(_co);
            _text.text = sc.Script;
            return;
        }
        else
        {
            _touch = false;

            if (index < SO.Script.Count)
            {
                if (_co != null)
                    StopCoroutine(_co);
                _co = StartCoroutine(Scripting(sc));
            }

        }
    }
    


    IEnumerator Scripting(StoryClass sc)
    {
        _touch = false;
        string ht = "";
        
        for (int i =0;i < sc.Script.Length; i++)
        {

            yield return new WaitForSeconds(0.05f);
            ht += sc.Script[i];
            _text.text = ht;
        }
        _touch = true;
    }
}
