using UnityEngine;
using System.Collections;

public class EffectRoot : MonoBehaviour
{
    public ParticleSystem clickEffect;

	void Start () {
        MyController.setEffectRoot(this);
	}
}
