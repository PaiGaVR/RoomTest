using UnityEngine;
using System.Collections;

//================================================================================
/// 模型的工具类
///
/// 模型中的使用工具类，譬如计算模型的点
//================================================================================
public static class MeshTools {

    /// <summary>
    /// 计算模型中的相对最大点
    /// </summary>
    /// 
    /// <param name="transforms">模型的Transform</param>
    /// <returns>最大点的封装</returns>
    public static MaxMinPoint calcuteMaxPoint(Transform transforms)
    {
        MaxMinPoint point = new MaxMinPoint();
        point = recurrenceCalcuteMaxPoint(transforms, point);
        point = point - transforms.position;
        return point;
    }

    /// <summary>
    /// 计算模型中的最大点
    /// </summary>
    /// 
    /// <param name="transforms">模型的Transform</param>
    /// <param name="maxPoint">最大点的封装类</param>
    private static MaxMinPoint recurrenceCalcuteMaxPoint(Transform transforms, MaxMinPoint maxPoint)
    {
        maxPoint = CalcuteMaxPointInTransform(transforms, maxPoint);

        if (transforms.childCount > 0)
        {
            for (int i = 0; i < transforms.childCount; i++ ) {
                maxPoint = recurrenceCalcuteMaxPoint(transforms.GetChild(i), maxPoint);
            }
        }

        return maxPoint;
    }

    /// <summary>
    /// 遍历模型中的最大点
    /// </summary>
    /// <param name="transforms">模型的Transform</param>
    /// <param name="maxPoint">最大点的封装类</param>
    /// <returns>最大点的封装类</returns>
    private static MaxMinPoint CalcuteMaxPointInTransform(Transform transforms, MaxMinPoint maxPoint)
    {
        if (transforms.GetComponent<MeshFilter>() != null)
        {
            Vector3[] vectors = transforms.GetComponent<MeshFilter>().mesh.vertices;

            for (int i = 0; i < vectors.Length; i++)
            {
                Vector3 v = transforms.TransformPoint(vectors[i]);

                maxPoint.MaxX = Mathf.Max(v.x, maxPoint.MaxX);
                maxPoint.MinX = Mathf.Min(v.x, maxPoint.MinX);
                maxPoint.MaxY = Mathf.Max(v.y, maxPoint.MaxY);
                maxPoint.MinY = Mathf.Min(v.y, maxPoint.MinY);
                maxPoint.MaxZ = Mathf.Max(v.z, maxPoint.MaxZ);
                maxPoint.MinZ = Mathf.Min(v.z, maxPoint.MinZ);
            }
        }

        return maxPoint;
    }

    /// <summary>
    /// 改变模型材质并返回原有材质的记录
    /// </summary>
    /// <param name="transforms"> 模型的Transform</param>
    /// <param name="materials">模型的Materials</param>
    /// <param name="materialsRecord">模型的材质记录</param>
    /// <returns>模型的材质记录</returns>
    public static ArrayList changeAllMaterialsInModel(Transform transforms, Material materials, ArrayList materialsRecord)
    {
        if (transforms.GetComponent<MeshRenderer>() != null)
        {
            materialsRecord.Add(transforms.GetComponent<MeshRenderer>().material);
            transforms.GetComponent<MeshRenderer>().material = materials;
        }

        if (transforms.childCount > 0)
        {
            for (int i = 0; i < transforms.childCount; i++)
            {
               materialsRecord = changeAllMaterialsInModel(transforms.GetChild(i), materials, materialsRecord);
            }
        }

        return materialsRecord;
    }

    /// <summary>
    /// 改变模型材质并返回原有材质的记录
    /// </summary>
    /// <param name="transforms">模型的Transform</param>
    /// <param name="materials">模型的Materials</param>
    /// <returns>模型的材质记录</returns>
    public static ArrayList changeAllMaterialsInModel(Transform transforms, Material materials)
    {
        ArrayList materialsRecord = new ArrayList();
        materialsRecord = changeAllMaterialsInModel(transforms, materials, materialsRecord);

        return materialsRecord;
    }

    /// <summary>
    /// 根据材质记录恢复模型材质
    /// </summary>
    /// <param name="transforms">模型的Transform</param>
    /// <param name="materialsRecord">模型的材质记录</param>
    public static void RecoverAllMaterials(Transform transforms, ArrayList materialsRecord)
    {
        if (materialsRecord == null || materialsRecord.Count == 0) return;
        if (transforms.GetComponent<MeshRenderer>() != null)
        {
            transforms.GetComponent<MeshRenderer>().material = materialsRecord[0] as Material;
            materialsRecord.RemoveAt(0);
        }

        if (transforms.childCount > 0)
        {
            for (int i = 0; i < transforms.childCount; i++)
            {
                RecoverAllMaterials(transforms.GetChild(i), materialsRecord);
            }
        }
    }

    
}
