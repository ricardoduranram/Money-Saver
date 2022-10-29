using System;
using System.ComponentModel.DataAnnotations;

namespace Horeb.Infrastructure.Data;

public interface IIdentity<T>
{
    T Id { get; set; }
}

public abstract class Identity<V> : IIdentity<V>
{
    private V? _id;
    private bool _idHasBeenSet = false;

    [Key]
    public virtual V Id
    {
        get => this._id;
        set {
            if (_idHasBeenSet) {
                ThrowExceptionIfOverwritingAnId();
            }

            _id = value;
            _idHasBeenSet = true;
        }
    }

    private static void ThrowExceptionIfOverwritingAnId ()
        => throw new ApplicationException("You cannot change the Id of an entity.");
}
