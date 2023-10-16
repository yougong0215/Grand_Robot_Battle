using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FriendUI : MonoBehaviour
{
    public VisualTreeAsset _Profile;
    private const string std = "닉네임 검색";
    private string oldInput;
    private VisualElement _root;
    private UIDocument _ui;
    private TextField _input;
    private ScrollView _friend;
    private ScrollView _serch;
    private Button _exit;

    private bool OnOffPanel =false;

    private bool reset = true;
    void OnEnable()
    {
        _ui = GetComponent<UIDocument>();
        _root = _ui.rootVisualElement.Q<VisualElement>("FriendRoot");
        _input = _root.Q<TextField>("Input");
        _friend = _root.Q<ScrollView>("FriendList");
        _serch = _root.Q<ScrollView>("SerchList");
        _exit = _root.Q<Button>("ExitBtn");
        _exit.clicked += () => OpenFriendList(false);
    }
    
    public void OpenFriendList(bool b)
    {
        if (b)
        {
            _root.RemoveFromClassList("off");
        }
        else
        {
            _root.AddToClassList("off");
        }

        OnOffPanel = b;
    }

    void AddProfile(string names)
    {
        Debug.LogWarning("Domi : 추가시 제거1");
        // if 없으면 return;
        // 서버 추가
        VisualElement _pl = _Profile.Instantiate();
        _pl.style.backgroundImage = new StyleBackground(); 
        _pl.Q<Label>("NameSpace").text = "Name";
        _pl.Q<Button>("Follow").clicked += FollowInput;
        _pl.Q<Button>("Battle").clicked += BattleInput;
    }

    void ResetFriend()
    {
        // 친구 추가시 혹은 최초 실행시 넣기 
        Debug.LogWarning("Domi : 추가시 제거2");
        _friend.Clear();

        for (int i = 0; i < 0; i++)
        {
            VisualElement _pl = _Profile.Instantiate();
            _pl.style.backgroundImage = new StyleBackground(); // 넣기
            _pl.Q<Label>("NameSpace").text = "Name";
            _pl.Q<Button>("Follow").clicked += FollowInput;
            _pl.Q<Button>("Battle").clicked += BattleInput;
        }
    }
    
    void FollowInput()
    {
        Debug.LogWarning("Domi : 추가시 제거3");
        // 팔로우
    }

    void BattleInput()
    {
        Debug.LogWarning("Domi : 추가시 제거4");
        // 전투 신청
    }

    private void Update()
    {
        if (OnOffPanel == false)
            return;
        
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OpenFriendList(false);
        }
        if (_input.value == string.Empty)
        {
            _input.value = std;
            oldInput = std;
        }

        if (_input.value == std)
        {
            _friend.style.display = DisplayStyle.Flex;
            _serch.style.display = DisplayStyle.None;
            if (reset == true)
            {
                reset = false;
                
                // 기존친구 넣기
                ResetFriend();
                

            }
            return;
        }
        
        if(_input.value != oldInput)
        {
            oldInput = _input.value;
            _friend.style.display = DisplayStyle.None;
            _serch.style.display = DisplayStyle.Flex;
            AddProfile(_input.value);
            reset = true;
        }
    }


}
