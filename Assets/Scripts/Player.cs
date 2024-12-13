using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private GameObject disparo_T1Prefab;
    [SerializeField] private GameObject fire_point_1;
    [SerializeField] private GameObject fire_point_2;
    [SerializeField] private GameObject vida_1;
    [SerializeField] private GameObject vida_2;
    [SerializeField] private GameObject vida_3;
    [SerializeField] private float fireRate = 0.5f; // Tiempo entre disparos
    [SerializeField] private float gradosDisparo;
    [SerializeField] private float velocidadDisparo = 10f; // Velocidad del proyectil
    private float vidas = 60;
       


    private ObjectPool<GameObject> projectilePool; // Pool de proyectiles
    private float lastFireTime; // Tiempo del último disparo

    // Variables para los PowerUps
    private bool powerUp_Active = false;
    private bool powerUp2_Active = false;
    private bool powerUp3_Active = false;

    void Start()
    {
        // Inicializamos el pool de proyectiles
        projectilePool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(disparo_T1Prefab), // Crear un nuevo proyectil
            actionOnGet: (projectile) => projectile.SetActive(true), // Activar el proyectil al obtenerlo
            actionOnRelease: (projectile) => projectile.SetActive(false), // Desactivar el proyectil al liberarlo
            actionOnDestroy: (projectile) => Destroy(projectile), // Destruir el proyectil si se elimina del pool
            defaultCapacity: 20, // Capacidad inicial
            maxSize: 50 // Tamaño máximo
        );
    }

    void Update()
    {
        Movimiento();
        Delimitarmovimiento();
        Disparar();
    }

    void Movimiento()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputH, inputV).normalized * velocidad * Time.deltaTime);
    }

    void Delimitarmovimiento()
    {
        float xClamped = Mathf.Clamp(transform.position.x, -150f, 150f);
        float yClamped = Mathf.Clamp(transform.position.y, -82f, 82f);
        transform.position = new Vector3(xClamped, yClamped, 0);
    }

    void Disparar()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > lastFireTime + fireRate)
        {
            if (powerUp_Active)
            {
                // Disparos modificados por PowerUp
                CrearDisparo(fire_point_1.transform.position, 10f, 200f);
                CrearDisparo(fire_point_1.transform.position, -10f, 200f);
                CrearDisparo(fire_point_2.transform.position, 10f, 200f);
                CrearDisparo(fire_point_2.transform.position, -10f, 200f);
            }
            else if (powerUp2_Active)
            {
                // Disparos modificados por PowerUp_2
                CrearDisparo(fire_point_1.transform.position, 0f, 150f);
                CrearDisparo(fire_point_1.transform.position, -30f, 150f);
                CrearDisparo(fire_point_1.transform.position, 30f, 150f);
                CrearDisparo(fire_point_2.transform.position, 0f, 150f);
                CrearDisparo(fire_point_2.transform.position, -30f, 150f);
                CrearDisparo(fire_point_2.transform.position, 30f, 150f);
            }
            else if (powerUp3_Active)
            {
                // Disparos modificados por PowerUp_3 en forma de onda
                for (float angle = -45f; angle <= 45f; angle += 15f)
                {
                    CrearDisparo(fire_point_1.transform.position, angle, 50f);
                    CrearDisparo(fire_point_2.transform.position, angle, 50f);
                }
            }
            else
            {
                // Disparos desde fire_point_1
                CrearDisparo(fire_point_1.transform.position, gradosDisparo, velocidadDisparo);
                CrearDisparo(fire_point_1.transform.position, -gradosDisparo, velocidadDisparo);

                // Disparos desde fire_point_2
                CrearDisparo(fire_point_2.transform.position, gradosDisparo, velocidadDisparo);
                CrearDisparo(fire_point_2.transform.position, -gradosDisparo, velocidadDisparo);
            }

            lastFireTime = Time.time; // Actualizar el tiempo del último disparo
        }
    }

    void CrearDisparo(Vector3 posicion, float angulo, float velocidad)
    {
        // Obtener proyectil del pool
        GameObject projectile = projectilePool.Get();
        projectile.transform.position = posicion;

        // Rotar el proyectil en el ángulo deseado
        projectile.transform.rotation = Quaternion.Euler(0, 0, angulo);

        // Aplicar velocidad al proyectil
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Convertir ángulo a vector de dirección
            Vector2 direccion = new Vector2(Mathf.Cos(angulo * Mathf.Deg2Rad), Mathf.Sin(angulo * Mathf.Deg2Rad));
            rb.velocity = direccion * velocidad; // Velocidad del proyectil
        }
    }

    public void ReleaseProjectile(GameObject projectile)
    {
        // Liberar el proyectil de vuelta al pool
        projectilePool.Release(projectile);
    }

    // Método para detectar colisiones con ítems
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Disparo_Enemigo") || collision.gameObject.CompareTag("Enemigo"))
        {

            vidas -= 20;
            Destroy(collision.gameObject);
            switch (vidas)
            {
                case 40:
                    Destroy(vida_1);
                    break;

                case 20:
                    Destroy(vida_2);
                    break;

                case 0:
                    Destroy(vida_3);
                    break;
            }
            if (vidas <= 0)
            {
                
                StartCoroutine(Restart ());
                Destroy(this.gameObject);
            }
        }

        
        if (collision.CompareTag("PowerUp"))
        {
            PowerUp powerUp = collision.GetComponent<PowerUp>();
            if (powerUp != null)
            {
                powerUp.AplicarEfecto(this);
            }
        }
        else if (collision.CompareTag("PowerUp_2"))
        {
            PowerUp_2 powerUp2 = collision.GetComponent<PowerUp_2>();
            if (powerUp2 != null)
            {
                powerUp2.AplicarEfecto(this);
            }
        }
        else if (collision.CompareTag("PowerUp_3"))
        {
            PowerUp_3 powerUp3 = collision.GetComponent<PowerUp_3>();
            if (powerUp3 != null)
            {
                powerUp3.AplicarEfecto(this);
            }
        }
    }
    IEnumerator Restart()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return new WaitForSeconds(5f);
    }

    // Métodos para activar y desactivar los PowerUps
    public void ActivarPowerUp(float duracion)
    {
        StartCoroutine(ActivarPowerUpTemporalmente(duracion));
    }

    private IEnumerator ActivarPowerUpTemporalmente(float duracion)
    {
        powerUp_Active = true;
        yield return new WaitForSeconds(duracion);
        powerUp_Active = false;
    }

    public void ActivarPowerUp2(float duracion)
    {
        StartCoroutine(ActivarPowerUp2Temporalmente(duracion));
    }

    private IEnumerator ActivarPowerUp2Temporalmente(float duracion)
    {
        powerUp2_Active = true;
        yield return new WaitForSeconds(duracion);
        powerUp2_Active = false;
    }

    public void ActivarPowerUp3(float duracion)
    {
        StartCoroutine(ActivarPowerUp3Temporalmente(duracion));
    }

    private IEnumerator ActivarPowerUp3Temporalmente(float duracion)
    {
        powerUp3_Active = true;
        yield return new WaitForSeconds(duracion);
        powerUp3_Active = false;
    }

   
}
