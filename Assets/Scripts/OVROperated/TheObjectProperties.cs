using UnityEngine;
using System.Collections;

//================================================================================
/// 物体属性记录的工具类
///
/// 记路了物体的几个重要属性
//================================================================================

/// <summary>
/// 物体属性记录的工具类
/// </summary>
public class TheObjectProperties : MonoBehaviour {

    /// <summary>
    /// 是否已经记录了MaxPoint
    /// </summary>
    public bool haveMaxPoint = false;

    /// <summary>
    /// 物体Mesh的最大点
    /// </summary>
    private MaxMinPoint maxPoint;

    /// <summary>
    /// 物体的摆放类型
    /// </summary>
    private MyEnum.PlaceObjectType placeObjectType;

    /// <summary>
    /// 物体在浏览器中的路径
    /// </summary>
    private string path;

    /// <summary>
    /// 设置物体Mesh的最大点
    /// </summary>
    /// 
    /// <param name="maxPoint">物体Mesh的最大点</param>
    public void SetMaxMinPoint(MaxMinPoint maxPoint)
    {
        this.maxPoint = maxPoint;
        this.haveMaxPoint = true;
    }

    /// <summary>
    /// 获取物体Mesh的最大点
    /// </summary>
    /// 
    /// <returns>物体Mesh的最大点</returns>
    public MaxMinPoint GetMaxMinPoint()
    {
        return maxPoint;
    }

    /// <summary>
    /// 设置物体的摆放类型
    /// </summary>
    /// 
    /// <param name="placeObjectType">物体的摆放类型</param>
    public void SetPlaceObjectType(MyEnum.PlaceObjectType placeObjectType)
    {
        this.placeObjectType = placeObjectType;
    }

    /// <summary>
    /// 获取物体的摆放类型
    /// </summary>
    /// 
    /// <returns>物体的摆放类型</returns>
    public MyEnum.PlaceObjectType GetPlaceObjectType()
    {
        return placeObjectType;
    }

    /// <summary>
    /// 设置物体在浏览器中的路径
    /// </summary>
    /// <param name="path">物体在浏览器中的路径</param>
    public void SetPath(string path)
    {
        this.path = path;
        calcuteTypeByPath(this.path);
    }

    /// <summary>
    /// 获取物体在浏览器中的路径
    /// </summary>
    /// 
    /// <returns>物体在浏览器中的路径</returns>
    public string GetPath()
    {
        return path;
    }

    /// <summary>
    /// 复制物体的属性
    /// </summary>
    /// <param name="properties">物体的属性脚本</param>
    public void copyProperties(TheObjectProperties properties)
    {
        SetMaxMinPoint(properties.GetMaxMinPoint());
        SetPlaceObjectType(properties.GetPlaceObjectType());
        SetPath(properties.GetPath());
    }

    /// <summary>
    /// 计算路径并获取物体的摆放类型
    /// </summary>
    /// <param name="path"></param>
    private void calcuteTypeByPath(string path)
    {
        string[] menuPaths = path.Split('/');

        if ("地面".Equals(menuPaths[0]))
        {
            this.placeObjectType = MyEnum.PlaceObjectType.DiMian;
        }
        else if ("非地面".Equals(menuPaths[0]))
        {
            this.placeObjectType = MyEnum.PlaceObjectType.FeiDiMian;
        }
        else if ("贴墙".Equals(menuPaths[0]))
        {
            this.placeObjectType = MyEnum.PlaceObjectType.TieQiang;
        }
    }

    public void calcuteTypeByFirstMenu(string firstMenu)
    {
        if ("地面".Equals(firstMenu))
        {
            this.placeObjectType = MyEnum.PlaceObjectType.DiMian;
        }
        else if ("非地面".Equals(firstMenu))
        {
            this.placeObjectType = MyEnum.PlaceObjectType.FeiDiMian;
        }
        else if ("贴墙".Equals(firstMenu))
        {
            this.placeObjectType = MyEnum.PlaceObjectType.TieQiang;
        }
        //addComponentByType();
    }

    //private void addComponentByType()
    //{
    //    switch (placeObjectType)
    //    {
    //        case MyEnum.PlaceObjectType.DiMian: gameObject.AddComponent<DiMianTrigger>(); break;
    //        case MyEnum.PlaceObjectType.FeiDiMian: gameObject.AddComponent<FeiDiMianTrigger>(); break;
    //        case MyEnum.PlaceObjectType.TieQiang: gameObject.AddComponent<TieQiangTrigger>(); break;
    //    }
    //}

    //public MonoBehaviour getTrigger()
    //{
    //    switch (placeObjectType)
    //    {
    //        case MyEnum.PlaceObjectType.DiMian: return gameObject.GetComponent<DiMianTrigger>();
    //        case MyEnum.PlaceObjectType.FeiDiMian: return gameObject.GetComponent<FeiDiMianTrigger>();
    //        case MyEnum.PlaceObjectType.TieQiang: return gameObject.GetComponent<TieQiangTrigger>();
    //        default: return null;
    //    }
    //}
}
