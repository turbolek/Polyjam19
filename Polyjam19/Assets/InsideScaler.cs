using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideScaler : MonoBehaviour
{
    public void ScaleToApartment(Apartment apartment)
    {
        if (apartment != null)
            transform.localScale = new Vector3(apartment.insideScale, apartment.insideScale, 1f);
        else
            transform.localScale = Vector3.one;
    }
}
