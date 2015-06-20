using UnityEngine;
using System.Collections;

/// <summary>
/// 场景初始化的控制器
/// </summary>
public class RootController : MyScripts
{
    // 初始化时的场景类型（InitScane： 刚开始的场景、TheObjectBrowserScane：物件浏览器的场景、TheObjectOperatedScane：物件操作的场景）.
    public MyEnum.ScaneType scaneType;

    // 头部射线.
    private Ray headRay;

    // 头部射线绑定的Transform.
    private Transform centerHead;

    private GameObject home;

    private GameObject icon;

    protected override void CreateDo()
    {
        //注册场景初始化的控制器.
        MyController.setRootController(this);

        // 添加场景物件控制器
        gameObject.AddComponent<ScaneObjectController>();

        // 初始化场景.
        ScaneSwitch(this.scaneType);

        //获取头部射线绑定的Transform.
        centerHead = getOvrControllerTransform(MyEnum.OvrControllerTransformName.CenterEyeAnchor);

        InitScane();

        home = MyController.CreatePrefabHomeInTransform(null, MyEnum.HuXingType.modo, Vector3.zero);

        AddMeshCollider(home.transform);
    }

    void Update()
    {
        headRay.origin = centerHead.position;
        headRay.direction = centerHead.forward;
        Debug.DrawRay(headRay.origin, headRay.direction, Color.blue);
        if (home != null)
        {
            foreach (Transform tran in home.transform)
            {
                if (tran.tag.Equals("DiMianWuTi") || tran.tag.Equals("TianHuaBanWuTi"))
                {
                    if (Vector3.Distance(tran.position, transform.position) <= 3f)
                    {
                        if (tran.FindChild("icon(Clone)") != null)
                        {
                            tran.FindChild("icon(Clone)").gameObject.SetActive(true);
                            Vector3 v = transform.position - MyController.getOvrControllerTransform(MyEnum.OvrControllerTransformName.CenterEyeAnchor).transform.position;
                            Quaternion rotation;
                            rotation = Quaternion.LookRotation(v);
                            tran.FindChild("icon(Clone)").transform.rotation = rotation;
                        }
                    }
                    else
                    {
                        if (tran.FindChild("icon(Clone)") != null)
                        {
                            tran.FindChild("icon(Clone)").gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 场景选择.
    /// </summary>
    /// <param name="scaneType">场景类型.</param>
    public void ScaneSwitch(MyEnum.ScaneType scaneType)
    {
        switch (scaneType)
        {
            case MyEnum.ScaneType.InitScane: break;
        }
    }

    private void AddMeshCollider(Transform transform)
    {
        foreach (Transform trans in transform)
        {
            AddIconToGameObject(trans);

            if (trans.childCount != 0)
            {
                AddMeshCollider(trans);
            }
            else
            {
                trans.gameObject.AddComponent<MeshCollider>();
            }
        }
    }

    private void AddIconToGameObject(Transform transform)
    {
        if (transform.tag.Equals("DiMianWuTi"))
        {
            icon = MyController.CreatePrefabInTransform(transform, MyEnum.ScaneObjectType.icon, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z));
        }
        if (transform.tag.Equals("TianHuaBanWuTi"))
        {
            icon = MyController.CreatePrefabInTransform(transform, MyEnum.ScaneObjectType.icon, new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z));
        }

        if (transform.tag.Equals("QiangZhi") && transform.tag.Equals("Men") && transform.tag.Equals("QiangShangWuTi"))
        {
            //icon = MyController.CreatePrefabInTransform(transform, MyEnum.ScaneObjectType.icon, transform.position - );
        }

    }

    /// <summary>
    /// 获取OVR角色控制对象中的Transform.
    /// </summary>
    /// 
    /// <param name="name">要获取的OVR角色控制器中的对象名称.</param>
    /// <returns>OVR角色控制对象中的Transform.</returns>
    public Transform getOvrControllerTransform(MyEnum.OvrControllerTransformName name)
    {
        switch (name)
        {
            case MyEnum.OvrControllerTransformName.OVRPlayerController: return transform;
            case MyEnum.OvrControllerTransformName.ForwardDirection: return transform.GetChild(0);
            case MyEnum.OvrControllerTransformName.OVRCameraRig: return transform.GetChild(1);
            case MyEnum.OvrControllerTransformName.LeftEyeAnchor: return transform.GetChild(1).GetChild(0);
            case MyEnum.OvrControllerTransformName.CenterEyeAnchor: return transform.GetChild(1).GetChild(1);
            case MyEnum.OvrControllerTransformName.RightEyeAnchor: return transform.GetChild(1).GetChild(2);
        }

        return null;
    }

    /// <summary>
    /// 初始的场景设置.
    /// </summary>
    private void InitScane()
    {
        getOvrControllerTransform(MyEnum.OvrControllerTransformName.OVRPlayerController).gameObject.AddComponent<InitScaneRoot>();
    }

    /// <summary>
    /// 获取头部射线.
    /// </summary>
    /// 
    /// <returns>头部射线.</returns>
    public Ray getHeadRay()
    {
        return headRay;
    }

    protected override void DestroyDo() { }
}
