using Flunt.Notifications;
using MediatR;

namespace Commom.Queries
{
    public abstract class QueryRequest : Notifiable<Notification>, IRequest<GenericQueryResult>, IQuery
    {
        public virtual void Validate() { }
}
}
