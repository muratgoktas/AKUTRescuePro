using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AKUTRescue.Core.Repositories
{
    public interface IEntity<TId> 
  

    {
        TId Id { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
        DateTime? DeleteDate { get; set; }
        string CreateBy { get; set; }
        string UpdatedBy { get; set; }
        string DeletedBy { get; set; }
        bool Status { get; set; }
    }
}
