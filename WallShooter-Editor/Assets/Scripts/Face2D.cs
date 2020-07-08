using UnityEngine;

public class Face2D : MonoBehaviour
{
    public float rotateSpeed = 5.0f;
    [SerializeField] float angleOffSet = 0f;
    [SerializeField] Transform target = null;

    private void Update()
    {
        LookAt2D(target.position);
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
