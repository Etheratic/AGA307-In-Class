using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    protected static GameManager _GM { get { return GameManager.INSTANCE; } }
    protected static EnemyManager _EM { get { return EnemyManager.INSTANCE; } }
    protected static UIManager _UI { get { return UIManager.INSTANCE; } }
    protected static PlayerMovement _PLAYER { get { return PlayerMovement.INSTANCE; } }



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

    /// <summary>
    /// Maps a value from one range to another
    /// </summary>
    /// <returns>The mapped value</returns>
    /// <param name="value">The input Value.</param>
    /// <param name="inMin">Input min</param>
    /// <param name="inMax">Input max</param>
    /// <param name="outMin">Output min</param>
    /// <param name="outMax">Output max</param>
    /// <param name="clamp">Clamp output value to outMin..outMax</param>
    static public float Map(float value, float inMin, float inMax, float outMin, float outMax, bool clamp = true)
    {
        float f = ((value - inMin) / (inMax - inMin));
        float d = (outMin <= outMax ? (outMax - outMin) : -(outMin - outMax));
        float v = (outMin + d * f);
        if (clamp) v = ClampSmart(v, outMin, outMax);
        return v;
    }
    /// <summary>
    /// Maps a value from 0f..1f to another range
    /// </summary>
    /// <returns>The mapped value</returns>
    /// <param name="value">The input Value.</param>
    /// <param name="outMin">Output min</param>
    /// <param name="outMax">Output max</param>
    /// <param name="clamp">Clamp output value to outMin..outMax</param>
    static public float MapFrom01(float value, float outMin, float outMax, bool clamp = true)
    {
        return Map(value, 0f, 1f, outMin, outMax, clamp);
    }
    /// <summary>
    /// Maps a value from a range to 0f..1f
    /// </summary>
    /// <returns>The mapped value</returns>
    /// <param name="value">The input Value.</param>
    /// <param name="inMin">Input min</param>
    /// <param name="inMax">Input max</param>
    /// <param name="clamp">Clamp output value to 0f..1f</param>
    static public float MapTo01(float value, float inMin, float inMax, bool clamp = true)
    {
        return Map(value, inMin, inMax, 0f, 1f, clamp);
    }
    /// <summary>
    /// Clamps a value, swapping min/max if necessary
    /// </summary>
    /// <returns>The smart.</returns>
    /// <param name="value">The value to clamp</param>
    /// <param name="min">Min output value</param>
    /// <param name="max">Max output value</param>
    static public float ClampSmart(float value, float min, float max)
    {
        if (min < max)
            return Mathf.Clamp(value, min, max);
        if (max < min)
            return Mathf.Clamp(value, max, min);
        return value;
    }
}
