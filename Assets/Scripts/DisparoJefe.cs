using System.Collections;
using UnityEngine;

public class DisparoJefe : MonoBehaviour
{
    [SerializeField] private float velocidadJefe;
    [SerializeField] private GameObject disparoJefePrefab;
    [SerializeField] private GameObject[] puntosDeDisparo;
    [SerializeField] private Renderer jefeRenderer;
    [SerializeField] private float fireRate = 1f; // Tiempo entre disparos
    [SerializeField] private float velocidadDisparo = 10f; // Velocidad del disparo

    private int currentPattern = 0;

    void Start()
    {
        jefeRenderer = GetComponent<Renderer>();
        InvokeRepeating("Disparar", 0f, fireRate); // Disparar a intervalos regulares
        InvokeRepeating("CambiarPatronDisparo", 5f, 5f); // Cambiar el patrón cada 5 segundos
    }

    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * velocidadJefe * Time.deltaTime);

        if (!jefeRenderer.isVisible)
        {
            Destroy(gameObject);
        }
    }

    void Disparar()
    {
        switch (currentPattern)
        {
            case 0:
                // Patrón simple: un disparo recto desde cada punto
                foreach (var punto in puntosDeDisparo)
                {
                    CrearDisparo(punto.transform.position, 0f);
                }
                break;
            case 1:
                // Patrón intermedio: dos disparos angulados desde cada punto
                foreach (var punto in puntosDeDisparo)
                {
                    CrearDisparo(punto.transform.position, 15f);
                    CrearDisparo(punto.transform.position, -15f);
                }
                break;
            case 2:
                // Patrón avanzado: tres disparos en abanico desde cada punto
                foreach (var punto in puntosDeDisparo)
                {
                    CrearDisparo(punto.transform.position, 0f);
                    CrearDisparo(punto.transform.position, 30f);
                    CrearDisparo(punto.transform.position, -30f);
                }
                break;
            case 3:
                // Patrón complejo: cinco disparos en abanico desde cada punto
                foreach (var punto in puntosDeDisparo)
                {
                    CrearDisparo(punto.transform.position, 0f);
                    CrearDisparo(punto.transform.position, 15f);
                    CrearDisparo(punto.transform.position, -15f);
                    CrearDisparo(punto.transform.position, 30f);
                    CrearDisparo(punto.transform.position, -30f);
                }
                break;
        }
    }

    void CrearDisparo(Vector3 posicion, float angulo)
    {
        GameObject disparo = Instantiate(disparoJefePrefab, posicion, Quaternion.Euler(0, 0, angulo));
        Rigidbody2D rb = disparo.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direccion = new Vector2(-Mathf.Cos(angulo * Mathf.Deg2Rad), Mathf.Sin(angulo * Mathf.Deg2Rad));
            rb.velocity = direccion * velocidadDisparo; // Velocidad del proyectil
        }
    }

    void CambiarPatronDisparo()
    {
        currentPattern = (currentPattern + 1) % 4; // Cambia al siguiente patrón
    }
}
