using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
{
    [SerializeField] private Image fadePanel;

    private CancellationTokenSource token;

    private const float fadeSpeed = 0.25f;

    public void ChangeScene(string sceneName)
    {
        DataSaveUtility.I.Save();
        token?.Cancel();
        token = new CancellationTokenSource();
        PlayChangeScene(sceneName, token.Token).Forget();
    }
    private async UniTask PlayChangeScene(string sceneNam, CancellationToken token)
    {
        SetActiveFadeImage(true);
        await fadePanel.DOFade(1, fadeSpeed)
            .SetEase(Ease.Linear)
            .ToUniTask(cancellationToken: token);

        DOTween.Clear(true);
        SceneManager.LoadScene(sceneNam);

        await UniTask.NextFrame(token);
        await fadePanel.DOFade(0, fadeSpeed)
            .SetEase(Ease.Linear)
            .ToUniTask(cancellationToken: token);

        SetActiveFadeImage(false);
    }

    private void SetActiveFadeImage(bool isActive)
    {
        var color = fadePanel.color;
        color.a = isActive ? 0 : 1;
        fadePanel.color = color;
        fadePanel.gameObject.SetActive(isActive);
    }
}
