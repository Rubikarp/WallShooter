using DG.Tweening;
using UnityEngine;

public class Mechanisme : MonoBehaviour, IInteractible
{
    [SerializeField] bool isON = false;
    [Space(10)]
    [SerializeField] private Ease easeType = Ease.InOutCirc;
    [SerializeField] private float mecanDur = 1f;
    [Space(10)]
    [SerializeField] private Vector2 activatePos;
    [SerializeField] private Vector2 desactivatePos;

    public void Interact()
    {
        if (isON)
        { Desactivate(); }
        else
        { Activate(); }

        isON = !isON;
    }

    public void Activate()
    {
        transform.DOMove(activatePos, mecanDur).SetEase(easeType);
    }
    public void Desactivate()
    {
        transform.DOMove(desactivatePos, mecanDur).SetEase(easeType);
    }

}
