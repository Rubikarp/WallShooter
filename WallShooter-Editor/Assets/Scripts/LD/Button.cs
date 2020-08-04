using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private List<Mechanisme> mechanismes = new List<Mechanisme>();
    private bool activatable = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player/Hurtbox") && !activatable)
        {
            Appuyer();
        }

        if (collision.CompareTag("Player/Bullet") &&!activatable)
        {
            Appuyer();
        }
    }

    public void Appuyer()
    {
        activatable = true;

        foreach (Mechanisme mecha in mechanismes)
        {
            mecha.Interact();
        }

        Invoke("cooldown", 0.5f);
    }

    public void cooldown()
    {
        activatable = false;
    }

}
