using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class VSGameController : MonoBehaviour
{
    [SerializeField] public List<VSPlayer> _players;
    [SerializeField] public List<VSPlayer> TeamRedPlayer;
    [SerializeField] public List<VSPlayer> TeamBluePlayer;

    [Header("POS")]
    [SerializeField] List<Transform> RedTeamPos;
    [SerializeField] List<Transform> BlueTeamPos;

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

        //Debug.LogWarning(t.Count());

        for(int i=0; i < t.Length; i++)
        {
            t[i].transform.parent = transform;
            t[i].GetComponent<RobotSettingAndSOList>().VSSet();

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
    
    public int TeamCount(Team t)
    {
        if(t == Team.Red)
        {
            return TeamBluePlayer.Count;
        }
        else
        {
            return TeamRedPlayer.Count;
        }
    }

    public int TeamSelect(Team t)
    {
        if (t == Team.Red)
        {
            return TeamBluePlayer[0].GameNumber;
        }
        else
        {
            return TeamRedPlayer[0].GameNumber;
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
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => _players[i].GetSkillNum() != -1 && _players[i].GetEnemyNum() != -1);


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
                    yield return StartCoroutine(_players[i].DoSkill(_players[selected]));
                    break;
                }
            }
        }


        int Red = 0, Blue= 0;


        for(int i =0; i < _players.Count; i++)
        {
            _players[i].OnDead(ref Red, ref Blue);
            Debug.Log($"{_players[i].name} : {_players[i].CurrentStat.HP}");
            _players[i].SetEnemyNum(-1);
            _players[i].SetSkillNum(-1);
        }

        if(Red != 0 && Blue != 0)
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
