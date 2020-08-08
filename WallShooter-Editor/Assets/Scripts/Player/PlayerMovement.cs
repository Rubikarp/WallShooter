using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rgb = null;
    private Collider2D collid = null;
    private InputHandler input = null;
    [Header("Variable")]
    public LayerMask EnviroLayerMask;
    [Space(10)]
    public Vector2 Orientation;
    public Vector2 Velocity;
    [SerializeField] bool isOnGround;
    public bool IsOnGround
    {
        get { return isOnGround; }
    }
    [SerializeField] bool isShooting;
    public bool IsShooting
    {
        get { return isShooting; }
        set { isShooting = value; }
    }

    [Header("Run")]
    private float accTimer = 0f, decTimer = 0f;
    [SerializeField] [Range(5, 25)] float maxRunSpeed = 12;
    [SerializeField] [Range(5, 25)] float maxAirSpeed = 12;
    [SerializeField] [Range(5, 25)] float maxSpeed = 12;
    [SerializeField] [Range(0, 5)] float inAirCoeff = 5;
    [SerializeField] [Range(0, 5)] float airFiction = 5;
    [SerializeField] AnimationCurve accelerationCurve = AnimationCurve.EaseInOut(0, 0, 0.2f, 1);
    [SerializeField] AnimationCurve deccelerationCurve = AnimationCurve.EaseInOut(0, 1, 0.1f, 0);

    [Header("Jump")]
    [SerializeField] [Range(1, 15)] float jumpForce = 15f;
    [Space(10)]
    [SerializeField] [Range(0, 5)] float normalFactor = 2f;
    [SerializeField] [Range(0, 5)] float fallFactor = 2f;
    [SerializeField] [Range(0, 5)] float lowJumpFactor = 2f;

    private void Awake()
    {
        input = InputHandler.Instance;
        rgb = this.gameObject.GetComponent<Rigidbody2D>();
        collid = this.gameObject.GetComponent<Collider2D>();
        
    }

    void Start()
    {
        if (collid == null)
        {
            Debug.LogError("Le collider n'a pas été attribué", this.gameObject);
        }

        PlayerOrientation();
        
    }

    private void FixedUpdate()
    {
        SpeedLimitation();
    }
    private void Update()
    {
        CheckGround();
        PlayerOrientation();

        Run(maxRunSpeed);
        Jump();
    }


    private void Run(float maxRunSpeed)
    {
        

        float activeSpeed = 0f;
        float inAirFactor = isOnGround ? 1f : inAirCoeff;
        float mouvMagnitude = input.mouvAxis.magnitude > 1 ? 1f : input.mouvAxis.magnitude;

        if (input.mouvAxis.magnitude > 0.1)
        {
            //incrémentation du timer en fonction du temps
            accTimer += Time.deltaTime;

            //determine la vitesse

            if (activeSpeed > 1)
            {
                activeSpeed = maxRunSpeed * mouvMagnitude * inAirFactor;
            }
            else
            {
                activeSpeed = maxRunSpeed * mouvMagnitude * inAirFactor * accelerationCurve.Evaluate(accTimer);
            }

            if (isOnGround)
            {
                //applique la vitesse
                rgb.velocity = new Vector2(input.mouvAxis.x * activeSpeed, rgb.velocity.y);
            }
            else
            {
                if (!IsShooting)
                {
                    if (Mathf.Sign(rgb.velocity.x) != Mathf.Sign(input.mouvAxis.x))
                    {
                        rgb.AddForce(Vector2.right * input.mouvAxis.x * activeSpeed, ForceMode2D.Force);
                    }
                    else if (Mathf.Abs(rgb.velocity.x) < maxAirSpeed)
                    {
                        rgb.AddForce(Vector2.right * input.mouvAxis.x * activeSpeed, ForceMode2D.Force);
                    }
                    else
                    {
                        rgb.velocity += (-Orientation) * airFiction * Time.deltaTime;
                    }
                }
            }

            //Reset decceleration timer
            if (decTimer != 0)
            {
                decTimer = 0f;
            }
        }
        else
        {
            //incrémentation du timer en fonction du temps
            decTimer += Time.deltaTime;

            //determine la vitesse
            activeSpeed = maxRunSpeed * inAirFactor *deccelerationCurve.Evaluate(decTimer);

            if (isOnGround)
            {
                //applique la vitesse
                rgb.velocity = new Vector2(input.mouvAxis.x * activeSpeed, rgb.velocity.y);
            }
            else
            {
                if(rgb.velocity.x > 1)
                rgb.velocity += (-Orientation) * airFiction * Time.deltaTime;
            }

            //Reset decceleration timer
            if (accTimer != 0)
            {
                accTimer = 0f;
            }
        }

    }

    private void Jump()
    {
        if (input.jumpEnter && isOnGround)
        {
            rgb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }

        if (!isOnGround)
        {
            if (rgb.velocity.y < 0)//Si le joueur à fini de monter, il redescend plus vite afin d'avoir un meilleur game feel
            {
                rgb.gravityScale = fallFactor;
            }
            else //Si le joueur est en train de monter MAIS qu'il lâche la touche de saut bam on augment la gravité pour stop son saut
            if (rgb.velocity.y > 0 && !input.jump)
            {
                rgb.gravityScale = lowJumpFactor;
            }
            else //retour à la normal
            {
                rgb.gravityScale = normalFactor;
            }
        }
    }

    private void SpeedLimitation()
    {
        if( Mathf.Abs(rgb.velocity.magnitude) > maxSpeed)
        {
            rgb.velocity = rgb.velocity.normalized * maxSpeed;
        }
    }

    private void PlayerOrientation()
    {
        if (input.mouvAxis.x < 0)
        {
            Orientation = Vector2.left;
        }
        else
        if (input.mouvAxis.x > 0)
        {
            Orientation = Vector2.right;
        }

        Velocity = rgb.velocity;
    }

    private void CheckGround()
    {
        Vector2 ColliderCenter = new Vector2(collid.transform.position.x, collid.transform.position.y) + collid.offset;
        Vector2 ColliderBottom = ColliderCenter + Vector2.down * collid.bounds.extents.y;

        isOnGround = Physics2D.Linecast(ColliderBottom, ColliderBottom + Vector2.down * 0.2f, EnviroLayerMask);

        #region Debug
        Color castColor;
        castColor = isOnGround? Color.green: Color.red;
        Debug.DrawLine(ColliderBottom, ColliderBottom + Vector2.down * 0.2f, castColor);
        #endregion
    }
}
