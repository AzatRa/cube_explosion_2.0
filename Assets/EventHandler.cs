using System;
using System.Collections;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private ExplodeEffector _explodeEffector;

    [SerializeField] private int _minNewCubes = 2;
    [SerializeField] private int _maxNewCubes = 6;

    [SerializeField] public float _explosionRadius = 10f;
    [SerializeField] private float _minExplosionForce = 100f;
    [SerializeField] private float _maxExplosionForce = 500f;

    private float _splitChance = 1f;
    private int _halver = 2;
    
    private event Action<Cube> OnHit;

    private void OnEnable()
    {
        OnHit += HandleHit;
    }

    private void OnDisable()
    {
        OnHit -= HandleHit;
    }

    public void Hit(Cube cube)
    {
        OnHit?.Invoke(cube);
    }

    private void HandleHit(Cube cube)
    {
        StartCoroutine(HitCoroutine(cube));
    }

    private IEnumerator HitCoroutine(Cube cube)
    {
        float random = UnityEngine.Random.value;
        Debug.Log($"random={random}, splitChance={_splitChance}");

        Vector3 explosionCenter = cube.transform.position;
        Vector3 localScale = cube.transform.localScale;
        float mass = cube.GetComponent<Rigidbody>().mass;

        if (random <= _splitChance)
        {
            _splitChance /= _halver;

            if (_spawner != null && cube.ColorChanger != null)
                _spawner.Spawn(explosionCenter, localScale, _minNewCubes, _maxNewCubes, cube.ColorChanger, mass);

            yield return new WaitForSeconds(0.2f);

            if (_exploder != null && cube.TryGetComponent(out Rigidbody rigidbody) && _explodeEffector != null)
            {
                _exploder.Explode(rigidbody, _minExplosionForce, _maxExplosionForce);
                _explodeEffector.Explode(explosionCenter);
            }
        }
        else
        {
            if (_explodeEffector != null)
            {
                _exploder.ExplodeWithoutDivision(explosionCenter, _explosionRadius, _minExplosionForce, _maxExplosionForce, localScale);
                _explodeEffector.Explode(cube.transform.position);
            }
        }

        Destroy(cube.gameObject);
    }
}
