using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public static class LotteryAlgorithm
{
    static int min = 1, max = 100;
    static int resultCountPerOneShot = 1;
    static bool isAllowRepeat = false;
    static List<int> samplesRaw = new List<int>();
    static List<int> samplesProcessed = new List<int>();

    // parram: resultCountPerOneShot <From 1 To 5>
    public static void SetUp(int _min, int _max, int _resultCountPerOneShot, bool _isAllowRepeat)
    {
        min = _min;
        max = _max;
        resultCountPerOneShot = Mathf.Clamp(_resultCountPerOneShot, 0, 5);
        isAllowRepeat = _isAllowRepeat;
    }

    public static void Reset()
    {
        samplesRaw.Clear();
        samplesProcessed.Clear();
        for (int i = min; i <= max; i++) samplesRaw.Add(i);
    }

    public static List<int> Draw()
    {
        var result = new List<int>();
        if (samplesRaw.Count == 0) return result;

        int resultCount = Mathf.Min(samplesRaw.Count, resultCountPerOneShot);

        List<int> exactlyIdxes = GetNonRepeatIdxesOfList(resultCount, samplesRaw);

        for (int i = 0; i < exactlyIdxes.Count; i++)
        {
            int numIdx = exactlyIdxes[i];
            result.Add(samplesRaw[numIdx]);
            if (!isAllowRepeat) samplesProcessed.Add(samplesRaw[numIdx]);
        }

        if (!isAllowRepeat)
        {
            // Decrese sort for avoid overflow when remove element of tagrgt list 
            exactlyIdxes.Sort();
            exactlyIdxes.Reverse();

            for (int i = 0; i < exactlyIdxes.Count; i++)
                samplesRaw.RemoveAt(exactlyIdxes[i]);
        }

        return result;
    }

    static List<int> GetNonRepeatIdxesOfList(int qty, List<int> source)
    {
        var result = new List<int>();
        if (source == null) { Debug.Log("[Error] Source can't be NULL."); return result; }

        qty = Mathf.Clamp(qty, 0, source.Count);

        List<int> idxesOfSource = new List<int>();
        for (int i = 0; i < source.Count; i++) idxesOfSource.Add(i);

        for (int i = 0; i < qty; i++)
        {
            int idx = Random.Range(0, idxesOfSource.Count);
            result.Add(idxesOfSource[idx]);
            idxesOfSource.RemoveAt(idx);
        }

        return result;
    }

}


