using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "SO/Item")]
public class ItemList : ScriptableObject
{
    public List<Item> iList;

    /// <summary>
    /// 아이템 생성 함수
    /// </summary>
    public static ItemList Create()
    {
        var asset = CreateInstance<ItemList>();
        //CtrayeInstance는 ScriptableObject에서 인스턴스를 생성하는 기능

        AssetDatabase.CreateAsset(asset, "Assets/Resources/Item/itemExample01.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }

    public static ItemList Load()
    {
        var itemList = AssetDatabase.LoadAssetAtPath("Assets/Resources/Item", typeof(ItemList)) as ItemList;
        return itemList;
    }
}

