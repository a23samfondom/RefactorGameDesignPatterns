using TMPro;
using UnityEngine;

public class UICoinController : MonoBehaviour
{

	[SerializeField] private TMP_Text textoMonedas; //cogemos la referencia del texto donde se muestra la cantidad de monedas recogidas
	[SerializeField] private  AchievementManager achievementManager; // cogemos la referencia de achievementManager (lo de contar las monedas, podriamos cambiarlo al player controller)

 
    void Update()
    {
		textoMonedas.text = achievementManager.GetCoinsCollected().ToString();	// esto lo que hace es coger la cantidad de monedas que tiene el player, pasarlas al texto y las 
																				// pasa a String para poder poner la cantidad, porque el get la devuelve en int.
	}
}
