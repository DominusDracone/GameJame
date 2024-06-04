using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject zamka;
    public GameObject explozivnaZamka;
    public float pomeraj;
    public TextMeshProUGUI txtGrancica;
    public float intesity = 1000;

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
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            //GameManager.Instance.PustiZvuk("walksfx");
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }    
        if (Input.GetButtonDown("Jump") && IsGrounded() && !isHidden)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            NapraviZamku();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            NapraviExploziju();
        }
        if (Input.GetKeyDown(KeyCode.F) && nextToBush && !isHunted)
        {
            UdiIzadjiIzBusha();
        }       

        UpdateAnimationState();
        
    }

    private void NapraviExploziju()
    {
        if (brGrana >= 5 && state == MovementState.idle)
        {
            brGrana -= 5;
            txtGrancica.text = brGrana.ToString();
            Instantiate(explozivnaZamka, transform.position, new Quaternion());
            GameManager.Instance.PustiZvuk("explosivetrapactivatedsfx");
        }
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
        if (brGrana >= 2 && state == MovementState.idle)
        {
            brGrana -= 2;
            txtGrancica.text = brGrana.ToString();
            Instantiate(zamka, transform.position, new Quaternion());
            GameManager.Instance.PustiZvuk("woodtrapsfx_final");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        PlayerLifeHP plHP = GetComponent<PlayerLifeHP>();

        switch (collision.gameObject.tag)
        {
            case "Grana":
                Destroy(collision.gameObject);
                brGrana++;                
                Debug.Log("Grana: " + brGrana);
                GameManager.Instance.PustiZvuk("woodcollectsfx_final");
                txtGrancica.text = brGrana.ToString();
                break;
            case "Bobica":
                Debug.Log("Pojeo bobicu");
                Destroy(collision.gameObject);
                
                if (plHP.currentHealth + bobiceHP > 100)
                {
                    plHP.currentHealth = 100;
                }
                else
                {
                    plHP.currentHealth += bobiceHP;
                }
                GameManager.Instance.PustiZvuk("eatingsfx_final2");
                break;
            case "Stake":
                Debug.Log("Pojeo stake");
                Destroy(collision.gameObject);
                if (plHP.currentHealth + stakeHP > 100)
                {
                    plHP.currentHealth = 100;
                }
                else
                {
                    plHP.currentHealth += stakeHP;
                }
                GameManager.Instance.PustiZvuk("eatingsfx_final2");
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Dno"))
        {
            GetComponent<PlayerLifeHP>().Die();
        }
    }

    internal void Odgurni(Vector3 hitpos)
    {
        Vector2 knockbackdir = transform.position - hitpos;
        knockbackdir.Normalize();
        knockbackdir.y = 3;
        knockbackdir.x *= intesity;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Debug.Log(">>> " + knockbackdir);
        rb.velocity = knockbackdir;
    }
}
