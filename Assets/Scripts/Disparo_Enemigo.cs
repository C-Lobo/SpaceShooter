using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Disparo_Enemigo : MonoBehaviour
{
    [SerializeField] private float velocidad_Disparo;
    [SerializeField] private Renderer Disparorenderer;
    [SerializeField] private GameObject disparoEnemigoPrefab;
  //  [SerializeField] private GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        Disparorenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * velocidad_Disparo * Time.deltaTime);
        if (!Disparorenderer.isVisible)
        {
            Destroy(gameObject);
        }
    }
}
