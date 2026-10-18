using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; //Referencia del prefab creado del enemigo
    [SerializeField] private Transform spawnPoint; //Referencia transform del spawn point donde aparecerá el enemigo

    private GameObject currentEnemy; //Guarda una referencia al enemigo creado

    private void Start()
    {
        CrearEnemigo(); //Crea un enemigo al empezar el juego
    }

    public void CrearEnemigo()
    {
        currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); //Instancia un enemigo en la posicion del spawnpoint
    }

}
