using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EksplozivnaZamka : MonoBehaviour
{
    private GameObject igrac;
    private bool isExploding = false;
    private float radius = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isExploding == false && other.gameObject.tag.Equals("Enemy"))
        {
            isExploding = true;
            //Debug.Log("boom");
            Explosion();
            Destroy(transform);
        }
    }

    private void Explosion()
    {  //playExplosionAnimation
        RaycastHit2D[] rc = Physics2D.CircleCastAll(this.transform.position, radius, Vector2.zero, 0f);

        foreach (RaycastHit2D r in rc)
        {
            if (r.collider.gameObject.tag.Equals("Enemy"))
            {
                EnemyScript es = r.collider.GetComponent<EnemyScript>();
                es.UnistiSe();             
            }

        }

    }
}
