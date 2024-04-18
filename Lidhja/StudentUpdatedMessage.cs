using System;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Lidhja
{
    public class StudentUpdatedMessage : ValueChangedMessage<string>
    {
        public StudentUpdatedMessage(string value) : base(value)
        {
        }
    }
}
