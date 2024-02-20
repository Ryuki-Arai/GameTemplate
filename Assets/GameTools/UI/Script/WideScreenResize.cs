using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����[���̏ꍇ�̉�ʕ��������T�C�Y�X�N���v�g
///
/// �g�p���@
/// �EInspector����CanvasScaler�́uUI Scale Mode�v���uScale With Screen Size�v�ɐݒ�
/// �E�uScreen Match Mode�v���uMatch Width Or Height�v�ɐݒ�
/// �ECanvas�ɖ{�X�N���v�g��AddComponent
///
/// �Q�l�jhttps://docs.unity3d.com/ja/current/ScriptReference/UI.CanvasScaler-matchWidthOrHeight.html
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
            Debug.LogWarning(gameObject.name + "��CanvasScaler���A�^�b�`����Ă܂���");
        }
    }
}