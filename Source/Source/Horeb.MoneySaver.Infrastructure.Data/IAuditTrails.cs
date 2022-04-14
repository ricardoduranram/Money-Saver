using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.Infrastructure.Data
{
    public interface IAuditTrails
    {
        DateTime CreatedOn { get; set;}

        DateTime LastestUpdateOn { get; set; }
        bool IsActive { get; set; }
    }
}
