using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    protected static GameManager _GM { get { return GameManager.INSTANCE; } }
    protected static EnemyManager _EM { get { return EnemyManager.INSTANCE; } }

    /// <summary>
    /// Scales all objects in a list to a new scales
    /// </summary>
    /// <param name="_objects">The list of objects</param>
    /// <param name="_Scale">The new scale</param>
    public void ScaleObjects(List<GameObject> _objects, Vector3 _Scale)
    {
        for(int i = 0; i < _objects.Count; i++)
        {
            _objects[i].transform.localScale = _Scale;
        }
    } 
    
    /// <summary>
    /// Scales all objects in a list to a new scales
    /// </summary>
    /// <param name="_objects">The list of objects</param>
    /// <param name="_Scale">The new scale</param>
    public void ScaleObjects(GameObject[] _objects, Vector3 _Scale)
    {
        for(int i = 0; i < _objects.Length; i++)
        {
            _objects[i].transform.localScale = _Scale;
        }
    }
    /// <summary>
    /// Scales all objects in a list to a new scales
    /// </summary>
    /// <param name="_objects">The object we want to scale</param>
    /// <param name="_Scale">The new scale</param>
    public void ScaleObject(GameObject _object, Vector3 _Scale)
    {
        _object.transform.localScale = _Scale;
    }
}
