using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidContainer : MonoBehaviour
{
    public float maxLiquidYScale = 0.035f;
    public string upAxis;
    public Transform liquid;
    public string fluidName;
    public bool isInfinite;
    private Vector3 emptyScaleChange = new(0, 0.001f, 0);
    private MeshRenderer liquidMesh;

    private void Start()
    {
        liquidMesh = liquid.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        ResetWhenEmpty();

        if (!IsUpright())
        {
            EmptyContainer();
        }
    }

    public void FillContainer(Vector3 scaleChange, string newliquidName, Material newliquidMaterial)
    {
        if (IsEmpty())
        {
            fluidName = newliquidName;
            liquidMesh.material = newliquidMaterial;
        }

        if (fluidName == newliquidName)
        {
            if (!IsFull())
            {
                liquid.localScale += scaleChange;

                liquid.localPosition += new Vector3(0, 0, scaleChange.y);
            }
        }
    }

    private bool IsFull()
    {
        if (liquid.lossyScale.y >= maxLiquidYScale)
        {
            return true;
        }

        return false;
    }

    private bool IsEmpty()
    {
        if (liquid.lossyScale.y <= 0)
        {
            return true;
        }

        return false;
    }

    private bool IsUpright()
    {
        if (upAxis == "z")
        {
            if (transform.forward.y < -0.5)
            {
                return false;
            }
        }

        return true;
    }

    private void EmptyContainer()
    {
        if (!isInfinite)
        {
            if (!IsEmpty())
            {
                liquid.localScale -= emptyScaleChange;
                liquid.localPosition -= new Vector3(0, 0, emptyScaleChange.y);
            }
        }
    }

    private void ResetWhenEmpty()
    {
        if (IsEmpty())
        {
            liquid.gameObject.SetActive(false);
            fluidName = null;
        }
        else
        {
            liquid.gameObject.SetActive(true);
        }
    }
}