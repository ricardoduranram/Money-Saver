using System;
using System.ComponentModel.DataAnnotations;

namespace Horeb.Infrastructure.Data
{    
    /// <summary>
    ///     Provides a base class for your objects which will be persisted to the database.
    /// </summary>
    public abstract class BaseEntity : Value<int>, IAuditTrails
    {
        public BaseEntity() {            
            CreatedOn = DateTime.Now;
            LastestUpdateOn = DateTime.Now;
        }                

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime LastestUpdateOn { get; set; }

        public bool IsActive { get; set; }
    }    
}