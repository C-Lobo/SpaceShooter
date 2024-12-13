using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_3 : MonoBehaviour
{
    [SerializeField] private float duracion = 8f; // Duración del efecto
    [SerializeField] private float velocidadPW;
    [SerializeField] private Renderer PWrenderer;
    [SerializeField] private GameObject PowerUpPrefab_3;
    void Start()
    {
        PWrenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * velocidadPW * Time.deltaTime);
        if (!PWrenderer.isVisible)
        {
            Destroy(gameObject);
        }
    }
    public void AplicarEfecto(Player player)
    {
        player.ActivarPowerUp3(duracion);
        Destroy(gameObject); // Destruir el ítem después de aplicar el efecto

        
    }
}

