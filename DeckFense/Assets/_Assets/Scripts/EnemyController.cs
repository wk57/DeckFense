using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Transform[] pathPoints; // Se asigna desde otro script
    public float speed = 2f;
    public float maxHealth = 100f;
    private float currentHealth;
    private float originalSpeed;
    private float currentSpeed;
    private bool isSlowed = false;


    private int currentPoint = 0;

    private void Start()
    {
        currentHealth = maxHealth;
        currentHealth = maxHealth;
        originalSpeed = speed; // usa tu variable de velocidad original
        currentSpeed = originalSpeed;
    }

    void Update()
    {
        if (currentPoint >= pathPoints.Length) return;

        transform.position = Vector3.MoveTowards(transform.position, pathPoints[currentPoint].position, currentSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pathPoints[currentPoint].position) < 0.1f)
        {
            currentPoint++;
        }

        if (currentPoint >= pathPoints.Length)
        {
            GameManager.Instance.PlayerTakeDamage(1);
            Destroy(gameObject);
        }
    }


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Efecto de muerte o partículas (opcional)
        Debug.Log(gameObject.name + " ha muerto.");

        // Aquí podrías devolverlo al pool más adelante
        Destroy(gameObject);
    }

    public void ApplySlow(float slowFactor, float duration)
    {
        if (isSlowed) return; // no acumular ralentización

        isSlowed = true;
        currentSpeed *= slowFactor;

        Debug.Log(gameObject.name + " está ralentizado por " + duration + " segundos");

        StartCoroutine(RemoveSlowAfterDelay(duration));
    }

    private IEnumerator RemoveSlowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentSpeed = originalSpeed;
        isSlowed = false;
    }


}
