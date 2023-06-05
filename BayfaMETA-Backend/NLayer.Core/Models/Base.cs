using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    /// <summary>
    /// Base model as all models have an Id attribute.
    /// </summary>
    public abstract class Base
    {
        public int Id { get; set; }
    }
}
