using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] pathPoints; // Se asigna desde otro script
    public float speed = 2f;

    private int currentPoint = 0;

    void Update()
    {
        if (currentPoint >= pathPoints.Length) return;

        transform.position = Vector3.MoveTowards(transform.position, pathPoints[currentPoint].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pathPoints[currentPoint].position) < 0.1f)
        {
            currentPoint++;
        }

        if (currentPoint >= pathPoints.Length)
        {
            // Llegó al final
            GameManager.Instance.PlayerTakeDamage(1); // Llama a GameManager para reducir salud
            Destroy(gameObject);
        }
    }
}
