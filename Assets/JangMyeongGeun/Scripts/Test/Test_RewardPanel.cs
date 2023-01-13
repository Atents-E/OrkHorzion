using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_RewardPanel : MonoBehaviour
{
    RewardPanel rewardPanel;
    Button open;
    Button close;
    Button setReward;

    private void Awake()
    {
        open = transform.GetChild(0).GetComponent<Button>();
        close = transform.GetChild(1).GetComponent<Button>();
        setReward = transform.GetChild(2).GetComponent<Button>();
    }

    void Start()
    {
        rewardPanel = GameManager.Inst.RewardPanel;

        open.onClick.AddListener(rewardPanel.Open);
        close.onClick.AddListener(rewardPanel.Close);
        setReward.onClick.AddListener(rewardPanel.SetReward);
    }

}
