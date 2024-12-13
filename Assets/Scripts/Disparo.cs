using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    [SerializeField] private float velocidad_D;
    [SerializeField] private Vector3 direccion;
   
    public float lifeTime = 3f; // Tiempo de vida del proyectil
    private Player player;

    void Start()
    {

       
    }
    void OnEnable()
    {
        Invoke("ReturnToPool", lifeTime);
    }

    void OnDisable()
    {
        CancelInvoke(); // Cancelar la devolución si el proyectil se reutiliza
    }

    void ReturnToPool()
    {
        if (player == null)
        {
            // Usar el nuevo método recomendado
            player = Object.FindFirstObjectByType<Player>();
        }

        if (player != null)
        {
            player.ReleaseProjectile(gameObject);
        }
        else
        {
            Debug.LogWarning("No se encontró el JefepreFab 'Player'. Asegúrate de que esté activo en la escena.");
        }
    }

    void OnBecameInvisible()
    {
        ReturnToPool(); // Devolver al pool si sale de la pantalla
    }

    void Update()
    {
        transform.Translate(direccion * velocidad_D * Time.deltaTime);
        
    }
}
