using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBrushMainMenu : SingletonMB<NewBrushMainMenu>
{
    public Transform m_BrushParent;

    private List<GameObject> m_brushesList = new List<GameObject>();
    private GameObject m_LastBrush;

    void Start()
    {
        Destroy(m_BrushParent.GetChild(0).gameObject);
        for (int i = 0; i < GameManager.Instance.m_Skins.Count; i++)
        {
            Debug.Log("Brushes created");
            CreateBrush(GameManager.Instance.m_Skins[i]);
        }
    }

    public void CreateBrush(SkinData _skin)
    {
        for (int i = 0; i < _skin.Color.m_Colors.Count; i++)
        {
            Debug.Log("Skin color set");
            Brush brush = Instantiate(_skin.Brush.m_Prefab).GetComponent<Brush>();

            brush.transform.SetParent(m_BrushParent);
            brush.transform.localScale = Vector3.one;
            brush.transform.localPosition = Vector3.zero;
            brush.transform.localRotation = Quaternion.identity;
            foreach (Renderer renderer in brush.m_Renderers)
            {
                renderer.material.color = _skin.Color.m_Colors[i];
            }
            m_brushesList.Add(brush.gameObject);
            brush.gameObject.SetActive(false);
        }
        m_LastBrush = m_brushesList[0];
        m_LastBrush.SetActive(true);
    }

    public void SelectBrush(int brushId)
    {
        if(m_LastBrush != null)
        {
            m_LastBrush.SetActive(false);
        }
        m_LastBrush = m_brushesList[brushId];
        m_LastBrush.SetActive(true);
    }
}
