using UnityEngine;

public class Exploder : MonoBehaviour
{
    public void Explode(Vector3 explosionCenter, float force, float explosionRadius, Vector3 scale)
    {
        float dividerForMediumSize = 3f;

        float scaleFactor = (scale.x + scale.y + scale.z) / dividerForMediumSize;
        float explosionRadiusForObject = explosionRadius * (1f / scaleFactor);
        float maxForce = force * (1f / scaleFactor);

        Collider[] overlappedColliders = Physics.OverlapSphere(explosionCenter, explosionRadius);

        foreach (var collider in overlappedColliders)
        {
            Rigidbody rigidbody = collider.attachedRigidbody;

            if (rigidbody != null)
            {
                float normalizedDistance = Mathf.Clamp01(Vector3Extensions.SqrDistance(explosionCenter, rigidbody.position) / explosionRadiusForObject);
                float forceForObject = Mathf.Lerp(maxForce, 0, normalizedDistance);

                Vector3 direction = (rigidbody.transform.position - explosionCenter).normalized;
                rigidbody.AddForce(direction * forceForObject + Vector3.up * 1f, ForceMode.Impulse);
            }
        }
    }
}