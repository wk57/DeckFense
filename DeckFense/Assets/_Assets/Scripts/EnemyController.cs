using UnityEngine;
using System.Collections;


public class EnemyController : MonoBehaviour
{
    public System.Action onDeath;

    public Transform[] pathPoints; // Se asigna desde otro script
    public float speed = 2f;
    public float maxHealth = 100f;
    private float currentHealth;
    private float originalSpeed;
    private float currentSpeed;
    private bool isSlowed = false;

    public EnemyData data;

    private Animator animator;
    private bool isDying = false;


    private int currentPoint = 0;

    private void Start()
    {
        currentHealth = maxHealth;
        currentHealth = maxHealth;
        originalSpeed = speed; // usa tu variable de velocidad original
        currentSpeed = originalSpeed;
        animator = GetComponentInChildren<Animator>();
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
        if (isDying) return;
        isDying = true;

        GameManager.Instance.playerScore += data.scoreValue;
        GameManager.Instance.UpdateUI();
        onDeath?.Invoke();

        // Activar animaci�n de muerte
        if (animator != null)
        {
            animator.SetTrigger("Die");
            StartCoroutine(DeathSequence());
        }
        else
        {
            Destroy(gameObject); // Si no hay animador, destruir de inmediato
        }
    }

    private IEnumerator DeathSequence()
    {
        // Esperar un frame para asegurar que el Animator procese el trigger
        yield return null;

        // Esperar duraci�n real de la animaci�n (aj�stala seg�n tu clip)
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }


    public void ApplySlow(float slowFactor, float duration)
    {
        if (isSlowed) return; // no acumular ralentizaci�n

        isSlowed = true;
        currentSpeed *= slowFactor;

        Debug.Log(gameObject.name + " est� ralentizado por " + duration + " segundos");

        StartCoroutine(RemoveSlowAfterDelay(duration));
    }

    private IEnumerator RemoveSlowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentSpeed = originalSpeed;
        isSlowed = false;
    }


}
