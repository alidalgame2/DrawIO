using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DayElementUI : MonoBehaviour
{
    public TextMeshProUGUI m_AmountText;
    public Image m_BgImage;
    public GameObject m_PointerImage;

    public void MakeTheDay(Color _DayColor)
    {
        m_BgImage.color = _DayColor;
        m_PointerImage.gameObject.SetActive(true);
    }
}
