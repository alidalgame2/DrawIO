using UnityEngine;
using TMPro;

public class DailyRewardsMenu : MonoBehaviour
{
    public DayElementUI[] m_DayElements;
    public TextMeshProUGUI m_TotalCoinText;
    
    public void Init(int _TotalCoin, int[] _Amounts)
    {
        for (int i = 0; i < _Amounts.Length && i < m_DayElements.Length; i++)
        {
            m_DayElements[i].m_AmountText.text = _Amounts + "";
        }
        m_TotalCoinText.text = _TotalCoin + "";
    }

    public void HandleClaimButton()
    {

    }
}
