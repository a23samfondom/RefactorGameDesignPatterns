using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private EnemyFactoryPool enemyFactoryPool; // creamos la referencia a enemyFactoryPool, que controla el metodo de creación de enemigos
	[SerializeField] private Transform spawnPoint; //Referencia transform del spawn point donde aparecerá el enemigo

    private GameObject currentEnemy; //Guarda una referencia al enemigo creado

    private void Start()
    {
        CrearEnemigo(); //Crea un enemigo al empezar el juego
    }

    public void CrearEnemigo()
    {
        currentEnemy = enemyFactoryPool.CreateEnemy(spawnPoint.position, spawnPoint.rotation); // llamamos al metodo de crear un enemigo

	}

}
