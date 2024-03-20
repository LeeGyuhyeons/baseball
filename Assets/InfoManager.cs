using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

class InfoManager : MonoBehaviour
{
    [SerializeField]  Info playerinfo;

    public Text IDText;
    public Text PointText;
    public Text GoldText;
    


    private void Awake()
    {
        playerinfo = new Info();

        var loadedJson = Resources.Load<TextAsset>("info");
        //���ҽ� ������ �ִ� info(Text Asset)�� �ε� �ϰڽ��ϴ�.
        //Text Asset�� �ؽ�Ʈ ������ ������ �ǹ��մϴ�.


        playerinfo = JsonUtility.FromJson<Info>(loadedJson.text);
        //JsonUtility.FromJson<T>(string json);
        //json ���Ϸκ��� �о�� ������ �������� �����͸� �����ϴ� �ڵ�
        printText();
    }

    /// <summary>
    /// ����Ʈ�� ����ؼ� ���� �����ϴ� �ڵ�(100 ����Ʈ -> 10000���)
    /// </summary>
    public void GoldPlus()
    {
        if (playerinfo.point >= 100)
        {
            playerinfo.point -= 100;
            playerinfo.gold += 10000;
            var classtoJsom = JsonUtility.ToJson(playerinfo);
            //JsonUtility.ToJson(object obj)
            //��ü�� ������ Json ���Ϸ� ������ ���
            //�÷��̾� ������ json���Ͽ� ����
            SaveData(playerinfo);
            printText();

        }
        else
        {
            Debug.Log("��ȯ�� ����Ʈ�� �����մϴ�.");
        }
    }
    public string PLAYER_ID => IDText.text = playerinfo.name.ToString();

    public string PLAYER_POINT => PointText.text = $"{playerinfo.point:n0}";

    public string PLAYER_GOLD => GoldText.text = $"{playerinfo.gold:n0}";

    void printText()
    {

        IDText.text = PLAYER_ID;
        PointText.text = PLAYER_POINT;
        GoldText.text = PLAYER_GOLD;
    }
    private string ResourcePath => Application.dataPath + "/Resources/";

    //����Ƽ ������ ���
    private string SavePath => Application.persistentDataPath;
    //���� ������ ������ ��ġ, Ư�� �ü������ ���� ����� �� �ֵ��� ����ϴ� ���
    //C:\User\[user name]\AppData\LocalLow\[company name]\[product name]

    private string DataPath => Application.dataPath;
    //�������� ���� ���(�б� ����)���� ������Ʈ ���� ����(Asset)�� �ǹ��մϴ�.
    
    private string StreamingPath => Application.streamingAssetsPath;
    //Application.dataPath + streamingAsset = Application.streamingAssetsPath

    public void SaveData(Info info)
    {
        //������ ���� ��쿡�� ������ �����մϴ�.
        if (!Directory.Exists(ResourcePath))
        {
            Directory.CreateDirectory(ResourcePath);
        }

        var sJson = JsonUtility.ToJson(info);//1. json ������ ������ string ���·� �����մϴ�.

        var filePath = ResourcePath + "info.json";

        //���� ���ڿ��� �� ��η� �����ϴ� ��� (System.IO)
        File.WriteAllText(filePath, sJson);
    }
    public void SaveData2()
    {
        if (!Directory.Exists(ResourcePath))
        {
            Directory.CreateDirectory(ResourcePath);
        }
        var sJson = JsonUtility.ToJson(playerinfo); //1. json ������ ������ string ���·� �����մϴ�.
        var FilePath = Path.Combine(DataPath, "info.json");
        //DataPath/info.json
        //���� ���ڿ��� �� ��η� �����ϴ� ��� (System.IO)
        File.WriteAllText(FilePath, sJson);
        printText();
    }

    public Info LoadData(string path)
    {
        playerinfo = null;//Ŭ���� ��ü ����
        if (File.Exists(path))//������ ������ �н��� ������ ���
        {
            var json = File.ReadAllText(path); //�ش� ��ηκ��� ������ �о�ɴϴ�.
            playerinfo = JsonUtility.FromJson<Info>(json);//�о�� ������ ���� Info�� ���� �����մϴ�.

        }
        return playerinfo;//�ϼ��� ��ü�� Info�մϴ�.
    }

    public void LoadData2()
    {
        var data = File.ReadAllText(ResourcePath + "info.json");
        playerinfo = JsonUtility.FromJson<Info>(data);



    }
}
