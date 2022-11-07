using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    public UnityEvent e_ClickScreen;

    private void Awake()
    {
        Observer.RegisterEventFromAllGameObjects<IClickScreen>("ClickScreen", e_ClickScreen);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            e_ClickScreen?.Invoke();
        

        if (Input.touchCount > 0)
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                e_ClickScreen.Invoke();
        
    }
}
