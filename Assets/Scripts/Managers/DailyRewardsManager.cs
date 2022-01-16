using System;
using System.Collections;
using UnityEngine;

public class DailyRewardsManager : SingletonMB<DailyRewardsManager>
{
    public DailyRewardsMenu m_DailyRewardsMenu;
    public int[] m_RewardDatabase;

    private int m_currentReward;
    private int coin;
    private int m_claimCount;
    private bool m_rewardClaimed;
    private DateTime m_lastClaimDate;

    void Awake()
    {
        if(OptionsManager.Instance.DailyRewardsActive)
        {
            int daysSinceLastClaim = 2;
            coin = PlayerPrefs.GetInt("Coin", 0);
            m_DailyRewardsMenu.Init(coin, m_RewardDatabase);
            m_currentReward = m_RewardDatabase[0];

            m_claimCount = PlayerPrefs.GetInt("ClaimCount", 0);
            if (PlayerPrefs.HasKey("LastClaimDate"))
            {
                string dateString = PlayerPrefs.GetString("LastClaimDate");
                DateTime.TryParse(dateString, out m_lastClaimDate);
                daysSinceLastClaim = (DateTime.Now - m_lastClaimDate).Days;
            }

            if (daysSinceLastClaim == 0)
            {
                m_currentReward = 0;
            }
            else if (daysSinceLastClaim != 0)
            {
                if (daysSinceLastClaim == 1)
                {
                    m_currentReward = m_RewardDatabase[Mathf.Min(m_claimCount, m_RewardDatabase.Length - 1)];
                }
                m_DailyRewardsMenu.SetTheDay(m_claimCount);
                m_DailyRewardsMenu.gameObject.SetActive(true);
            }
        }
    }

    public void ClaimReward()
    {
        if (!m_rewardClaimed)
        {
            coin += m_currentReward;
            PlayerPrefs.SetInt("Coin", coin);
            PlayerPrefs.SetInt("ClaimCount", m_claimCount + 1);
            PlayerPrefs.SetString("LastClaimDate",DateTime.Now.ToString());
            m_rewardClaimed = true;
            m_DailyRewardsMenu.SetCoin(coin);
        }
    }
}
