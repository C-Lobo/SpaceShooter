using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Final : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 5f;
    [SerializeField] private GameObject disparoEnemigoPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private float velocidad_Disparo;
   

    private Vector2 areaMovimiento;
    private Rigidbody2D rb;
    private float vidaJefe = 500;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CalcularAreaMovimiento();

        StartCoroutine(MoverAleatoriamente());
        StartCoroutine(SpawnerDisparo());
    
    }

 
    IEnumerator SpawnerDisparo()
    {
        while (true)
        {
            Instantiate(disparoEnemigoPrefab, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {
        // Mantener al enemigo dentro del área de movimiento
        Vector2 nuevaPosicion = new Vector2(
            Mathf.Clamp(transform.position.x, areaMovimiento.x, areaMovimiento.x + areaMovimiento.y),
            Mathf.Clamp(transform.position.y, -82f, 82f)
        );
        transform.position = nuevaPosicion;
    }

    void CalcularAreaMovimiento()
    {
        // Calcular el área de movimiento basándose en el tamaño de la pantalla
        float anchoPantalla = Camera.main.orthographicSize * Camera.main.aspect;
        areaMovimiento = new Vector2(anchoPantalla / 2, 50f); // Moverse en la mitad derecha de la pantalla
    }

    IEnumerator MoverAleatoriamente()
    {
        while (true)
        {
            Vector2 direccionMovimiento = new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized;

            rb.velocity = direccionMovimiento * velocidadMovimiento;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Disparo"))
        {
            vidaJefe -= 20;
            Destroy(collision.gameObject);
            if (vidaJefe <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

