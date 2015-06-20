using UnityEngine;
using System.Collections;

//================================================================================
/// 控制器
///
/// 记录脚本中使用的变量或脚本,脚本中变量传输的控制类.
//================================================================================
public static class MyController {

    /// <summary>
    /// 模型存放的路径.
    /// </summary>
    public static string filePath = UnityEngine.Application.dataPath + "/" + "Models";

    /// <summary>
    /// 场景存放的路径.
    /// </summary>
    public static string scanesPath = UnityEngine.Application.dataPath + "/" + "Scanes";

    /// <summary>
    /// 摆放模型的脚本.
    /// </summary>
    private static PlaceOperated placeOperated;

    /// <summary>
    /// 特效的初始化脚本.
    /// </summary>
    private static EffectRoot effectRoot;

    /// <summary>
    /// 根物体.
    /// </summary>
    private static GameObject rootObject;

    /// <summary>
    /// Shader的初始化脚本.
    /// </summary>
    private static MyShaderRoot shaderController;

    /// <summary>
    /// 场景初始化的控制器.
    /// </summary>
    private static RootController controller;

    /// <summary>
    /// 编辑模式下的第一个场景初始化.
    /// </summary>
    private static InitScaneRoot initScaneRoot;

    /// <summary>
    /// 场景工具控制器.
    /// </summary>
    private static ScaneObjectController scaneObjectController;

    /// <summary>
    /// 注册摆放模型的脚本.
    /// </summary>
    /// 
    /// <param name="operated">摆放模型的脚本.</param>
    public static void setPlaceOperated(PlaceOperated operated)
    {
        placeOperated = operated;
    }

    /// <summary>
    /// 获取摆放模型的脚本.
    /// </summary>
    /// 
    /// <returns>摆放模型的脚本.</returns>
    public static PlaceOperated getPlaceOperated()
    {
        return placeOperated;
    }

    /// <summary>
    /// 注册特效的初始化脚本.
    /// </summary>
    /// 
    /// <param name="root">特效的初始化脚本.</param>
    public static void setEffectRoot(EffectRoot root)
    {
        effectRoot = root;
    }

    /// <summary>
    /// 获取特效的初始化脚本.
    /// </summary>
    /// 
    /// <returns>特效的初始化脚本.</returns>
    public static EffectRoot getEffectRoot()
    {
        return effectRoot;
    }

    /// <summary>
    /// 设置场景初始化的控制器.
    /// </summary>
    /// 
    /// <param name="myController">场景初始化的控制器.</param>
    public static void setRootController(RootController myController){
        controller = myController;
        rootObject = myController.gameObject;
    }

    /// <summary>
    /// 注册特效的初始化脚本.
    /// </summary>
    /// 
    /// <param name="controller">特效的初始化脚本.</param>
    public static void setShaderController(MyShaderRoot controller)
    {
        shaderController = controller;
    }

    /// <summary>
    /// 获取特效的初始化脚本.
    /// </summary>
    /// 
    /// <returns>特效的初始化脚本.</returns>
    public static MyShaderRoot getMyShaderController()
    {
        return shaderController;
    }

    /// <summary>
    /// 获取根物体.
    /// </summary>
    /// 
    /// <returns>根物体.</returns>
    public static GameObject getRootObject()
    {
        return rootObject;
    }

    /// <summary>
    /// 设置编辑模式下的第一个场景初始化.
    /// </summary>
    /// 
    /// <param name="root">编辑模式下的第一个场景初始化.</param>
    public static void setInitScaneRoot(InitScaneRoot root)
    {
        initScaneRoot = root;
    }

    /// <summary>
    /// 设置场景工具控制器.
    /// </summary>
    /// 
    /// <param name="controller">场景工具控制器.</param>
    public static void setScaneObjectController(ScaneObjectController controller)
    {
        scaneObjectController = controller;
    }

    /// <summary>
    /// 获取场景初始化的控制器.
    /// </summary>
    /// 
    /// <returns>场景初始化的控制器.</returns>
    public static RootController getRootController()
    {
        return controller;
    }

    /// <summary>
    /// 获取编辑模式下的第一个场景初始化.
    /// </summary>
    /// <returns>编辑模式下的第一个场景初始化.</returns>
    public static InitScaneRoot getInitScaneRoot()
    {
        return initScaneRoot;
    }

    /// <summary>
    /// 获取场景工具控制器.
    /// </summary>
    /// 
    /// <returns>场景工具控制器.</returns>
    public static ScaneObjectController getScaneObjectController()
    {
        return scaneObjectController;
    }

    /// <summary>
    /// 获取头部射线.
    /// </summary>
    /// 
    /// <returns>头部射线.</returns>
    public static Ray getHeadRay()
    {
        return controller.getHeadRay();
    }

    /// <summary>
    /// 获取OVR角色控制对象中的Transform.
    /// </summary>
    /// 
    /// <param name="name">要获取的OVR角色控制器中的对象名称.</param>
    /// <returns>OVR角色控制对象中的Transform.</returns>
    public static Transform getOvrControllerTransform(MyEnum.OvrControllerTransformName name)
    {
        return controller.getOvrControllerTransform(name);
    }

    /// <summary>
    /// 在指定的Transform下创建场景工具
    /// </summary>
    /// 
    /// <param name="parent">指定的Transform,即父对象</param>
    /// <param name="scaneObjectName">场景工具名称</param>
    public static GameObject CreatePrefabInTransform(Transform parent, MyEnum.ScaneObjectType scaneObjectName, Vector3 position)
    {
        return scaneObjectController.CreatePrefabInTransform(parent, scaneObjectName, position);
    }


    public static GameObject CreatePrefabHomeInTransform(Transform parent, MyEnum.HuXingType scaneObjectName, Vector3 position)
    {
        return scaneObjectController.CreatePrefabHomeInTransform(parent, scaneObjectName, position);
    }
}
