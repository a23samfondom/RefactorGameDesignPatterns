using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
	[SerializeField] private GameObject coinPrefab; // el prefab de las coins
	[SerializeField] private CoinFactory coinFactory; // referencia del script del coin Factory

	[SerializeField] private int cantidadInicial = 10; // cantidad inicial que hay de monedas, serialize para poder modificar la cantidad desde el editor directamente
	private List<GameObject> monedasGuardadas = new List<GameObject>(); // vale esto es importante, esta es la array de las monedas que guarda el Pool, aqui estan
																		//las monedas guardadas que se van creando.


	
	// VARIABLE Y AWAKE AÑADIDO Para que el enemigo pueda acceder al pool sin tener que asignárselo manualmente
	// a cada enemigo, añadimos una referencia compartida.

	public static CoinPool Instance { get; private set; }

    private void Awake()
    {
		if (Instance != null && Instance != this) { Destroy(gameObject); return; }

		Instance = this;
    }
	//-----------------------------------------//

    void Start()
    {
		// lo que hacemos en este for es hacer instantiate de las monedas que hemos indicado en la variable de cantidadInicial, despues las descativamos y las guardamos
		// el la array de monedasGuardadas.
		for (int i = 0; i < cantidadInicial; i++)
		{
			GameObject moneda = Instantiate(coinPrefab); // hacemos el instantiate del prefab
			moneda.SetActive(false); // desactivamos la moneda
			monedasGuardadas.Add(moneda); // añadimos la moneda a la array
		}
	}

	// este metodo lo que hace es coger la moneda del array para poder utilizarla.
	public GameObject ObtenerMoneda(Vector3 posicion)
	{
		if (monedasGuardadas.Count == 0) // si no hay monedas, las crea utilizando el factory de las coins
		{
			return coinFactory.CreateCoin(posicion);
		}

		GameObject moneda = monedasGuardadas[0]; // crea la variable moneda, que es la moneda que se va a devolver, esta es la primero moneda de la array
		monedasGuardadas.RemoveAt(0); // ahora como hemos cogido una moneda, lo que ahcemos es retirarla del array, porque ya no esta

		moneda.transform.position = posicion; // como en el input se nos da una posicion, pues indicamos que la moneda va a estar en la ubicación que se le asigna
		moneda.SetActive(true); // activamos la moneda, porque en el array viene descativada

		return moneda; // devolvemos la moneda
	}

	// este metodo como dice su nombre es devolver la moneda a la array para que este guardada
	public void DevolverMoneda(GameObject moneda) 
	{

		moneda.SetActive(false); // setea la moneda a false para que no se pueda ver ni ahcer nada
		monedasGuardadas.Add(moneda); // añade a la moneda a la array.
	}
}
