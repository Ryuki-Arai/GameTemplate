using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 横長端末の場合の画面幅自動リサイズスクリプト
///
/// 使用方法
/// ・InspectorからCanvasScalerの「UI Scale Mode」を「Scale With Screen Size」に設定
/// ・「Screen Match Mode」を「Match Width Or Height」に設定
/// ・Canvasに本スクリプトをAddComponent
///
/// 参考）https://docs.unity3d.com/ja/current/ScriptReference/UI.CanvasScaler-matchWidthOrHeight.html
/// </summary>
[RequireComponent(typeof(CanvasScaler))]
public class WideScreenResize : MonoBehaviour
{
    private float WideScreenJudgeRate = 1136.0f / 640.0f;

    [SerializeField] private bool isAwake = true;

    void Awake()
    {
        if (isAwake)
        {
            Initialize();
        }
    }

    public void Initialize()
    {
        var scaler = GetComponent<CanvasScaler>();
        if (scaler != null)
        {
            var screenRate = (float)Screen.height / (float)Screen.width;
            if (screenRate < WideScreenJudgeRate)
            {
                scaler.matchWidthOrHeight = 1f;
            }
        }
        else
        {
            Debug.LogWarning(gameObject.name + "にCanvasScalerがアタッチされてません");
        }
    }
}