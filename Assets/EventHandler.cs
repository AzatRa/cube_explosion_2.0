using UnityEngine;
using UnityEngine.UIElements;

public class EventHandler : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Spreader _spreader;
    [SerializeField] private ExplodeEffector _explodeEffector;

    [SerializeField] private int _minNewCubes = 2;
    [SerializeField] private int _maxNewCubes = 6;

    [SerializeField] public float _explosionRadius = 10f;
    [SerializeField] private float _minExplosionForce = 100f;
    [SerializeField] private float _maxExplosionForce = 500f;

    //private float _splitChance = 1f;
    private int _halver = 2;
    
    public void Hit(Cube cube)
    {
        float random = Random.value;
        float randomForce = Random.Range(_minExplosionForce, _maxExplosionForce + 1);

        Vector3 explosionCenter = cube.transform.position;
        Vector3 newScale = cube.transform.localScale / _halver;
        float mass = cube.GetComponent<Rigidbody>().mass;

        if (random <= cube.SplitChance)
        {
            int count = Random.Range(_minNewCubes, _maxNewCubes + 1);

            if (_spawner != null)
                _spawner.Spawn(explosionCenter, newScale, count, mass, cube.SplitChance);

            if (_spreader != null && cube.TryGetComponent(out Rigidbody _))
                _spreader.Spread(cube, randomForce);
        }
        else 
        {
            if (_exploder != null && cube.TryGetComponent(out Rigidbody _) && _explodeEffector != null)
            {
                _exploder.Explode(explosionCenter, randomForce, _explosionRadius, newScale);
                _explodeEffector.Explode(explosionCenter);
            }
        }

        Destroy(cube.gameObject);
    }
}
