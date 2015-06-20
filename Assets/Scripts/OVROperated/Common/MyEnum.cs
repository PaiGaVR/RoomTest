using UnityEngine;
using System.Collections;

//================================================================================
/// 所用到的枚举类集合
///
/// 集合大部分枚举类
//================================================================================

public static class MyEnum {

    public enum HuXingType
    {
        modo
    }

    /// <summary>
    /// 场景类型.
    /// </summary>
    public enum ScaneType
    {
        InitScane, TheObjectBrowserScane, TheObjectOperatedScane
    }

    /// <summary>
    /// OVR角色控制器中的对象名称.
    /// </summary>
    public enum OvrControllerTransformName
    {
        OVRPlayerController, ForwardDirection, OVRCameraRig, LeftEyeAnchor, CenterEyeAnchor, RightEyeAnchor
    }

    /// <summary>
    /// 场景物件的名称.
    /// </summary>
    public enum ScaneObjectType
    {
        UI_Root, UI_FirstMenu, UI_SecondMenu, UI_ThirdMenu, ObjectOperatedMenu, Menu, Menu02, Taggle, XiangQingMenu, icon, TiHuanMenu, FunctionMenu, test
    }

    /// <summary>
    /// 菜单级别.
    /// </summary>
    public enum MenuLevel
    {
        first = 0, second = 1, third = 3, other = 4
    }

    /// <summary>
    /// 模型的类型，贴地面、贴墙、和贴天花板
    /// </summary>
    public enum PlaceObjectType
    {
        DiMian, FeiDiMian, TieQiang
    }

    /// <summary>
    /// 方向
    /// </summary>
    public enum Direction
    {
        X, Y, Z
    }
}
