using Flunt.Notifications;
using MediatR;

namespace Commom.Commands
{
    public abstract class CommandRequest : Notifiable<Notification>, IRequest<GenericCommandResult>, ICommand
    {
        public virtual void Validate() { }
    }
}
