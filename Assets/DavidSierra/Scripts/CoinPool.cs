using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
	[SerializeField] private GameObject coinPrefab; // el prefab de las coins
	[SerializeField] private int cantidadInicial = 10; // cantidad inicial que hay de monedas, serialize para poder modificar la cantidad desde el editor directamente

	private List<GameObject> monedasGuardadas = new List<GameObject>(); // vale esto es importante, esta es la array de las monedas que guarda el Pool, aqui estan
                                                                        //las monedas guardadas que se van creando.
	

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

    
    void Update()
    {
        
    }


    public void DevolverMoneda(){
        

    }
}
