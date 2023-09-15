using Wax.Domain.Users;

namespace Wax.Domain.Utilities.Records
{
    public class BaseEditableRecord : BaseRecord, IEditableRecord
    {
        public DateTime LastUpdateTime { get; set; }
        public IUserIdentity? UpdateUser { get; set; }
    }
}
