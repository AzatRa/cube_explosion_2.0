using UnityEngine;
using UnityEngine.UIElements;

public class Exploder : MonoBehaviour
{
    public void Explode(Rigidbody rigidbody, Vector3 explosionCenter, float minForce, float maxForce, float explosionRadius, Vector3 scale)
    {
        float dividerForMediumSize = 3f;
        float randomForce = Random.Range(minForce, maxForce + 1);

        float scaleFactor = (scale.x + scale.y + scale.z) / dividerForMediumSize; // средний масштаб куба
        float explosionRadiusForObject = explosionRadius * (1f / scaleFactor); // чем меньше масштаб Ч тем больше радиус
        float maxForceForObject = randomForce * (1f / scaleFactor); //чем меньше масштаб - тем больше сила
        Debug.Log($"scaleFactor = {scaleFactor}, explosionRadiusForObject = {explosionRadiusForObject}, maxForceForObject = {maxForceForObject}");

        Collider[] overlappedColliders = Physics.OverlapSphere(explosionCenter, explosionRadius);

        foreach (var collider in overlappedColliders)
        {
            rigidbody = collider.attachedRigidbody;

            if (rigidbody != null)
            {
                float distance = Vector3.Distance(rigidbody.position, explosionCenter);
                float normalizedDistance = Mathf.Clamp01(distance / explosionRadiusForObject);
                float force = Mathf.Lerp(maxForceForObject, 0, normalizedDistance);

                Vector3 direction = (rigidbody.transform.position - explosionCenter).normalized;
                rigidbody.AddForce(direction * force + Vector3.up * 1f, ForceMode.Impulse);
            }
        }
    }
}