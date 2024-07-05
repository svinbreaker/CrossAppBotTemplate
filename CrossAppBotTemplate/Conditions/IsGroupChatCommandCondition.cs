using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СrossAppBot.Commands;
using СrossAppBot.Entities;
using СrossAppBot;

namespace CrossAppBotTemplate.Conditions
{
    public class IsGroupChatCommandCondition : AbstractCommandCondition
    {
        private CommandContext Context { get; }

        public IsGroupChatCommandCondition(CommandContext context)
        {
            Context = context;
        }

        public override async Task<bool> Condition()
        {
            return Context.ChatGroup != null;
        }

        public override async Task Action()
        {
            await Context.AnswerAsync("The command does not work in private chat", null, true);
        }
    }
}


