using UnityEngine;

public class Scr_Character_Bullet : MonoBehaviour
{
    [Header("movement")]
    public Vector3 _direction = Vector3.up;
    private Vector3 _startPos = Vector3.zero;
    public float _maxTravellingDistance = 50f;
    public float _bulletSpeed = 15f;

    [Header("Dégât")]
    public int _bulletDamage = 1;

    private void Start()
    {
        _direction = _direction.normalized;
        _startPos = transform.position;
    }

    private void Update()
    {
        transform.position += _direction * _bulletSpeed * Time.deltaTime;

        if (Vector2.Distance(transform.position, _startPos) > _maxTravellingDistance)
        {
            Destroy(gameObject);
        }
    }
    /*
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Scr_LifeSystem_Ennemis lifesyst = collision.gameObject.GetComponent<Scr_LifeSystem_Ennemis>();

            if (!lifesyst._haveTakeDamage)
            {
                lifesyst.TakingDamange(_bulletDamage);
            }

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(this.transform.position, _direction.normalized, Color.yellow);
    }
    */

}