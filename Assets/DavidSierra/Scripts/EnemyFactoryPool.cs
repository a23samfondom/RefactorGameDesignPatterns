
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryPool : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab; // referencia del prefab enemigo
	[SerializeField] private int initialAmount = 3; // cantidad inicial de enemigos en la pool

	private List<GameObject> pool = new List<GameObject>(); // creamos el pool de los enemigos, este es una lista que guarda a los enemigos
    public static EnemyFactoryPool Instance { get; private set; }

    private void Awake() 
	{
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }

        Instance = this;

        for (int i = 0; i < initialAmount; i++) // al empezar, creamos los enemigos que hemos indicado en la variable initialAmount
        {
			GameObject enemy = Instantiate(enemyPrefab);
			enemy.SetActive(false);
			pool.Add(enemy); // añade al pool
		}
	}

	public GameObject CreateEnemy(Vector3 position, Quaternion rotation) // metodo que va a crear los enemigos
	{
		GameObject enemy; // creamos la variable enemy que sera el enemigo

		if (pool.Count > 0) // si hay mas de un enemigo, lo selecciona y lo saca del pool
		{
			enemy = pool[0];
			pool.RemoveAt(0);
		}
		else // si por lo que sea no hay enemigos, los instancia
		{
			enemy = Instantiate(enemyPrefab);
		}

		// pilla la posicion y rotacion del enemigo
		enemy.transform.position = position; 
		enemy.transform.rotation = rotation;

		// 
		EnemyPoolMember enemyScript = enemy.GetComponent<EnemyPoolMember>(); // coge el script que tiene asignado el enemigo EnemyPoolMember
		enemyScript.SetPool(this); // le manda al enemigo la pool, para que puede reconocer a esta
		enemyScript.ResetEnemy(); // reinicia la vida del enemigo

		enemy.SetActive(true); // activa al enemigo

		return enemy;
	}

	public void ReturnEnemy(GameObject enemy) // este metodo devuelve al enemigo al pool
	{
		enemy.SetActive(false);
		pool.Add(enemy);
	}
}
