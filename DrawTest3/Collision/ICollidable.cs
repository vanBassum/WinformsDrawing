using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTest3.Collision
{

    internal interface ICollidable
    {
        ICollider Collider { get; }
    }
}
