using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Renderer _renderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    public void SetMass(float mass)
    {
        if (_rigidbody != null)
        {
            _rigidbody.mass = mass;
        }
    }

    public void ChangeColor()
    {
        if (_renderer != null)
        {
            _renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}