using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RassMobile.Mods
{
    internal class BiggerMonkey : ModFramework
    {
        Vector3 ogSize;
        public BiggerMonkey(string name, string description) : base(name, description)
        {
        }

        public override void OnEnabled()
        {
            base.OnEnabled();
            ogSize = GorillaLocomotion.Player.Instance.bodyCollider.transform.root.localScale;

            GorillaLocomotion.Player.Instance.bodyCollider.transform.root.Find("RigCache").localScale = GorillaLocomotion.Player.Instance.bodyCollider.transform.root.localScale / 2;
            GameObject.Find("Local Gorilla Player").transform.parent = GorillaLocomotion.Player.Instance.bodyCollider.transform.root;
            GorillaLocomotion.Player.Instance.bodyCollider.transform.root.localScale = GorillaLocomotion.Player.Instance.bodyCollider.transform.root.localScale * 2;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            GorillaLocomotion.Player.Instance.bodyCollider.transform.root.Find("RigCache").localScale = ogSize;
            GameObject.Find("Local Gorilla Player").transform.parent = null;
            GorillaLocomotion.Player.Instance.bodyCollider.transform.root.localScale = ogSize;
        }

    }
}
