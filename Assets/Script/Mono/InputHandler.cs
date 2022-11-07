using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    UnityEvent e_AfterClickScreen;
    public UnityEvent e_ClickScreen;

    private void Awake()
    {
        e_AfterClickScreen = new UnityEvent();
        Observer.RegisterEventFromAllGameObjects<IAfterClickScreen>("ClickScreen", e_AfterClickScreen);
        Observer.RegisterEventFromAllGameObjects<IClickScreen>("ClickScreen", e_ClickScreen);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            e_ClickScreen?.Invoke();
        

        if (Input.touchCount > 0)
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                e_ClickScreen.Invoke();
                e_AfterClickScreen.Invoke();
            }
               
        
    }
}
