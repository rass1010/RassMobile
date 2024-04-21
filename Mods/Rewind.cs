using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RassMobile.Mods
{
    internal class Rewind : ModFramework
    {

        bool hold;
        bool rewind;
        List<Vector3> positions = new List<Vector3>();

        public Rewind(string name, string description) : base(name, description)
        {
        }

        public override void Update()
        {
            base.Update();

            if (ControllerInputPoller.instance.rightControllerSecondaryButton && !rewind)
            {
                hold = true;
                positions.Add(GorillaLocomotion.Player.Instance.transform.position);
            }
            else if(!ControllerInputPoller.instance.rightControllerSecondaryButton && hold)
            {
                hold = false;
                rewind = true;
            }

            if (rewind && positions.Count > 0)
            {
                GorillaLocomotion.Player.Instance.transform.position = positions[positions.Count - 1];
                positions.RemoveAt(positions.Count - 1);
            }
            else if (rewind)
            {
                rewind = false;
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
            }

        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            positions = new List<Vector3>();
            rewind = false;
        }


    }
}
