using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СrossAppBot.Commands;
using СrossAppBot.Entities;

namespace CrossAppBotTemplate.Conditions
{
    public class UserExistCommandCondition : AbstractCommandCondition
    {
        private CommandContext Context { get; }
        private ChatUser Target { get; }

        public UserExistCommandCondition(CommandContext context, ChatUser target)
        {
            Context = context;
            Target = target;
        }

        public override async Task<bool> Condition()
        {
            return Target != null;
        }

        public override async Task Action()
        {          
            await Context.AnswerAsync("This user does not exist.", null, true);
        }
    }
}
