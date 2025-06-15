using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private float _splitChance = 1f;

    public float SplitChance => _splitChance;

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

    public void ChangeColor(Color color)
    {
        if (_renderer != null)
        {
            _renderer.material.color = color;
        }
    }
    
    public void SetSplitChance(float splitChance)
    {
        _splitChance = splitChance;
    }
}