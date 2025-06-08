using UnityEngine;

public class ExplodeEffector : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffectPrefab;

    public void Explode(Vector3 position)
    {
        if (_explosionEffectPrefab != null)
        {
            ParticleSystem particleSystem = Instantiate(_explosionEffectPrefab, position, Quaternion.identity);
            particleSystem.Play();
            Destroy(particleSystem.gameObject, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
        }
    }
}