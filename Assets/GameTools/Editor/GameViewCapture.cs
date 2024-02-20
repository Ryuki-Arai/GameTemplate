using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Diagnostics;
using System.Collections;

public class GameViewCapture : EditorWindow
{

    //�f�B���N�g���̃p�X
    //�G�X�P�[�v�V�[�P���X�ɒ��Ӂi\ �� \\�j
    public static string directoryPath = "./ScreenShots";

    //�t�@�C���̖��O
    //�g���q�͕s�v
    public static string filename = "ScreenShot";

    //���t�����邩�ǂ���
    public static bool isYear = true;
    public static bool isMonth = true;
    public static bool isDate = true;
    public static bool isHour = true;
    public static bool isMinute = true;
    public static bool isSecond = true;

    //�B�e������Q�[�����~���邩�ǂ���
    public static bool isPausePlaying = true;

    //���O�����ĕۑ��̃p�l�����J�����ǂ���
    public static bool isOpenSaveFilePanel = true;

    //�G�N�X�v���[���[�ŊJ�����ǂ���
    public static bool isOpenExplorer = true;

    //�֘A�t����ꂽ�A�v���P�[�V�����ŊJ�����ǂ���
    public static bool isOpenApplication = false;

    //�o�͒��̃t�@�C���̃p�X
    public static string generatedFilePath = null;

    // �I�u�W�F�N�g�̋���Open�R�}���h
    // % (ctrl on Windows, cmd on macOS), # (shift), & (alt)
    [MenuItem("Tools/GameViewCapture &#c")]

    //�Q�[���r���[���L���v�`������
    static void Capture()
    {

        if (isPausePlaying)
        {
            //�Đ�����������
            if (EditorApplication.isPlaying)
            {
                //�Q�[�����~
                EditorApplication.isPaused = true;
            }
        }

        //�t�@�C�����o�͒���������
        if (generatedFilePath != null)
        {
            //�_�C�A���O��\��
            if (EditorUtility.DisplayDialog("�t�@�C�����o�͒��ł�", "�o�͒��̃t�@�C��" + generatedFilePath + "�͔j������܂��B" + "\n" + "�V�����t�@�C����ۑ����܂����H", "�ۑ�", "���~"))
            {
                //�o�͒�������
                generatedFilePath = null;
            }
            else
            {
                //�����𒆎~
                return;
            }
        }

        //���t��������쐬
        string time = "";
        if (isYear)
        {
            time += DateTime.Now.ToString("_yyyy");
        }
        if (isMonth)
        {
            time += DateTime.Now.ToString("_MM");
        }
        if (isDate)
        {
            time += DateTime.Now.ToString("_dd");
        }
        if (isHour)
        {
            time += DateTime.Now.ToString("_HH");
        }
        if (isMinute)
        {
            time += DateTime.Now.ToString("_mm");
        }
        if (isSecond)
        {
            time += DateTime.Now.ToString("_ss");
        }

        //�t�@�C���̃p�X
        string filePath = null;

        if (isOpenSaveFilePanel)
        {

            //�t�@�C���̕ۑ�����w��
            filePath = EditorUtility.SaveFilePanel("���O��t���ĕۑ�", "", filename + time, "png");

            //�p�X�̎w�肪�Ȃ�������
            if (string.IsNullOrEmpty(filePath))
            {
                //�����𒆎~
                return;
            }

        }
        else
        {

            //�t�@�C���̕ۑ�����쐬
            filePath = string.Format(Path.Combine(directoryPath, filename + time + ".png"));

            //�t�H���_�����݂��Ă��Ȃ�������
            if (!Directory.Exists(directoryPath))
            {

                //�_�C�A���O��\��
                if (EditorUtility.DisplayDialog("�t�H���_�����݂��܂���", "�ۑ���̃t�H���_" + directoryPath + "�͑��݂��܂���B" + "\n" + "�V�����t�H���_���쐬���܂����H", "�쐬", "���~"))
                {
                    //�t�H���_���쐬
                    Directory.CreateDirectory(directoryPath);
                    UnityEngine.Debug.Log("generated : " + directoryPath);
                }
                else
                {
                    //�����𒆎~
                    return;
                }

            }

        }

        //�Q�[���r���[���L���v�`��
        ScreenCapture.CaptureScreenshot(string.Format(filePath));

        //�L���v�`���������Ƃ�ʒm
        UnityEngine.Debug.Log("Captured : " + filePath);

        //EditorApplication.update�ɃR���[�`���̐i�s��ǉ�
        IEnumerator coroutine = GenerateFile(filePath);
        EditorApplication.update += () => coroutine.MoveNext();

    }

    public static IEnumerator GenerateFile(string path)
    {

        //�o�͒��ɐݒ�
        generatedFilePath = path;

        //�o�͂����܂ő҂�
        while (!File.Exists(path))
        {
            yield return null;
        }

        //�o�͒�������
        generatedFilePath = null;

        //�o�͂��ꂽ���Ƃ�ʒm
        UnityEngine.Debug.Log("generatedd : " + path);

        //�t�@�C�����J��
        OpenFile(path);

    }

    public static void OpenFile(string path)
    {

        if (isOpenExplorer)
        {
            //�G�N�X�v���[���[�ŊJ��
            Process.Start("explorer.exe", "/select," + path.Replace("/", "\\"));
        }

        if (isOpenApplication)
        {
            //�֘A�t����ꂽ�A�v���P�[�V�����ŊJ��
            Process.Start(path);
        }

    }

}

