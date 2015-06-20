using UnityEngine;
using System.Collections;

//================================================================================
/// 摆放物体脚本
///
/// 通过头瞄碰撞点及点的周围环境，计算物体的中心点
//================================================================================

public class PlaceOperated : MyScripts {

    /// <summary>
    /// 头瞄射线碰撞的图层.
    /// </summary>
    private LayerMask layermask;

    /// <summary>
    /// 改变材质的图层.
    /// </summary>
    private LayerMask changedMaterialLayer;

    /// <summary>
    /// 摆放物件的图层.
    /// </summary>
    private LayerMask objectLayermask = 1 << 8;

    /// <summary>
    /// 地面的图层.
    /// </summary>
    private LayerMask diMianLayer = 1 << 10;

    /// <summary>
    /// 墙壁的图层.
    /// </summary>
    private LayerMask qiangBiLayer = 1 << 11;

    /// <summary>
    /// 天花板的图层.
    /// </summary>
    private LayerMask tianHuaBanLayer = 1 << 12;

    /// <summary>
    /// 射线的距离.
    /// </summary>
    private float raycastDistance = 100f;

    /// <summary>
    /// 物体的中心位置.
    /// </summary>
    private Vector3 center;

    /// <summary>
    /// 物件的摆放类型.
    /// </summary>
    private MyEnum.PlaceObjectType objectType;

    /// <summary>
    /// 滑杆的位置.
    /// </summary>
    private Vector3 tagglePosition = new Vector3(0.05f, 0f, 0.44f);

    /// <summary>
    /// 滑杆.
    /// </summary>
    private GameObject taggle;

    protected override void CreateDo()
    {
        // 注册摆放物体的脚本.
        MyController.setPlaceOperated(this);

        // 加载刚体组件.
        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>().useGravity = false;
        }
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().isKinematic = false;

        // 恢复物件的缩放.
        transform.localScale = Vector3.one;

        // 判断并加载碰撞体组件，并获取最大点maxPoint.
        if (GetComponent<Collider>() == null)
        {
            maxVector3 = (transform.rotation * GetComponent<TheObjectProperties>().GetMaxMinPoint().Max);
            gameObject.AddComponent<BoxCollider>().size = maxVector3 * 2f;
        }
        else
        {
            maxVector3 = gameObject.GetComponent<BoxCollider>().size / 2f;
        }

        // 获取摆放类型.
        objectType = GetComponent<TheObjectProperties>().GetPlaceObjectType();

        // 判断摆放类型，并设置“头瞄射线碰撞的图层”和“改变材质的图层”.
        switch (objectType)
        {
            case MyEnum.PlaceObjectType.DiMian: 
            {
                layermask = diMianLayer | qiangBiLayer;
                changedMaterialLayer = objectLayermask | diMianLayer;
                taggle = MyController.CreatePrefabInTransform(MyController.getOvrControllerTransform(MyEnum.OvrControllerTransformName.CenterEyeAnchor), MyEnum.ScaneObjectType.Taggle, tagglePosition);
                taggle.transform.localRotation = Quaternion.Euler(Vector3.zero);
                break;
            }
            case MyEnum.PlaceObjectType.TieQiang: layermask = qiangBiLayer; changedMaterialLayer = objectLayermask | qiangBiLayer; break;
            case MyEnum.PlaceObjectType.FeiDiMian: layermask = tianHuaBanLayer; changedMaterialLayer = objectLayermask | tianHuaBanLayer; break;
        }

