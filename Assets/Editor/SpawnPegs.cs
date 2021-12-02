using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnPegs : MonoBehaviour
{
    private static GameObject _peg = (GameObject)Resources.Load("Peg");
    private static GameObject _pegs = new GameObject("Pegs");

    private static float _xStart = -5.95f;
    private static float _xEnd = 0.95f;
    private static int _xAmount = 10;
    private static float _xSpacing;

    private static float _yStart = 2.5f;
    private static float _yEnd = -3.45f;
    private static int _yAmount = 14;
    private static float _ySpacing;

    [MenuItem("Custom Scripts/Spawn Pegs")]
    private static void Create()
    {
        if (_peg != null)
        {
            _pegs.transform.SetParent(GameObject.FindWithTag("Board").transform);
        }
        
        _xSpacing = (_xEnd - _xStart) / (_xAmount - 1);
        _ySpacing = (_yEnd - _yStart) / (_yAmount - 1);

        for (int yCount = 0 ;yCount < _yAmount; yCount++)
        {
            if (yCount % 2 == 0) // even pass
            {
                for (int xCount = 0; xCount < _xAmount; xCount++)
                {
                    SpawnPeg(xCount, yCount);
                }
            }
            else // odd pass
            {
                for (int xCount = 0; xCount < _xAmount - 1; xCount++) // one less on each odd row
                {
                    SpawnPeg(xCount, yCount, true);
                }
            }
        }

        Undo.RegisterCreatedObjectUndo(_pegs, "Spawn pegs");
    }

    private static void SpawnPeg(int xCount, int yCount, bool oddOffset = false)
    {
        GameObject gameObject = Instantiate(
            _peg,
            new Vector3(
                _xStart + xCount * _xSpacing + (oddOffset ? _xSpacing / 2 : 0.0f),
                _yStart + yCount * _ySpacing,
                0.0f
            ),
            Quaternion.identity
        );

        gameObject.transform.SetParent(_pegs.transform);
    }
}