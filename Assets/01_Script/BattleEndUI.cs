using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleEndUI : MonoBehaviour
{
    UIDocument _doc;
    VisualElement _root;

    Button exitbtn;
    Button againbtn;
    void OnEnable()
    {
        _doc = GetComponent<UIDocument>();
        _root = _doc.rootVisualElement;

        exitbtn = _root.Q<Button>("ExitBtn");
        againbtn = _root.Q<Button>("AgainBtn");


        exitbtn.clicked += () => LoadManager.LoadScene(SceneEnum.Menu);
        againbtn.clicked += () => LoadManager.ReturnBack();
    }
}
