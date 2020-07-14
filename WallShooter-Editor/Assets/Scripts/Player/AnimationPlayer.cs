using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sprite;
    [Space(10)]
    [SerializeField] PlayerMovement player;
    [SerializeField] OrientGun gun;
    [SerializeField] Rigidbody2D rgb = null;


    private void Awake()
    {     
        rgb = this.gameObject.GetComponent<Rigidbody2D>();
        anim = this.gameObject.GetComponent<Animator>();
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        float horizontalSpeed = Mathf.Abs(rgb.velocity.x);
        anim.SetFloat("horizontal", horizontalSpeed); 
        anim.SetFloat("vertical", rgb.velocity.y);
      
        FlipOrientation();
        JumpAnim();
    }

    private void FlipOrientation()
    {
        if (player.Orientation == Vector2.right)
        {
            sprite.flipX = false;
        }
        if (player.Orientation == Vector2.left)
        {
            sprite.flipX = true;
        }
    }

    private void JumpAnim()
    {
        if (player.IsOnGround == true)
        {
            anim.SetBool("isJumping", false);
        }
        if (player.IsOnGround == false)
        {
            anim.SetBool("isJumping", true);
        }
    }

    private void GetAnimationEvent (string eventMessage)
    {
        if(eventMessage.Equals("EventMessage"))
        {
            //Truc
        }
    }
}

