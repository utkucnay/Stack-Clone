using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>, IClickScreen
{
    GameObject _player;
    public Vector3 _target;
    [SerializeField] float _waitSec;
    WaitForSeconds wait;

    bool _endGameCheck;

    UnityEvent e_StartGame;
    UnityEvent e_EndGame;

    private void Start()
    {
        wait = new WaitForSeconds(_waitSec);
    }

    public override void Awake()
    {
        base.Awake();
        e_StartGame = new UnityEvent();
        e_EndGame = new UnityEvent();
        _player = GameObject.FindGameObjectWithTag("Player");
        Observer.RegisterEventFromAllGameObjects<IStartGame>("StartGame", e_StartGame);
        Observer.RegisterEventFromAllGameObjects<IEndGame>("EndGame", e_EndGame);
        e_EndGame.AddListener(() =>
        {
            StopAllCoroutines();
            _endGameCheck = true;
        });
    }

    IEnumerator<WaitForSeconds> SpawnPlayer()
    {
        yield return wait;
        _player.SetActive(true);
    }

    public void ClickScreen()
    {
        if (_endGameCheck) return;
        StartCoroutine(SpawnPlayer());
    }

    public void EndGame()
    {
        e_EndGame.Invoke();
    }
}
