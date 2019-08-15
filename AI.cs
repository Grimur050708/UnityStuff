using UnityEngine;
using System.Collections;

[CreateAssetMenu()]
public class AI : ScriptableObject
{
    public float m_minDistanceToPlayer = 5;
    public float m_maxDistanceToPlayer = 5;
}