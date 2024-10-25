using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPManager : MonoBehaviour
{
    public Image mpBar; // MPバーのImage
    public float maxMP = 100f; // 最大MP
    public float currentMP; // 現在のMP
    public float recoveryRate = 5f; // 回復速度

    void Start()
    {
        currentMP = maxMP; // 初期MPを最大に設定
        UpdateMPBar(); // MPバーを更新
    }

    void Update()
    {
        // MPの回復
        if (currentMP < maxMP)
        {
            currentMP += recoveryRate * Time.deltaTime;
            currentMP = Mathf.Min(currentMP, maxMP); // 最大MPを超えないように
            UpdateMPBar();
        }
    }

    public void Attack()
    {
        float attackCost = 20f; // 攻撃に必要なMP
        if (currentMP >= attackCost)
        {
            currentMP -= attackCost; // MPを減少
            UpdateMPBar();
            // 攻撃の処理をここに追加
        }
        else
        {
            Debug.Log("MPが不足しています！");
        }
    }

    private void UpdateMPBar()
    {
        mpBar.fillAmount = currentMP / maxMP; // MPバーの表示を更新
    }
}