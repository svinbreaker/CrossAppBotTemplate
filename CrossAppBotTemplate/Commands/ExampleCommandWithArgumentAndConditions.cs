using CrossAppBotTemplate.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СrossAppBot.Commands;
using СrossAppBot.Entities;

namespace CrossAppBotTemplate.Commands
{
    public class ExampleCommandWithArgumentAndConditions : AbstractCommand
    {

        [CommandArgument("User", "User to be mentioned")]
        public ChatUser? Target { get; set; }

        public ExampleCommandWithArgumentAndConditions() : base("mention", "Mention command") { }

        public override void Conditions()
        {
            Condition(new IsGroupChatCommandCondition(Context));
            Condition(new UserExistCommandCondition(Context, Target));
        }

        protected override async Task Executee()
        {
            await Context.AnswerAsync(Context.Client.Mention(Target), null, true);
        }
    }
}
