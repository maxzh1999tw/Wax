using Wax.Domain.Users;

namespace Wax.Domain.Utilities.Records
{
    public class BaseRecord : IRecord
    {
        public DateTime CreateTime { get; set; }
        public IUserIdentity? Creater { get; set; }
    }
}
