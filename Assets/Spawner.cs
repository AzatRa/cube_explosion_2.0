using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    private float _halver = 2f;

    public void Spawn(Vector3 position, Vector3 scale, int count, float mass)
    {
        Vector3 newScale = scale / _halver;

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new(
                Random.Range(-newScale.x / _halver, newScale.x / _halver),
                Random.Range(-newScale.y / _halver, newScale.y / _halver),
                Random.Range(-newScale.z / _halver, newScale.z / _halver)
            );

            GameObject newCube = Instantiate(_cubePrefab, position + offset, Quaternion.identity);
            newCube.transform.localScale = newScale;

            Cube cube = newCube.GetComponent<Cube>();

            if (cube != null)
            {
                float newMass = mass / _halver;
                cube.SetMass(newMass);

                Color color = new(Random.value, Random.value, Random.value);
                cube.ChangeColor(color);
            }
        }
    }
}
