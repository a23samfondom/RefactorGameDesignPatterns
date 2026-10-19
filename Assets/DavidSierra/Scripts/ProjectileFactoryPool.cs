using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactoryPool : MonoBehaviour
{
	[SerializeField] private GameObject projectilePrefab; // prefab del proyectil
	[SerializeField] private int initialAmount = 10;// la cantidad inicial de proyectles que vamos a crear para la pool

	private List<GameObject> pool = new List<GameObject>(); // creamos la pool, es una lista que guarda los proyectiles

	private void Awake() // al empezar, crea los proyectiles que hemos indicado en la variable initialAmount, 
	{
		for (int i = 0; i < initialAmount; i++)
		{
			GameObject projectile = Instantiate(projectilePrefab); // instancia el proyectil
			projectile.SetActive(false); // lo desactiva
			pool.Add(projectile); // lo añade al pool
		}
	}
	

	// esto es un FACTORY prepara y entrega un proyectil para disparar.
	public GameObject CreateProjectile(Vector3 position, Vector2 direction)
	{
		GameObject projectile;

		
		if (pool.Count > 0) // esto obtiene el proyectil y lo elimina de la lista porque lo vamos ha utilizar
		{
			projectile = pool[0];
			pool.RemoveAt(0);
		}
		else
		{
			// Si no quedan proyectiles disponibles, crea uno nuevo.
			projectile = Instantiate(projectilePrefab);
		}

		projectile.transform.position = position; // coge la posicion del proyectil y la guarda en la variable posicion

		Projectile projectileScript = projectile.GetComponent<Projectile>(); // busca el script que tiene el proyectil, que es projectile, para acceder al metodo Init

		projectileScript.SetPool(this); // pasamos la referencia del pool al proyectil

		projectile.SetActive(true); // activa el proyectil
		
		
		projectileScript.Init(direction); //utiliza el metodo del poryectil para mandar el proyectil a la dirección que toca

		

		return projectile; // devuelve el proyectil
	}

	// Devuelve el proyectil al pool para poder reutilizarlo.
	public void ReturnProjectile(GameObject projectile)
	{
		projectile.SetActive(false); // desactiva el proyectil
		pool.Add(projectile); // lo añade de nuevo al pool
	}
}
