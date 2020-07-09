using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehavior : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rgb = null;

    [Header("Variable")]
    public Vector2 Orientation;
    [SerializeField] float angleCorrection = 0;
    
    /*
    [Space(10)]
    [SerializeField] float speed = 35f;
    [SerializeField] int damage = 2;
    
    [Space(10)]
    [SerializeField] bool isAffectByGravity = false;
    [SerializeField] bool isLifetime = false;
    [SerializeField] float lifetime = 1f;
    [SerializeField] bool haveMaxRange = false;
    [SerializeField] float maxtravellingDist = 10f;
    */

    private void Awake()
    {
        rgb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rgb.velocity.magnitude > 1)
        {
            FaceShootingDirection(rgb.velocity.normalized);
        }
    }

    public void SetShoot(Vector2 shootingDirection, float force)
    {
        Orientation = shootingDirection;
        rgb.AddForce(Orientation * force, ForceMode2D.Impulse);
    }

    public void FaceShootingDirection(Vector2 shootingDirection)
    {
        //calcul l'angle pour faire face au joueur
        float rotZ = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg + angleCorrection;
        //oriente l'object pour faire face au joueur
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
