using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleEndUI : MonoBehaviour
{
    UIDocument _doc;
    VisualElement _root;

    StoryUISO _so;
    Button exitbtn;
    Button againbtn;
    void OnEnable()
    {
        _doc = GetComponent<UIDocument>();
        _root = _doc.rootVisualElement;

        exitbtn = _root.Q<Button>("ExitBtn");
        againbtn = _root.Q<Button>("AgainBtn");

        _so = StoryLoadResource.Instance.Loading();

        if(_so != null)
        {
            exitbtn.clicked += () => StoryLoadResource.Instance.Save(null);
            againbtn.clicked += () => StoryLoadResource.Instance.Save(null);

        }

        exitbtn.clicked += () => LoadManager.LoadScene(SceneEnum.Menu);
        againbtn.clicked += () => LoadManager.ReturnBack();

        
    }
}
