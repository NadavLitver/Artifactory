using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float speed;
    private Transform target;
    private void OnEnable()
    {
        target = GameManager.Instance.assets.playerActor.transform;
    }

    private void Update()
    {
        if (target == null)
        {
            // If the target is destroyed or not set, destroy the projectile
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        transform.rotation = rotation;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}