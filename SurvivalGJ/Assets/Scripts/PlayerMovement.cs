using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject zamka;
    public float pomeraj;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    public int brGrana;
    public int bobiceHP;
    public int stakeHP;
    public bool nextToBush = false;
    public bool isHidden = false;

    private SpriteRenderer sprite;
    [SerializeField] private LayerMask jumpableGround;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField]  private float jumpForce = 8f;
    private float spiderWebSlowdownFactor = 1f;
    private MovementState state;
    internal bool isHunted;

    private enum MovementState { idle, running, jumping, falling }
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHidden)
        {
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * moveSpeed * spiderWebSlowdownFactor, rb.velocity.y);
        }         
        if (Input.GetButtonDown("Jump") && IsGrounded() && !isHidden)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            NapraviZamku();
        }
        if (Input.GetKeyDown(KeyCode.F) && nextToBush && !isHunted)
        {
            UdiIzadjiIzBusha();
        }       

        UpdateAnimationState();
        
    }

    private void UdiIzadjiIzBusha()
    {
        if (isHidden)
        {            
            isHidden = false;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Animator>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<Rigidbody2D>().gravityScale = 1;

            RaycastHit2D[] niz = Physics2D.CircleCastAll(transform.position, 2, Vector2.zero);

            foreach (RaycastHit2D rh in niz)
            {
                if (rh.collider.transform.tag == "Bush")
                {
                    BushScript bs = rh.collider.GetComponent<BushScript>();
                    bs.IgracJeIzasao();
                    break;
                }
            }
        }
        else
        {
            isHidden = true;
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            nextToBush = true;
            RaycastHit2D[] niz = Physics2D.CircleCastAll(transform.position, 2, Vector2.zero);

            foreach (RaycastHit2D rh in niz)
            {
                if (rh.collider.transform.tag == "Bush")
                {
                    BushScript bs = rh.collider.GetComponent<BushScript>();
                    bs.SakrijiIgraca();
                    break;
                }
            }
        }
        GameManager.Instance.PustiZvuk("bushsfx_final");
    }

    private void NapraviZamku()
    {
        if (brGrana > 0 && state == MovementState.idle)
        {
            brGrana -= 2;
            Instantiate(zamka, transform.position, new Quaternion());
        }
    }

    private void UpdateAnimationState()
    {
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);

    }

    public void ApplySpiderWebSlowdown(float slowdownFactor)
    {
        spiderWebSlowdownFactor = slowdownFactor;
    }

    public void ResetSpiderWebSlowdown()
    {
        spiderWebSlowdownFactor = 1f; // Reset to default (no slowdown)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        PlayerLifeHP plHP = GetComponent<PlayerLifeHP>();

        switch (collision.gameObject.tag)
        {
            case "Grana":
                Destroy(collision.gameObject);
                brGrana++;
                Debug.Log("Grana: " + brGrana);
                break;
            case "Bobica":
                Debug.Log("Pojeo bobicu");
                Destroy(collision.gameObject);
                
                if (plHP.currentHealth + stakeHP > 100)
                {
                    plHP.currentHealth = 100;
                }
                else
                {
                    plHP.currentHealth += stakeHP;
                }
                break;
            case "Stake":
                Debug.Log("Pojeo stake");
                Destroy(collision.gameObject);
                if (plHP.currentHealth + bobiceHP > 100)
                {
                    plHP.currentHealth = 100;
                }
                else
                {
                    plHP.currentHealth += bobiceHP;
                }
                break;
        }
    }

    internal void Odgurni(Vector3 vector3)
    {
        Vector3 normV3 = vector3.normalized + new Vector3(7, 0, 0);

        transform.position = Vector3.Lerp(transform.position, normV3, pomeraj);
    }
}
