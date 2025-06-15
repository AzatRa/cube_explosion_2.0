using UnityEngine;

public class Spreader : MonoBehaviour
{
    public void Spread(Cube cube, float force)
    {
        Rigidbody rigidbody = cube.GetComponent<Rigidbody>();

        Vector3 direction = (cube.transform.position - transform.position).normalized;
        rigidbody.AddForce(direction * force + Vector3.up * 1f, ForceMode.Impulse);
    }
}