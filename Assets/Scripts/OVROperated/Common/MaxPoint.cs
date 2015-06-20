using UnityEngine;
using System.Collections;

public struct MaxMinPoint {

    private float max_x;
    private float max_y;
    private float max_z;

    private float min_x;
    private float min_y;
    private float min_z;

    public MaxMinPoint(float x, float y, float z)
    {
        this.max_x = x;
        this.max_y = y;
        this.max_z = z;

        this.min_x = x;
        this.min_y = y;
        this.min_z = z;
    }

    public MaxMinPoint(Vector3 v)
    {
        this.max_x = v.x;
        this.max_y = v.y;
        this.max_z = v.z;

        this.min_x = v.x;
        this.min_y = v.y;
        this.min_z = v.z;
    }

    public Vector3 getMaxX()
    {
        return new Vector3(max_x, 0f, 0f);
    }

    public Vector3 getMinX()
    {
        return new Vector3(min_x, 0f, 0f);
    }

    public Vector3 getMaxY()
    {
        return new Vector3(0f, max_y, 0f);
    }

    public Vector3 getMinY()
    {
        return new Vector3(0f, min_y, 0f);
    }

    public Vector3 getMaxZ()
    {
        return new Vector3(0f, 0f, max_z);
    }

    public Vector3 getMinZ()
    {
        return new Vector3(0f, 0f, min_z);
    }

    public Vector3 Max
    {
        get
        {
            return new Vector3(max_x, max_y, max_z);
        }
    }

    public float MaxValue
    {
        get
        {
            return Mathf.Max(Mathf.Max(this.max_x, Mathf.Max(this.max_y, this.max_z)));
        }
    }

    public float MaxX
    {
        set
        {
            this.max_x = value;
        }

        get
        {
            return this.max_x;
        }
    }

    public float MinX
    {
        set
        {
            this.min_x = value;
        }
        get
        {
            return this.min_x;
        }
    }

    public float MaxY
    {
        set
        {
            this.max_y = value;
        }

        get
        {
            return this.max_y;
        }
    }

    public float MinY
    {
        set
        {
            this.min_y = value;
        }
        get
        {
            return this.min_y;
        }
    }

    public float MaxZ
    {
        set
        {
            this.max_z = value;
        }

        get
        {
            return this.max_z;
        }
    }

    public float MinZ
    {
        set
        {
            this.min_z = value;
        }
        get
        {
            return min_z;
        }
    }

    public Vector3 MaxBounds
    {
        get
        {
            return new Vector3(Mathf.Max(max_x, -min_x), Mathf.Max(max_y, -min_y), Mathf.Max(max_z, -min_z));
        }
    }

    public Vector3 MinBounds
    {
        get
        {
            return new Vector3(Mathf.Min(-min_x, max_x), Mathf.Min(-min_y, max_y), Mathf.Min(-min_z, max_z));
        }
    }

    public float AbsMaxX
    {
        get
        {
            return Mathf.Max(max_x, -min_x);
        }
    }

    public float AbsMaxY
    {
        get
        {
            return Mathf.Max(max_y, -min_y);
        }
    }

    public float AbsMaxZ
    {
        get
        {
            return Mathf.Max(max_z, -min_z);
        }
    }

    public string ToString()
    {
        return "max:(" + max_x + "," + max_y + "," + max_z + ");min(" + min_x + "," + min_y + "," + min_z + ")";
    }

    public static MaxMinPoint operator -(MaxMinPoint point, Vector3 vector3)
    {
        point.MaxX = point.MaxX - vector3.x;
        point.MinX = point.MinX - vector3.x;
        point.MaxY = point.MaxY - vector3.y;
        point.MinY = point.MinY - vector3.y;
        point.MaxZ = point.MaxZ - vector3.z;
        point.MinZ = point.MinZ - vector3.z;
        return point;
    }

    public static MaxMinPoint operator +(MaxMinPoint point, Vector3 vector3)
    {
        point.MaxX = point.MaxX + vector3.x;
        point.MinX = point.MinX + vector3.x;
        point.MaxY = point.MaxY + vector3.y;
        point.MinY = point.MinY + vector3.y;
        point.MaxZ = point.MaxZ + vector3.z;
        point.MinZ = point.MinZ + vector3.z;
        return point;
    }

    public Vector3 getMaxDot(Vector3 vector3)
    {
        return new Vector3(max_x * vector3.x, max_y * vector3.y, max_z * vector3.z);
    }
    
}
