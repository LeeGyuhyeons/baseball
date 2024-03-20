using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//스크립터블 오브젝트(Scriptable Object)
//유니티에서 제공하는 대량의 데이터를 저장할 수 있는 데이터 컨테이너

//값의 사본이 생성되는 것을 방지 할 수 있습니다.

//게임 오브젝트에 컴포넌트로 부착이 불가능하며, 프로젝트에서 에셋으로 저장됩니다.

public enum ItemTYPE
{
    WEAPON, ARMOR, POTION
}

[CreateAssetMenu(fileName = "item", menuName = "SO/Item")]
public class Item : ScriptableObject
{
    [SerializeField] ItemTYPE type;
    [SerializeField] private string description;

    public ItemTYPE Type { get => type; set => type = value; }
    public string Description { get => description; set => description = value; }
    public static Item Create()
    {
        var asset = CreateInstance<Item>();
        //CtrayeInstance는 ScriptableObject에서 인스턴스를 생성하는 기능

        AssetDatabase.CreateAsset(asset, "Assets/Resources/Item/itemExample01.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
    public static Item Load()
    {
        var item = AssetDatabase.LoadAssetAtPath("Assets/Resources/Item", typeof(Item)) as Item;
        return item;
    }
}
