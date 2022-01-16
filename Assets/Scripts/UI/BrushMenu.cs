using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushMenu : MonoBehaviour {

    public List<GameObject> m_BrushParts;

    //OPTIMIZATION HINT: This can be using renderer list only to avoid using get component and set a material instead of a color to improve graphical performance
    public void SetNewColor(Color _Color)
    {
        for (int i = 0; i < m_BrushParts.Count; i++)
        {
            m_BrushParts[i].GetComponent<Renderer>().material.color = _Color;
        }
    }
}
