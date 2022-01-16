using UnityEngine;

public class AlternativeMainMenu : MonoBehaviour
{
    public GameObject m_MainMenu;
    public GameObject s_SkinSelectionMenu;
    public GameObject m_SkinSelectionButtonPrefab;
    public Transform m_SkinSelectionButtonParent;

    public void Start()
    {
        int skinCount = GameManager.Instance.m_Skins.Count;
        int indexCount = 0;
        for (int i = 0; i < skinCount; i++)
        {
            int colorCount = GameManager.Instance.m_Skins[i].Color.m_Colors.Count;
            for (int z = 0; z < colorCount; z++)
            {
                PreviewButton skinSelectionButton = Instantiate(m_SkinSelectionButtonPrefab, m_SkinSelectionButtonParent).GetComponent<PreviewButton>();
                skinSelectionButton.Init(indexCount++, GameManager.Instance.m_Skins[i].skinSprites[z]);
            }
        }
    }
    public void HandleBrushSelectButton()
    {
        m_MainMenu.SetActive(false);
        s_SkinSelectionMenu.SetActive(true);
    }

    public void HandleBackMainMenu()
    {
        m_MainMenu.SetActive(true);
        s_SkinSelectionMenu.SetActive(false);
    }
}
