using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientGun : MonoBehaviour
{
    private InputHandler input = null;
    public float rotateSpeed = 5.0f;
    [SerializeField] float angleOffSet = 5.0f;
    [SerializeField] PlayerMovement player = null;
    private void Awake()
    {
        input = InputHandler.Instance;
    }

    private void Update()
    {
        Vector3 inputVect = input.mouvAxis.normalized * 3;

        if (input.mouvAxis.magnitude > 0)
        {
            if (player.IsOnGround)
            {
                LookAt2D(transform.position + inputVect);
            }
            else
            {
                LookAt2D(transform.position - inputVect);
            }
        }

    }

    void LookAt2D(Vector3 lookAtPosition)
    {
        float distanceX = lookAtPosition.x - transform.position.x;
        float distanceY = lookAtPosition.y - transform.position.y;
        float angle = Mathf.Atan2(distanceX, distanceY) * Mathf.Rad2Deg;

        Quaternion endRotation = Quaternion.AngleAxis(angle + angleOffSet, Vector3.back);
        transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, Time.deltaTime * rotateSpeed);
    }
}
