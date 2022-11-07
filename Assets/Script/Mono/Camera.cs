using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour, IClickScreen
{
    public void ClickScreen()
    {
        this.transform.position += Vector3.up * 0.1f;
    }
}
