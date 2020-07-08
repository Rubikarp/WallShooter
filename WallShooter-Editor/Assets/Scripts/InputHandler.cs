using UnityEngine;

/// Fait par Arthur Deleye (arthurdeleye@gmail.com / a.deleye@rubika-edu.com)
/// 
/// [SerializeField] private Scr_InputHandler _input;        
/// _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scr_InputHandler>();

public class InputHandler : Singleton<InputHandler>
{
    [Header("Control")]
    public bool _canInput = true;

    [Header("Input")]
    public Vector2 mouvAxis = Vector2.zero;
    [Space(10)]
    public bool interact;
    public bool jump;

    private void Update()
    {
        if (_canInput)
        {
            //Je prends les valeurs du stick
            mouvAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            //Je prends les buttons
            interact = Input.GetButtonDown("Fire3");
            jump = Input.GetButtonDown("Fire1");
        }
    }

    public void DesactivateControl()
    {
        _canInput = false;

        interact = false;
        jump = false;
    }

    public void ReActivateControl()
    {
        _canInput = true;
    }
}
