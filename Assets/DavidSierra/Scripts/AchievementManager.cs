using UnityEngine; 

public class AchievementManager : MonoBehaviour
{
    [SerializeField] NotificationManager notificationManager; //Referencia al script del NotificationManager

    /*PARA LOGRO DE ENEMIGOS*/
    private int enemiesKilled = 0; //Cuenta las muertes de los enemigos
    private bool killAchievementUnlocked = false; //Evita que el logro se repita

    /*PARA LOGRO DE MONEDAS*/
    private int coinsCollected = 0; //Cuenta las monedas recogidas
    private bool coinAchievementUnblocked = false; //Evita que el logro se repita

    /*PARA LOGRO DE SALTOS*/
    private int jumpsCount = 0; //Contador de saltos
    private bool jumpAchievementUnlocked = false; //Evita que el logro se repita


    private void OnEnable() 
    {
        HealthController.EnemyDied += RegisterEnemyKill; //se suscribe al aviso de muerte de los enemigos.
        PlayerController.CoinCollected += RegisterCoinCollected; //Se suscribe al aviso de recogida de monedas
        PlayerController.PlayerJumped += RegisterJump; //Se suscribe al evento para saber cuando salta
    }

    private void OnDisable() 
    {
        HealthController.EnemyDied -= RegisterEnemyKill; //cancela la suscripción para evitar mantener conexiones cuando el objeto se desactiva.
        PlayerController.CoinCollected -= RegisterCoinCollected; //cancela suscripción cuando ya las ha recogido
        PlayerController.PlayerJumped -= RegisterJump; //cancela suscripción cuando ya ha completado los saltos
    }

    private void RegisterEnemyKill() //REGISTRO DE ENEMIGOS MUERTOS
    {
        enemiesKilled++; //suma muerte

        if (enemiesKilled >= 3 && !killAchievementUnlocked) //comprueba si hay que desbloquear el logro.
        {
            killAchievementUnlocked = true; //logro desbloqueado
            notificationManager.AddNotification("Logro desbloqueado: Matador!", 3f); //mensaje que muestra por pantalla
        }
    }

    private void RegisterCoinCollected() //REGISTRO DE MONEDAS RECOGIDAS
    {
        coinsCollected++;

        if(coinsCollected >= 5 && !coinAchievementUnblocked)
        {
            coinAchievementUnblocked = true; //logro desbloqueado
            notificationManager.AddNotification("Logro desbloqueado: Recolector!", 3f);//mensaje que muestra por pantalla   
        }
    }

    private void RegisterJump() //REGISTRO DE SALTOS
    {
        jumpsCount++; //suma contador de saltos

        if(jumpsCount >= 10 && !jumpAchievementUnlocked)
        {
            jumpAchievementUnlocked = true;//logro desbloqueado
            notificationManager.AddNotification("Logro desbloqueado: Saltador!", 3f);//mensaje que muestra por pantalla
        }
    }

    // un metodo get, este devuelve la cantidad de monedas que tiene el player
    public int GetCoinsCollected(){

        return coinsCollected; // devuelve la cantidad de monedas que tiene el player

	}
}
