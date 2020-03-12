using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterConsole : MonoBehaviour
{
    public List<Text> textsAtNav;
    public GameObject infoPanel;
    public GameObject settingsPanel;
    public Lottery lottery;

    [Header("Settings")]
    public InputField minInp;
    public InputField maxInp;
    public InputField resultCountInp;
    public Toggle allowRepeatTG;

    void Start()
    {
        ToSettings();
        textsAtNav.ForEach(x => x.text = string.Empty);
    }

    void RefreshTextsAtNav(List<int> source)
    {
        for (int i = 0; i < textsAtNav.Count; i++)
        {
            if (i >= source.Count) textsAtNav[i].text = string.Empty;
            else textsAtNav[i].text = source[i].ToString();
        }
    }

    void RefreshTextsAtFloor(List<int> source)
    {
        for (int i = 0; i < lottery.txtsOnFloor.Count; i++)
        {
            if (i >= source.Count) lottery.txtsOnFloor[i].text = string.Empty;
            else lottery.txtsOnFloor[i].text = source[i].ToString();
        }
    }

    // BtnEvnt
    public void OnDraw()
    {
        lottery.StartAnimation(() =>
        {
            var result = LotteryAlgorithm.Draw();
            RefreshTextsAtNav(result);
            RefreshTextsAtFloor(result);
        });
    }

    // BtnEvnt
    public void OnResetLottery()
    {
        LotteryAlgorithm.Reset();
        textsAtNav.ForEach(x => x.text = string.Empty);
        lottery.txtsOnFloor.ForEach(x => x.text = string.Empty);
    }

    // BtnEvnt
    public void ToSettings()
    {
        settingsPanel.SetActive(true);
        infoPanel.SetActive(false);
    }

    // BtnEvnt
    public void ToInfoPanelAndApplySettings()
    {
        infoPanel.SetActive(true);
        settingsPanel.SetActive(false);
        ApplySettings();
    }

    void ApplySettings()
    {
        bool isMinPass = int.TryParse(minInp.text, out int min);
        bool isMaxPass = int.TryParse(maxInp.text, out int max);
        if (!isMinPass) min = 1;
        if (!isMaxPass) max = 100;
        if (min > max)
        {
            var tmp = min;
            min = max;
            max = tmp;
        }
        minInp.text = min.ToString();
        maxInp.text = max.ToString();

        bool isResultCountInpPass = int.TryParse(resultCountInp.text, out int resultCount);
        if (!isResultCountInpPass) resultCount = 1;
        resultCount = Mathf.Clamp(resultCount, 1, 5);
        resultCountInp.text = resultCount.ToString();

        LotteryAlgorithm.SetUp(min, max, resultCount, allowRepeatTG.isOn);
    }
}