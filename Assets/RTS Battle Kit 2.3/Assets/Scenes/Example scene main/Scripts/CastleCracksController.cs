using System;
using System.Linq;
using UnityEngine;

public class CastleCracksController : MonoBehaviour
{
    private Material _cracksMaterial;
    private Material _currentMaterial;
    private Castle _castle;

    private void Awake()
    {
        _castle = GetComponent<Castle>();
        _cracksMaterial = Resources.Load<Material>("CracksWall");
        var meshRenderer = GetComponent<MeshRenderer>();
        var materialsList = new System.Collections.Generic.List<Material>(meshRenderer.materials);
        _currentMaterial = new Material(_cracksMaterial);
        materialsList.Add(_currentMaterial);
        meshRenderer.materials = materialsList.ToArray();
        _currentMaterial.SetFloat("_HealthMax", _castle.lives);
    }

    private void FixedUpdate()
    {
        _currentMaterial.SetFloat("_Health", _castle.lives);
    }
}
