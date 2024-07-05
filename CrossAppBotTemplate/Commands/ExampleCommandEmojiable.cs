using CrossAppBotTemplate.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СrossAppBot;
using СrossAppBot.Commands;
using СrossAppBot.Entities;

namespace CrossAppBotTemplate.Commands
{
    public class ExampleCommandEmojiable : AbstractCommand
    {

        public ExampleCommandEmojiable() : base("reaction", "Command to set a reaction to your message") { }

        protected override async Task Executee()
        {
            AbstractBotClient client = Context.Client;
            if (client is not IAddReaction) 
            {
                throw new NotSupportedException("Emojiable commands are not supported in this client");
            }
            await ((IAddReaction)client).AddReaction(Context.Message, "✅");
        }
    }
}
