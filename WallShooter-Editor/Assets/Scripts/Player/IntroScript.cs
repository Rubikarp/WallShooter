using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private InputHandler input;
    [SerializeField] private float startTime = 1.5f;
    private void Awake()
    {
        input._canInput = false;
        Invoke("InputActivate", startTime);
    }

    public void InputActivate()
    {
        input._canInput = true;
    }
}
