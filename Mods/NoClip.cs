using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RassMobile.Mods
{
    internal class NoClip : ModFramework
    {

        public static int layer = 29, layerMask = 1 << layer;
        private LayerMask baseMask;

        public NoClip(string name, string description) : base(name, description)
        {
        }

        public override void OnEnabled()
        {
            base.OnEnabled();
            baseMask = GorillaLocomotion.Player.Instance.locomotionEnabledLayers;
            GorillaLocomotion.Player.Instance.locomotionEnabledLayers = layerMask;
            GorillaLocomotion.Player.Instance.bodyCollider.isTrigger = true;
            GorillaLocomotion.Player.Instance.headCollider.isTrigger = true;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            GorillaLocomotion.Player.Instance.locomotionEnabledLayers = baseMask;
            GorillaLocomotion.Player.Instance.bodyCollider.isTrigger = false;
            GorillaLocomotion.Player.Instance.headCollider.isTrigger = false;
        }

    }
}
