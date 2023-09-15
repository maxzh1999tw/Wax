using Wax.Domain.Users;

namespace Wax.Domain.Utilities.Records
{
    public interface IEditableRecord : IRecord
    {
        DateTime LastUpdateTime { get; set; }
        IUserIdentity? UpdateUser { get; set; }
    }
}
