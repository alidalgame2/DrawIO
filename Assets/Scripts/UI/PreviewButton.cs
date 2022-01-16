using UnityEngine;
using UnityEngine.UI;

public class PreviewButton : MonoBehaviour
{
    public Image m_ButtonImage; 
    private int m_buttonId;

    public void Init(int _ButtonId, Sprite _ButtonSprite)
    {
        m_buttonId = _ButtonId;
        m_ButtonImage.sprite = _ButtonSprite;
    }

    public void HandlePreviewButton()
    {
        MainMenuView.Instance.ChangeBrush(m_buttonId);
    }
}
