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
    public class AdminRightsCommandCondition : AbstractCommandCondition
    {
        private CommandContext Context { get; }

        public AdminRightsCommandCondition(CommandContext context)
        {
            Context = context;
        }

        public override async Task<bool> Condition()
        {
            ChatGroup chatGroup = Context.ChatGroup;
            if (chatGroup == null)
            {
                return false;
            }
            else
            {
                return (await Context.Sender.GetRightsAsync(chatGroup)).Contains(UserRight.Administrator);
            }
        }

        public override async Task Action()
        {         
            await Context.AnswerAsync("Not enough rights to run the command.", null, true);
        }
    }
}
