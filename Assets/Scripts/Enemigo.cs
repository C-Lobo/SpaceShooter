using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float velocidad_En;
    [SerializeField] private Renderer enemigorenderer;
    [SerializeField] private GameObject disparoEnemigoPrefab;
    [SerializeField] private GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (SpawnerDisparo());
        enemigorenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * velocidad_En * Time.deltaTime);
        if (!enemigorenderer.isVisible) 
        { 
            Destroy(gameObject); 
        }
    }

    IEnumerator SpawnerDisparo()
    {
       // for (int i = 0; i < 10; i++)
           while(true)         
        {
            Instantiate(disparoEnemigoPrefab, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.8f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Disparo"))
        {

            
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
