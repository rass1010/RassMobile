using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RassMobile.Mods
{
    internal class BounceMonke : ModFramework
    {
        float bounce;
        PhysicMaterialCombine ogCombine;

        public BounceMonke(string name, string description) : base(name, description)
        {
        }

        public override void OnEnabled()
        {
            base.OnEnabled();
            bounce = GorillaLocomotion.Player.Instance.bodyCollider.material.bounciness;
            ogCombine = GorillaLocomotion.Player.Instance.bodyCollider.material.bounceCombine;
            GorillaLocomotion.Player.Instance.bodyCollider.material.bounceCombine = PhysicMaterialCombine.Maximum;
            GorillaLocomotion.Player.Instance.bodyCollider.material.bounciness = 1.0f;
            
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            GorillaLocomotion.Player.Instance.bodyCollider.material.bounciness = bounce;
            GorillaLocomotion.Player.Instance.bodyCollider.material.bounceCombine = ogCombine;
        }

    }
}
