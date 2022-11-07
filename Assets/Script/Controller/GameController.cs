using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    GameObject _player;
    Vector3 _target;

    private void Start()
    {
        var playerMovement = _player.GetComponent<Movement>();
        playerMovement._target = Vector3.zero - _player.transform.position;
        playerMovement._speed = 1;
    }

    public override void Awake()
    {
        base.Awake();
        _player = GameObject.FindGameObjectWithTag("Player");
    }


}
