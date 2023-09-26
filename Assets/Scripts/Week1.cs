using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Week1 : MonoBehaviour
{
    public int numberOne = 1;
    public int numberTwo = 3;
    public int newNumber;
    public GameObject player;
    public Color newColor;
   
    void Start()
    {
        player.transform.position = new Vector3(numberTwo, numberOne, 1);
        player.GetComponent<Renderer>().material.color = newColor;
        AddNumbers();
        AddNumbers(7, 9);
        AddNumbers(numberTwo, numberOne);
        newNumber = AddNumbers2(7, 7);
    }

    int AddNumbers2(int _one, int _two)
    {
        return _one + _two;
    }
    void AddNumbers(int _one, int _two)
    {
        newNumber = _one + _two;
        print(newNumber);
    }
    private void AddNumbers()
    {
        newNumber = numberOne + numberTwo;
        print(newNumber);

    }
}
