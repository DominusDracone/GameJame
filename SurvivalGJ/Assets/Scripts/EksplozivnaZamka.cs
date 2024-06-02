using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EksplozivnaZamka : MonoBehaviour
{
    private GameObject igrac;
    private bool isExploding = false;
    private float radius = 1;
    // Start is called before the first frame update
    void Start()
    {
        igrac = GameObject.FindGameObjectWithTag("Igrac");
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), igrac.GetComponent<Collider2D>(), true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explosion(GameObject enemy)
    {  //playExplosionAnimation
        RaycastHit2D[] rc = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, 0f);

        foreach (RaycastHit2D r in rc)
        {
            if (r.collider.gameObject.tag.Equals("Enemy"))
            {
                EnemyScript es = r.collider.GetComponent<EnemyScript>();
                es.UnistiSe(transform.gameObject);
            }

        }

    }
}
