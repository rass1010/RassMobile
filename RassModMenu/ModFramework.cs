using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace RassMobile
{
    internal class ModFramework
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public ModFramework(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public virtual void Update()
        {

        }

        public virtual void OnEnabled()
        {

        }

        public virtual void OnDisabled()
        {

        }

    }
}
