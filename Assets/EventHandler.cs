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
    
    public void Hit(Cube cube)
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

            if (_exploder != null && cube.TryGetComponent(out Rigidbody rigidbody) && _explodeEffector != null)
            {
                _exploder.Explode(rigidbody, explosionCenter, _minExplosionForce, _maxExplosionForce, _explosionRadius, localScale);
                _explodeEffector.Explode(explosionCenter);
            }
        }
        else
        {
            if (_exploder != null && cube.TryGetComponent(out Rigidbody rigidbody) && _explodeEffector != null)
            {
                _exploder.Explode(rigidbody, explosionCenter, _minExplosionForce, _maxExplosionForce, _explosionRadius, localScale);
                _explodeEffector.Explode(explosionCenter);
            }
        }

        Destroy(cube.gameObject);
    }
}
