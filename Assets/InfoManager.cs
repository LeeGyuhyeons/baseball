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
        //리소스 폴더에 있는 info(Text Asset)를 로드 하겠습니다.
        //Text Asset은 텍스트 형태의 에셋을 의미합니다.


        playerinfo = JsonUtility.FromJson<Info>(loadedJson.text);
        //JsonUtility.FromJson<T>(string json);
        //json 파일로부터 읽어온 파일을 기준으로 데이터를 적용하는 코드
        printText();
    }

    /// <summary>
    /// 포인트를 사용해서 골드로 변경하는 코드(100 포인트 -> 10000골드)
    /// </summary>
    public void GoldPlus()
    {
        if (playerinfo.point >= 100)
        {
            playerinfo.point -= 100;
            playerinfo.gold += 10000;
            var classtoJsom = JsonUtility.ToJson(playerinfo);
            //JsonUtility.ToJson(object obj)
            //객체의 정보를 Json 파일로 보내는 기능
            //플레이어 정보를 json파일에 전달
            SaveData(playerinfo);
            printText();

        }
        else
        {
            Debug.Log("교환할 포인트가 부족합니다.");
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

    //유니티 데이터 경로
    private string SavePath => Application.persistentDataPath;
    //쓰기 가능한 폴더의 위치, 특정 운영체제에서 앱이 사용할 수 있도록 허용하는 경로
    //C:\User\[user name]\AppData\LocalLow\[company name]\[product name]

    private string DataPath => Application.dataPath;
    //데이터의 저장 경로(읽기 전용)으로 프로젝트 폴더 내부(Asset)을 의미합니다.
    
    private string StreamingPath => Application.streamingAssetsPath;
    //Application.dataPath + streamingAsset = Application.streamingAssetsPath

    public void SaveData(Info info)
    {
        //폴더가 없을 경우에는 폴더를 생성합니다.
        if (!Directory.Exists(ResourcePath))
        {
            Directory.CreateDirectory(ResourcePath);
        }

        var sJson = JsonUtility.ToJson(info);//1. json 파일의 정보를 string 형태로 저장합니다.

        var filePath = ResourcePath + "info.json";

        //여러 문자열을 한 경로로 결합하는 기능 (System.IO)
        File.WriteAllText(filePath, sJson);
    }
    public void SaveData2()
    {
        if (!Directory.Exists(ResourcePath))
        {
            Directory.CreateDirectory(ResourcePath);
        }
        var sJson = JsonUtility.ToJson(playerinfo); //1. json 파일의 정보를 string 형태로 저장합니다.
        var FilePath = Path.Combine(DataPath, "info.json");
        //DataPath/info.json
        //여러 문자열을 한 경로로 결합하는 기능 (System.IO)
        File.WriteAllText(FilePath, sJson);
        printText();
    }

    public Info LoadData(string path)
    {
        playerinfo = null;//클래스 객체 비우기
        if (File.Exists(path))//파일이 전달한 패스에 존재할 경우
        {
            var json = File.ReadAllText(path); //해당 경로로부터 파일을 읽어옵니다.
            playerinfo = JsonUtility.FromJson<Info>(json);//읽어온 내용을 통해 Info에 값을 전달합니다.

        }
        return playerinfo;//완성된 객체를 Info합니다.
    }

    public void LoadData2()
    {
        var data = File.ReadAllText(ResourcePath + "info.json");
        playerinfo = JsonUtility.FromJson<Info>(data);



    }
}
