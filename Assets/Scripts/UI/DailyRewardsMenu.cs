using UnityEngine;
using TMPro;
using System.Collections;

public class DailyRewardsMenu : MonoBehaviour
{
    public DayElementUI[] m_DayElements;
    public TextMeshProUGUI m_TotalCoinText;
    public Color m_DayColor;

    private int m_currentCoin;
    
  
    public void Init(int _TotalCoin, int[] _Amounts)
    {
        for (int i = 0; i < _Amounts.Length && i < m_DayElements.Length; i++)
        {
            m_DayElements[i].m_AmountText.text = _Amounts[i] + "";
        }
        m_currentCoin = _TotalCoin;
        m_TotalCoinText.text = _TotalCoin + "";
    }

    public void SetTheDay(int day)
    {
        day = Mathf.Min(day, m_DayElements.Length - 1);
        m_DayElements[day].MakeTheDay(m_DayColor);
    }

    public void HandleClaimButton()
    {
        DailyRewardsManager.Instance.ClaimReward();
    }

    public void SetCoin(int _NextAmount)
    {
        StartCoroutine(CoinChangeRoutine(_NextAmount));
    }

    public IEnumerator CoinChangeRoutine(int nextAmount)
    {
        float lerpValue = 0;
        while (lerpValue < 1)
        {
            m_TotalCoinText.text = (int)Mathf.Lerp(m_currentCoin, nextAmount, lerpValue) + "";
            lerpValue += Time.deltaTime * 2f;
            yield return null;
        }
        m_TotalCoinText.text = nextAmount + "";
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
