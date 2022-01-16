using UnityEngine;
using TMPro;
public class AlternativeMainMenu : SingletonMB<AlternativeMainMenu>
{
    public GameObject m_MainMenu;
    public GameObject s_SkinSelectionMenu;
    public GameObject m_SkinSelectionButtonPrefab;
    public Transform m_SkinSelectionButtonParent;
    public SpriteTypeFillPanel spriteTypeFillPanel;
    public TextMeshProUGUI buttonIdText;

    public void Start()
    {
        int skinCount = GameManager.Instance.m_Skins.Count;
        int buttonId = 0;
        for (int i = 0; i < skinCount; i++)
        {
            int colorCount = GameManager.Instance.m_Skins[i].Color.m_Colors.Count;
            for (int z = 0; z < colorCount; z++)
            {
                BrushSelectionButton skinSelectionButton = Instantiate(m_SkinSelectionButtonPrefab, m_SkinSelectionButtonParent).GetComponent<BrushSelectionButton>();
                skinSelectionButton.Init(buttonId++, i,z, GameManager.Instance.m_Skins[i].skinSprites[z]);
            }
        }
        spriteTypeFillPanel.maxValue = buttonId;
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

    public void SelectSkin(int _ButtonId, int _SkinId, int _ColorId)
    {
        GameManager.Instance.m_PlayerSkinID = _SkinId;
        GameManager.Instance.HumanColor = GameManager.Instance.m_Skins[_SkinId].Color.m_Colors[_ColorId];
        spriteTypeFillPanel.SetFill(_ButtonId + 1);
        buttonIdText.text = (_ButtonId + 1) +"/" + spriteTypeFillPanel.maxValue;
    }
}
