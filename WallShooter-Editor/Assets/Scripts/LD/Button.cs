using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IInteractible
{
    private bool isON = false;
    public bool IsON
    {
        get { return isON; }
        set { isON = value; }
    }

    public void Interact()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
