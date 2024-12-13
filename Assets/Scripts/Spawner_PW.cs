using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_PW : MonoBehaviour
{
    [SerializeField] private GameObject PowerUpPrefab_1;
    [SerializeField] private GameObject PowerUpPrefab_2;
    [SerializeField] private GameObject PowerUpPrefab_3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnearPW());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnearPW()
    {
        float peridos = 15f;
        
        for (int i = 0; i < 10; i++) //Niveles
        {
            for (int j = 0; j < 3; j++)//Oleadas
            {

                
                for (int k = 0; k < 5; k++)//Enemigos
                {
                    Vector3 puntorandom = new Vector3(transform.position.x, Random.Range(-82f, 82f), 0);

                    switch (j)
                    {
                        case 0:
                            Instantiate(PowerUpPrefab_1, puntorandom, Quaternion.identity);
                            yield return new WaitForSeconds(peridos);
                            break;

                        case 1:
                            Instantiate(PowerUpPrefab_2, puntorandom, Quaternion.identity);
                            
                            yield return new WaitForSeconds(peridos);
                            break;

                        case 2:
                            Instantiate(PowerUpPrefab_3, puntorandom, Quaternion.identity);
                            
                            yield return new WaitForSeconds(peridos);
                            break;
                    }






                }

                yield return new WaitForSeconds(3f);

            }


            yield return new WaitForSeconds(5f);

        }
    }
}

