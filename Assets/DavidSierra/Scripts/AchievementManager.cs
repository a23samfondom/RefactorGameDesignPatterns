using UnityEngine; 

public class AchievementManager : MonoBehaviour
{
    [SerializeField] NotificationManager notificationManager; //Referencia al script del NotificationManager

    private int enemiesKilled = 0; //Cuenta las muertes de los enemigos
    private bool killAchievementUnlocked = false; //Evita que el logro se repita

    

    private void OnEnable() //se suscribe al aviso de muerte de los enemigos.
    {
        HealthController.EnemyDied += RegisterEnemyKill; 
    }

    private void OnDisable() //cancela la suscripción para evitar mantener conexiones cuando el objeto se desactiva.
    {
        HealthController.EnemyDied -= RegisterEnemyKill;
    }

    private void RegisterEnemyKill() 
    {
        enemiesKilled++; //suma muerte

        if (enemiesKilled >= 3 && !killAchievementUnlocked) //comprueba si hay que desbloquear el logro.
        {
            killAchievementUnlocked = true;
            notificationManager.AddNotification("Logro desbloqueado: Matador!", 3f);
        }
    }
}
