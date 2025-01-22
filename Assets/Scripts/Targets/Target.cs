using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    private bool _isActive = false;
    [SerializeField] private Material[] _materials;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = _materials[0];
    }
    

    public void SetActive()
    {
        _isActive = !_isActive;
        if (_isActive)
        {
            _meshRenderer.material = _materials[1];
        }
        else if (!_isActive)
        {
            _meshRenderer.material = _materials[0];
        }
    }
}
