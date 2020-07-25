using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private List<Mechanisme> mechanismes = new List<Mechanisme>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player/Bullet"))
        {
            Appuyer();
        }
    }

    public void Appuyer()
    {
        foreach (Mechanisme mecha in mechanismes)
        {
            mecha.Interact();
        }
    }
}
