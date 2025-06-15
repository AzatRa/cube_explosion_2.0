using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private float _halver = 2f;

    public void Spawn(Vector3 position, Vector3 scale, int count, float mass, float splitChance)
    {
        splitChance /= _halver;

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new(
                Random.Range(-scale.x / _halver, scale.x / _halver),
                Random.Range(-scale.y / _halver, scale.y / _halver),
                Random.Range(-scale.z / _halver, scale.z / _halver)
            );

            Cube newCube = Instantiate(_cubePrefab, position + offset, Quaternion.identity);
            newCube.transform.localScale = scale;

            Cube cube = newCube.GetComponent<Cube>();

            if (newCube.TryGetComponent<Cube>(out _))
            {
                float newMass = mass / _halver;
                cube.SetMass(newMass);

                Color color = new(Random.value, Random.value, Random.value);
                cube.ChangeColor(color);

                cube.SetSplitChance(splitChance);
            }
        }
    }
}


