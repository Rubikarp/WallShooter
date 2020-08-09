using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerMovement))]

public class ShootingMecha : Singleton<ShootingMecha>
{
    [Header("Components")]
    private Rigidbody2D rgb = null;
    private InputHandler input = null;
    private PlayerMovement player = null;

    [SerializeField] Transform gun = null;
    [SerializeField] Transform bulletContainer = null;
    [SerializeField] GameObject bullet = null;

    [Header("Shoot")]
    public int bulletCapacity = 3;
    [SerializeField] [Range(0, 10)] int sharpnelQuantity = 5;
    [SerializeField] [Range(-90, 90)] float sharpnelSpread = 30f;
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

                    //boom boom les balles
                    ShootShrapnel(bulletDir, recoilForce * 0.5f, sharpnelQuantity, sharpnelSpread);                    

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

    private void ShootShrapnel(Vector2 shootDir, float shootStrength, int quantity, float angleVariance)
    {
        //First Bullet created
        GameObject shootedBullet = Instantiate(bullet, gun.position + (gun.right * 0.5f), Quaternion.identity, bulletContainer);

        //First Bullet have perfect speed and trajectory
        BulletBehavior shootedBulletBehav = shootedBullet.GetComponent<BulletBehavior>();
        shootedBulletBehav.SetShoot(shootDir, shootStrength);


        for (int i = 0; i < quantity; i++)
        {
            //additional bullet
            shootedBullet = Instantiate(bullet, gun.position + (gun.right * 0.5f), Quaternion.identity, bulletContainer);

            //Random dir
            float rdmAngle = -angleVariance + (i*((angleVariance*2)/quantity));
            if(rdmAngle != 0)
            {
                rdmAngle *= Random.Range(-0.95f, -1.05f);
            }
            Vector2 rdmShootDir = Quaternion.Euler(0, 0, rdmAngle) * shootDir;
            float rdmShootStrength = shootStrength * Random.Range(1f, 1.5f) ;

            //Random speed and trajectory
            shootedBulletBehav = shootedBullet.GetComponent<BulletBehavior>();
            shootedBulletBehav.SetShoot(rdmShootDir, rdmShootStrength);
        }
    }

    private void BlankReload()
    {
        canBlankShoot = true;
    }
}
