using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СrossAppBot.Commands;

namespace CrossAppBotTemplate.Conditions
{
    public class IsPrivateChatCommandCondition : AbstractCommandCondition
    {

        private CommandContext Context { get; }

        public IsPrivateChatCommandCondition(CommandContext context)
        {
            Context = context;
        }

        public override async Task<bool> Condition()
        {
            return Context.ChatGroup == null;
        }

        public override async Task Action()
        {
            await Context.AnswerAsync("The command does not work in group chat", null, true);
        }
    }

}
