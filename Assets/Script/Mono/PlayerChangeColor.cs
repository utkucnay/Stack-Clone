using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeColor : MonoBehaviour, IClickScreen
{
    Color _white = Color.white;
    Color _black = Color.black;

    Color _currColor = Color.black;

    Material _material;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _material.color = _currColor;
    }

    public void ClickScreen()
    {
        if (_currColor == Color.white)
        {
            _currColor = _black;
            _material.color = Color.black ;

        }
        else
        {
            _currColor = _white;
            _material.color = Color.white;

        }
    }
}
