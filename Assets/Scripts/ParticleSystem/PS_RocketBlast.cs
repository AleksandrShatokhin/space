using System.Collections;
using UnityEngine;

public class PS_RocketBlast : MonoBehaviour
{
    protected float damage = 3.0f;
    [SerializeField] private SphereCollider blastCol;

    void Start()
    {
        //blastCol = GetComponentInChildren<SphereCollider>();
        blastCol.enabled = false;

        StartCoroutine(Blast());
        Destroy(gameObject, 2);
    }

    void Update()
    {
        
    }

    IEnumerator Blast()
    {
        yield return new WaitForSeconds(0.1f);
        blastCol.enabled = true;
    }
}
