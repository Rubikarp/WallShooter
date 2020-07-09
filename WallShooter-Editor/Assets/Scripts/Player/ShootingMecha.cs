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
    [SerializeField] Transform bulletContainer = null;
    [SerializeField] GameObject bullet = null;

    [Header("Shoot")]
    [SerializeField] int bulletCapacity = 3;
    [SerializeField] int bulletInMagasine = 3;
    [SerializeField] bool canBlankShoot = true;
    [SerializeField] float blankShootCD = 0.3f;
    [SerializeField] float shootStunDur = 0.8f;

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

    void LateUpdate()
    {
        if (!player.IsOnGround)
        {
            if (input.jumpEnter)
            {
                if (bulletInMagasine > 0)
                {
                    rgb.velocity = Vector2.zero;
                    player.IsShooting = true;

                    //Bim! le tir
                    rgb.AddForce(gun.right * -1 * recoilForce, ForceMode2D.Impulse);

                    Vector2 bulletDir = gun.right;

                    //moins une balle
                    bulletInMagasine--;

                    //Elle est crée
                    GameObject shootedBullet = Instantiate( bullet, gun.position + (gun.right * 0.5f), Quaternion.identity, bulletContainer);

                    //Elle est partie
                    BulletBehavior shootedBulletBehav = shootedBullet.GetComponent<BulletBehavior>();
                    shootedBulletBehav.SetShoot(bulletDir, recoilForce);

                    Invoke("EndShoot", shootStunDur);
                }
                else
                if(canBlankShoot)
                {
                    rgb.velocity = Vector2.zero;
                    player.IsShooting = true;

                    //Bim! le tir
                    rgb.AddForce(gun.right * -1 * blankForce, ForceMode2D.Impulse);

                    //calm down
                    canBlankShoot = false;

                    //
                    Invoke("BlankReload", blankShootCD);
                    Invoke("EndShoot", shootStunDur);

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

    private void EndShoot()
    {
        player.IsShooting = false;
    }

    private void BlankReload()
    {
        canBlankShoot = true;
    }
}
