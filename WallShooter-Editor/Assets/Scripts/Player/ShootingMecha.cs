using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerMovement))]

public class ShootingMecha : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rgb = null;
    private InputHandler input = null;
    private PlayerMovement player = null;
    [SerializeField] Transform gun = null;
    [SerializeField] GameObject bullet = null;
    [Header("Shoot")]
    [SerializeField] int bulletCapacity = 3;
    [SerializeField] int bulletInMagasine = 3;


    [Header("Projection")]
    [SerializeField] float recoilForce = 15f;
    [SerializeField] [Range(1, 15)] float blankForce = 15f;


    private void Awake()
    {
        input = InputHandler.Instance;
        rgb = this.gameObject.GetComponent<Rigidbody2D>();
        player = this.gameObject.GetComponent<PlayerMovement>();
    }

    void Start()
    {
        bulletInMagasine = bulletCapacity;
    }


    void Update()
    {
        if (!player.IsOnGround)
        {
            if (input.jump)
            {
                if (bulletInMagasine > 0)
                {
                    //Bim! le tir
                    rgb.AddForce(gun.right * -1 * recoilForce, ForceMode2D.Impulse);

                    Vector2 bulletDir = gun.right;

                    //moins une balle
                    bulletInMagasine--;

                    //Elle est crée
                    GameObject shootedBullet = Instantiate( bullet, gun.position + (gun.right * 0.5f), Quaternion.identity);

                    //Elle est partie
                    BulletBehavior shootedBulletBehav = shootedBullet.GetComponent<BulletBehavior>();
                    shootedBulletBehav.SetShoot(bulletDir, recoilForce);
                }
                else
                {
                    //Bim! le tir
                    rgb.AddForce(gun.right * -1 * blankForce, ForceMode2D.Impulse);

                }
            }
        }
        else
        {
            if (bulletInMagasine != bulletCapacity)
            {
                bulletInMagasine = bulletCapacity;
            }
        }
    }
}
