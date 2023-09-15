using Wax.Domain.Users;

namespace Wax.Domain.Utilities.Records
{
    public interface IRecord
    {
        DateTime CreateTime { get; set; }
        IUserIdentity? Creater { get; set; }
    }
}
