using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner_Enemigo : MonoBehaviour
{
    [SerializeField] private GameObject enemigoPrefab_1;
    [SerializeField] private GameObject enemigoPrefab_2;
    [SerializeField] private GameObject enemigoPrefab_3;
    
    [SerializeField] private GameObject PowerUpPrefab_1;
    [SerializeField] private GameObject PowerUpPrefab_2;
    [SerializeField] private GameObject PowerUpPrefab_3;
    [SerializeField] private TextMeshProUGUI textFinish_UI;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnearEnemigo());

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator SpawnearEnemigo()
    {
        float peridos = 1.5f;
        for (int i = 0;i < 3; i++) //Niveles
        {
            for(int j = 0; j < 2; j++)//Oleadas
            {
                 for (int k = 0; k < 40; k++)//Enemigos
                 {
                    Vector3 puntorandom = new Vector3(transform.position.x, Random.Range(-82f, 82f), 0);

                    switch (i)
                    {
                        case 0:
                            switch (j)
                            {
                                case 0:
                                    Instantiate(enemigoPrefab_1, puntorandom, Quaternion.identity);
                                    yield return new WaitForSeconds(peridos);
                                    break;

                                case 1:
                                    Instantiate(enemigoPrefab_1, puntorandom, Quaternion.identity);
                                    peridos = 0.8f;
                                    yield return new WaitForSeconds(peridos);
                                    break;

                            }
                            break;
                        case 1:
                            switch (j)
                            {
                                case 0:
                                    Instantiate(enemigoPrefab_2, puntorandom, Quaternion.identity);
                                    peridos = 1f;
                                    yield return new WaitForSeconds(peridos);
                                    break;

                                case 1:
                                    Instantiate(enemigoPrefab_2, puntorandom, Quaternion.identity);
                                    peridos = 0.8f;
                                    yield return new WaitForSeconds(peridos);
                                    break;

                            }
                            break;

                        case 2:
                            switch (j)
                            {
                                case 0:
                                    Instantiate(enemigoPrefab_3, puntorandom, Quaternion.identity);
                                    peridos = 0.9f;
                                    yield return new WaitForSeconds(peridos);
                                    break;

                                case 1:
                                    Instantiate(enemigoPrefab_3, puntorandom, Quaternion.identity);
                                    peridos = 0.5f;
                                    yield return new WaitForSeconds(peridos);
                                    break;

                            }
                            break;
                    }
                   





                }


               
                yield return new WaitForSeconds(3f);

            }

        
            yield return new WaitForSeconds(2f);

        }
        textFinish_UI.text = "FINISH";
    }
}
