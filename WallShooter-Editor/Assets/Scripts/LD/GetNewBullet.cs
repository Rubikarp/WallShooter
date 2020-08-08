using UnityEngine;

public class GetNewBullet : MonoBehaviour
{
    [Header("Components")]
    private ShootingMecha player = null;
   
    private void Awake()
    {
        player = ShootingMecha.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player/Hurtbox"))
        {
            Debug.Log("bim");
            player.bulletCapacity++;
            Destroy(this.gameObject);
        }
    }
}
