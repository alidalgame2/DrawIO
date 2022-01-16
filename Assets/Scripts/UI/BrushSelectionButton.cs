using UnityEngine;
using UnityEngine.UI;

public class BrushSelectionButton : MonoBehaviour
{
    public Image m_ButtonImage; 

    private int m_skinId;
    private int m_colorId;
    private int m_buttonId;

    public void Init(int _ButtonId, int _SkinId, int _ColorId, Sprite _ButtonSprite)
    {
        m_skinId = _SkinId;
        m_colorId = _ColorId;
        m_buttonId = _ButtonId;
        m_ButtonImage.sprite = _ButtonSprite;
    }

    public void HandlePreviewButton()
    {
        AlternativeMainMenu.Instance.SelectSkin(m_buttonId, m_skinId, m_colorId);
        NewBrushMainMenu.Instance.SelectBrush(m_buttonId);
    }
}
