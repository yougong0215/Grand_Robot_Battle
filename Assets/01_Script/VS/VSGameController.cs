using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using DG.Tweening;

public class VSGameController : MonoBehaviour
{
    [SerializeField] public List<VSPlayer> _players;
    [SerializeField] public List<VSPlayer> TeamRedPlayer;
    [SerializeField] public List<VSPlayer> TeamBluePlayer;

    [Header("POS")]
    [SerializeField] List<Transform> RedTeamPos;
    [SerializeField] List<Transform> BlueTeamPos;
    [SerializeField] public GameObject TextPanel;
    [SerializeField] public TextMeshProUGUI TMPPanel;

    static VSGameController _ins;

    public static VSGameController Instance => _ins;

    int _turnCount = 0;

    public int CurrentTurn => _turnCount;

    public bool MultiMode = false;

    void Awake()
    {
        _ins = this;

    }

    IEnumerator Start()
    {
        GameObject[] t = GameObject.FindGameObjectsWithTag("Unit");
        TextPanel = GameObject.Find("TextPanel");
        TMPPanel = TextPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextPanel.SetActive(false);

        //Debug.LogWarning(t.Count());

        for(int i=0; i < t.Length; i++)
        {
            t[i].transform.parent = transform;
            //t[i].GetComponent<RobotSettingAndSOList>().VSSet();

            yield return null;
            _players.Add(transform.GetChild(i).GetComponent<VSPlayer>());
            _players[i].transform.localScale = new Vector3(1, 1, 1);
        }
        int red = 0, blue = 0;

        for (int i = 0; i < _players.Count; i++)
        {
            yield return null;
            
            _players[i].GameNumber = i;

            

            if(_players[i].team == Team.Red)
            {
                _players[i].transform.parent = RedTeamPos[red];
                red++;
            }
            else
            {
                _players[i].transform.parent = BlueTeamPos[blue];
                blue++;
            }

            _players[i].transform.localPosition = Vector3.zero;

            _players[i].transform.localEulerAngles = Vector3.zero;
            _players[i].BattleInit();
        }

        StartCoroutine(Sycle());
    }
    
    public int TeamSelected(Team t)
    {
        int x;
        if (t == Team.Red)
        {
            x = UnityEngine.Random.Range(0, TeamBluePlayer.Count);

            return TeamBluePlayer[x].GameNumber;
        }
        else
        {

            x = UnityEngine.Random.Range(0, TeamRedPlayer.Count);
            return TeamRedPlayer[x].GameNumber;
        }
    }


    IEnumerator Sycle()
    {
        TeamBluePlayer.Clear();
        TeamRedPlayer.Clear();



        for (int i = 0; i < _players.Count; i++)
        {
            if(_players[i].team == Team.Red)
            {
                TeamRedPlayer.Add(_players[i]);
            }
            else
            {
                TeamBluePlayer.Add(_players[i]);
            }
        }

        for (int i = 0; i < _players.Count; i++)
        {

            _players[i].Turn();
            Debug.Log($"{_players[i].name} : 스킬 고르른중");
            yield return new WaitUntil(() => _players[i].GetSkillNum() != -1 && _players[i].GetEnemyNum() != -1);
            Debug.Log($"{_players[i].name} : {_players[i].GetSkillNum()}, {_players[i].GetEnemyNum()}");
            yield return new WaitForSeconds(2.5f);

        }

        PlayerSpeedSort();



        int selected = 0;



        for (int i = 0; i < _players.Count; i++)
        {
            for (int j = 0; j < _players.Count; j++)
            {
                if (_players[j].GameNumber == _players[i].GetEnemyNum())
                {
                    selected = j;
                    Debug.Log($"{_players[i].name} 턴 {_players[selected]}공격"); 

                    yield return StartCoroutine(_players[i].DoSkill(_players[selected]));
                  

                    TextPanel.SetActive(false);
                    break;
                }
            }
        }


        int Red = 0, Blue= 0;


        for(int i =0; i < _players.Count; i++)
        {
            Debug.Log($"{_players[i].name} : {_players[i].CurrentStat.HP}");
            _players[i].SetEnemyNum(-1);
            _players[i].SetSkillNum(-1);

        }

        for (int i = 0; i < _players.Count; i++)
            _players[i].OnDead(ref Red, ref Blue);


        if (Red != 0 && Blue != 0)
        {
            StartCoroutine(Sycle());
        }
        else
        {
            GameEnd();
        }

    }

    void GameEnd()
    {
        SceneManager.LoadScene("MakeRobotScene");
    }


    void PlayerSpeedSort()
    {
        int n = _players.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (_players[j].CurrentStat.SPEED < _players[j + 1].CurrentStat.SPEED)
                {
                    // Swap _players[j] and _players[j+1]
                    VSPlayer temp = _players[j];
                    _players[j] = _players[j + 1];
                    _players[j + 1] = temp;
                }
            }
        }
    }



}
