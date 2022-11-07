using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour, IClickScreen
{
    [SerializeField] float lenght;
    [SerializeField] float height;
    bool IsZ;

    private void OnEnable()
    {
        if (IsZ)
        {
            transform.position = new Vector3(GameController.s_Instance._target.x, height, lenght);
        }
        else
        {
            transform.position = new Vector3(-lenght, height, GameController.s_Instance._target.z);
        }
        var dir = GameController.s_Instance._target - transform.position;
        dir.y = 0;
        GetComponent<Movement>()._target = dir.normalized;
        IsZ = !IsZ;
        height += 0.1f;
    }
    public void ClickScreen()
    {
        this.gameObject.SetActive(false);
    }
}
