using UnityEngine;

[RequireComponent(typeof(MeshFilter))][RequireComponent(typeof(MeshRenderer))]
public class MeshGeneration : MonoBehaviour
{
    [SerializeField, Range(3, 64)] private int numberOfSegments = 3;
    
}
