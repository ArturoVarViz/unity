using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent pathfinder;
    private Transform target;

    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        pathfinder.SetDestination(target.position);
        Debug.Log(target.position);

        // Si la posición del enemigo es igual a la posición del jugador, mostrar "Game Over"
        if (transform.position == target.position)
        {
            Debug.Log("Game Over - Enemy caught the player!");
            // Aquí puedes llamar a una función para manejar el "Game Over", como reiniciar el nivel
        }
    }
}