        gameObject.layer = 0;

    }

    /// <summary>
    /// 头瞄主体射线的碰撞信息.
    /// </summary>
    private RaycastHit hitInfo;

    /// <summary>
    /// 以物体为中心，向上方向的射线碰撞信息.
    /// </summary>
    private RaycastHit hitInfoTop;

    /// <summary>
    /// 以物体为中心，向下方向的射线碰撞信息.
    /// </summary>
    private RaycastHit hitInfoBottom;

    /// <summary>
    /// 以物体为中心，向前方向的射线碰撞信息.
    /// </summary>
    private RaycastHit hitInfoForward;

    /// <summary>
    /// 以物体为中心，向后方向的射线碰撞信息.
    /// </summary>
    private RaycastHit hitInfoBack;

    /// <summary>
    /// 以物体为中心，向右方向的射线碰撞信息.
    /// </summary>
    private RaycastHit hitInfoRight;

    /// <summary>
    /// 以物体为中心，向左方向的射线碰撞信息.
    /// </summary>
    private RaycastHit hitInfoLeft;

    /// <summary>
    /// 头瞄射线所穿透的物体的信息
    /// </summary>
    private RaycastHit[] hitInfoLine;

    /// <summary>
    /// 头瞄射线
    /// </summary>
    private Ray headRay;

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(center);
    }

    /// <summary>
    /// 物件模型的最大点
    /// </summary>
    private Vector3 maxVector3;

    /// <summary>
    /// 计算选择后的物件模型的最大点
    /// </summary>
    private Vector3 changedMaxVector3;
	void Update () {

        // 如果出现两只手，则放置物体
        bool fangzhi;
        if ( fangzhi =  true)
        {
            PlaceObject();
        }

        // 获取头瞄射线
        headRay = MyController.getHeadRay();

        // 判断头瞄射线碰撞点周围环境，并计算物体摆放的最佳中心点
        if (Physics.Raycast(headRay, out hitInfo, raycastDistance, layermask.value))
        {
            switch (objectType)
            {
                case MyEnum.PlaceObjectType.DiMian:
                    {
                        if (10 == hitInfo.collider.gameObject.layer)
                        {
                            changedMaxVector3 = transform.rotation * GetComponent<TheObjectProperties>().GetMaxMinPoint().Max;
                            center = hitInfo.point + transform.TransformDirection(Vector3.Scale(transform.InverseTransformDirection(-Vector3.up), maxVector3));

                            // 判断向前射线的碰撞点，与物体中心对比，并计算应该挪动的距离
                            if (Physics.Raycast(center, Vector3.forward, out hitInfoForward, raycastDistance, qiangBiLayer.value))
                            {
                                if (Vector3.Distance(hitInfoForward.point, center) <= Mathf.Abs(maxVector3.z))
                                {
                                    center.z = hitInfoForward.point.z - Mathf.Abs(changedMaxVector3.z);
                                }
                            }

                            // 判断向后射线的碰撞点，与物体中心对比，并计算应该挪动的距离
                            if (Physics.Raycast(center, -Vector3.forward, out hitInfoBottom, raycastDistance, qiangBiLayer.value))
                            {
                                if (Vector3.Distance(hitInfoBottom.point, center) <= Mathf.Abs(maxVector3.z))
                                {
                                    center.z = hitInfoBottom.point.z + Mathf.Abs(changedMaxVector3.z);
                                }
                            }

                            // 判断向右射线的碰撞点，与物体中心对比，并计算应该挪动的距离
                            if (Physics.Raycast(center, Vector3.right, out hitInfoRight, raycastDistance, qiangBiLayer.value))
                            {
                                if (Vector3.Distance(hitInfoRight.point, center) <= Mathf.Abs(maxVector3.z))
                                {
                                    center.x = hitInfoRight.point.x - Mathf.Abs(changedMaxVector3.x);
                                }
                            }

                            // 判断向左射线的碰撞点，与物体中心对比，并计算应该挪动的距离
                            if (Physics.Raycast(center, -Vector3.right, out hitInfoLeft, raycastDistance, qiangBiLayer.value))
                            {
                                if (Vector3.Distance(hitInfoLeft.point, center) <= Mathf.Abs(maxVector3.z))
                                {
                                    center.x = hitInfoLeft.point.x + Mathf.Abs(changedMaxVector3.x);
                                }
                            }
                        }
                        break;
                    }
                case MyEnum.PlaceObjectType.TieQiang:
                    {
                        if (11 == hitInfo.collider.gameObject.layer)
                        {
                            transform.localRotation = Quaternion.Euler(hitInfo.transform.localRotation.eulerAngles + new Vector3(-90f, -180f, 0f));
                            center = hitInfo.point + transform.TransformDirection(Vector3.Scale(transform.InverseTransformDirection(hitInfo.normal), maxVector3));
                            float vector3y = transform.TransformDirection(Vector3.Scale(transform.InverseTransformDirection(Vector3.up), maxVector3)).y;

                            // 判断模型的左方向射线的碰撞点，与物体中心对比，并计算应该挪动的距离
                            if (Physics.Raycast(center, -transform.right, out hitInfoLeft, raycastDistance, layermask.value))
                            {
                                if ((center - hitInfoLeft.point).sqrMagnitude <= transform.TransformDirection(Vector3.Scale(transform.InverseTransformDirection(hitInfoLeft.normal), maxVector3)).sqrMagnitude)
                                {
                                    center = center + Mathf.Sqrt((transform.TransformDirection(Vector3.Scale(transform.InverseTransformDirection(hitInfoLeft.normal), maxVector3)) - hitInfoLeft.point + center).sqrMagnitude) * hitInfoLeft.normal;
                                }
                            }

                            // 判断模型的右方向射线的碰撞点，与物体中心对比，并计算应该挪动的距离
                            if (Physics.Raycast(center, transform.right, out hitInfoRight, raycastDistance, layermask.value))
                            {
                                if ((center - hitInfoRight.point).sqrMagnitude <= transform.TransformDirection(Vector3.Scale(transform.InverseTransformDirection(hitInfoRight.normal), maxVector3)).sqrMagnitude)
                                {
                                    center = center + Mathf.Sqrt((transform.TransformDirection(Vector3.Scale(transform.InverseTransformDirection(hitInfoRight.normal), maxVector3)) - hitInfoRight.point + center).sqrMagnitude) * hitInfoRight.normal;
                                }
                            }

                            // 判断世界坐标的向上方向射线的碰撞点，与物体中心对比，并计算应该挪动的距离
                            if (Physics.Raycast(center, Vector3.up, out hitInfoTop, raycastDistance, tianHuaBanLayer.value))
                            {
                                if ((center - hitInfoTop.point).sqrMagnitude <= transform.TransformDirection(Vector3.Scale(transform.InverseTransformDirection(hitInfoTop.normal), maxVector3)).sqrMagnitude)
                                {
                                    center.y = hitInfoTop.point.y - vector3y;
                                }
                            }

                            // 判断世界坐标的向下方向射线的碰撞点，与物体中心对比，并计算应该挪动的距离
                            if (Physics.Raycast(center, -Vector3.up, out hitInfoBottom, raycastDistance, diMianLayer.value))
                            {
                                if ((center - hitInfoBottom.point).sqrMagnitude <= transform.TransformDirection(Vector3.Scale(transform.InverseTransformDirection(hitInfoBottom.normal), maxVector3)).sqrMagnitude)
                                {
                                    center.y = hitInfoBottom.point.y + vector3y;
                                }
                            }
                        }
                        break;
                    }
                case MyEnum.PlaceObjectType.FeiDiMian:
                    {
                        if (12 == hitInfo.collider.gameObject.layer)
                        {
                            center = hitInfo.point;
                        }
                        break;
                    }
            }

            // 判断是否有物体遮挡
            hitInfoLine = Physics.RaycastAll(headRay.origin, headRay.direction, raycastDistance, changedMaterialLayer.value);

            if (hitInfoLine.Length > 0)
            {
                changeMaterialsInHitInfos(hitInfoLine, MyController.getMyShaderController().transformMaterial);
            }
        }
	}

    /// <summary>
    /// 放置物体
    /// </summary>
    private void PlaceObject()
    {

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().freezeRotation = false;
        gameObject.layer = 8;

        Destroy(this);
    }

    /// <summary>
    /// 给遮挡物体添加变色脚本
    /// </summary>
    /// <param name="hitInfos"></param>
    /// <param name="materials"></param>
    private void changeMaterialsInHitInfos(RaycastHit[] hitInfos, Material materials)
    {
        for (int i = 0; i < hitInfos.Length; i++ )
        {
            if (hitInfos[i].collider.GetComponent<ChangeMaterialsByRay>() == null)
                hitInfos[i].collider.gameObject.AddComponent<ChangeMaterialsByRay>().materials = materials;
        }
    }

    protected override void DestroyDo()
    {

        Destroy(taggle);
    }

    //public void addMeshCollider(Transform objects)
    //{
    //    if (objects.childCount > 0)
    //    {
    //        if (objects.collider != null) Destroy(objects.collider);
    //        if (objects.GetComponent<MeshFilter>() != null)
    //        {
    //            objects.gameObject.AddComponent<MeshCollider>().sharedMesh = objects.GetComponent<MeshFilter>().mesh;
    //        }

    //        for (int i = 0; i < objects.childCount; i++ )
    //        {
    //            addMeshCollider(objects.GetChild(i));
    //        }
    //    }
    //    else if (objects.GetComponent<MeshFilter>() != null)
    //    {
    //        if (objects.collider != null) Destroy(objects.collider);
    //        objects.gameObject.AddComponent<MeshCollider>().sharedMesh = objects.GetComponent<MeshFilter>().mesh;
    //    }
    //}

    //public void removeMeshCollider(Transform objects)
    //{
    //    if (objects.childCount > 0)
    //    {
    //        Destroy(objects.collider);

    //        for (int i = 0; i < objects.childCount; i++)
    //        {
    //            removeMeshCollider(objects.GetChild(i));
    //        }
    //    }
    //    else if (objects.GetComponent<MeshCollider>() != null)
    //    {
    //        Destroy(objects.collider);
    //    }
    //}

    //public IEnumerator addMeshCollider(GameObject objects)
    //{
    //    addMeshCollider(objects.transform);
    //    yield return true;
    //}

    //public IEnumerator removeMeshCollider(GameObject objects)
    //{
    //    removeMeshCollider(objects.transform);
    //    yield return true;
    //}
}
