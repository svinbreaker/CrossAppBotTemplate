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
    public class HigherRightsCommandCondition : AbstractCommandCondition
    {
        private CommandContext Context { get; }
        private ChatUser Target { get; set; }

        public HigherRightsCommandCondition(CommandContext context, ChatUser target)
        {
            Context = context;
            Target = target;
        }

        public override async Task<bool> Condition()
        {
            ChatGroup guild = Context.ChatGroup;

            if (guild == null)
            {
                return false;
            }

            ChatUser author = Context.Sender;

            List<UserRight> authorRights = await author.GetRightsAsync(guild);
            if (authorRights.Contains(UserRight.Owner))
            {
                return true;
            }
            else
            {
                List<UserRight> targetRights = await Target.GetRightsAsync(guild);
                return authorRights.Contains(UserRight.Administrator) & !targetRights.Contains(UserRight.Administrator);
            }
        }

        public override async Task Action()
        { 
            await Context.AnswerAsync("Not enough rights to run the command.", null, true);
        }
    }
}
