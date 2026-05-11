using UnityEngine;
using UnityEngine.Events;

public class Granade : MonoBehaviour
{
    public float timer;
    public float radius;
    public LayerMask mask;
    public GameObject humo;

    public UnityEvent OnExplotion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(OnExplode), timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnExplode()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, radius, mask);

        foreach(var coll in colls)
        {
            //->mueran todos
            //->
        }
        //->sistema de particulas de explosion
        OnExplotion?.Invoke();
        GameObject particle = Instantiate(humo);
        particle.transform.position = transform.position;

        Destroy(gameObject);
    }
}
