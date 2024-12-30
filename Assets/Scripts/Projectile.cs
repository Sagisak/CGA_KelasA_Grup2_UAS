using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    private Game game;

    void Start()
    {
        game = FindObjectOfType<Game>();
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        Vector3 targetPosition = new Vector3(target.position.x, 1.5f, target.position.z);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        // Destroy the projectile
        Destroy(gameObject);

        // Destroy the target (ant) and increase money
        if (target != null)
        {
            Destroy(target.gameObject);
            game.IncreaseMoney(5);
        }
    }
}