using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnItem
    {
        public string nombre;
        public GameObject prefab;
        public float alturaY = 0f;
        [Tooltip("Marcar si es una caja, pincho o trampa (seguirá saliendo durante el jefe)")]
        public bool esObstaculo = false; 
    }

    [Header("Lista de Objetos y Enemigos")]
    public SpawnItem[] elementosASpawnear;

    [Header("Intervalos de tiempo (segundos)")]
    public float tiempoMinimo = 1.8f;
    public float tiempoMaximo = 3.2f;

    void Start()
    {
        StartCoroutine(SpawnRutina());
    }

    IEnumerator SpawnRutina()
    {
        while (true)
        {
            float tiempoEspera = Random.Range(tiempoMinimo, tiempoMaximo);
            yield return new WaitForSeconds(tiempoEspera);

            if (elementosASpawnear.Length == 0) continue;

            bool jefeEnPantalla = GameManager.instance != null && GameManager.instance.jefeActivo;

            
            List<SpawnItem> itemsValidos = new List<SpawnItem>();

            foreach (var item in elementosASpawnear)
            {
                if (item.prefab == null) continue;

                
                if (jefeEnPantalla)
                {
                    if (item.esObstaculo)
                    {
                        itemsValidos.Add(item);
                    }
                }
                else
                {
                    
                    itemsValidos.Add(item);
                }
            }

            
            if (itemsValidos.Count > 0)
            {
                int index = Random.Range(0, itemsValidos.Count);
                SpawnItem itemElegido = itemsValidos[index];

                Vector3 posicionSpawn = new Vector3(transform.position.x, itemElegido.alturaY, transform.position.z);
                Instantiate(itemElegido.prefab, posicionSpawn, Quaternion.identity);
            }
        }
    }
}