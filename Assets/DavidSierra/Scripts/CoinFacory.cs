
using UnityEngine;

public class CoinFactory : MonoBehaviour
{
	[SerializeField] private GameObject coinPrefab; // prefab de las monedas

	public GameObject CreateCoin(Vector3 posicion) // metodo que crea mondeas
	{
		GameObject moneda = Instantiate(coinPrefab, posicion, Quaternion.identity); //instancia la moneda
		return moneda; //devuevle la moneda
	}
}
