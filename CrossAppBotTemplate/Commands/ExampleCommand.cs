using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СrossAppBot;
using СrossAppBot.Commands;

namespace CrossAppBotTemplate.Commands
{
    public class ExampleCommand : AbstractCommand
    {
        public ExampleCommand() : base("test", "Test command") { }

        protected override async Task Executee()
        {
            await Context.AnswerAsync("Test!", null, true);
        }
    }
}
