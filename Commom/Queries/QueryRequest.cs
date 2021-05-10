using Flunt.Notifications;

namespace Commom.Queries
{
    public abstract class QueryRequest : Notifiable<Notification>, IQuery
    {
        public virtual void Validate() { }
}
}
