using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Jefe : MonoBehaviour
{
    [SerializeField] private GameObject EnemigoPrefab_Final;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnearJefe());
    }
    IEnumerator SpawnearJefe()
    {
        for (int i = 0; i < 4; i++)
        {
        yield return new WaitForSeconds(50f);
        Vector3 puntorandom = new Vector3(transform.position.x, Random.Range(-82f, 82f), 0);
        Instantiate(EnemigoPrefab_Final, puntorandom, Quaternion.identity);

        }
        
    }
    
        // Update is called once per frame
        void Update()
    {
        
    }
}
