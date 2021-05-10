using Flunt.Notifications;

namespace Commom.Commands
{
    public abstract class CommandRequest : Notifiable<Notification>, ICommand
    {
        public virtual void Validate(){}
    }
}
