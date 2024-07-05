using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using СrossAppBot;
using СrossAppBot.Commands;
using СrossAppBot.Entities;

namespace CrossAppBotTemplate.Commands
{
    public class ExampleHelpCommand : AbstractCommand
    {

        public ExampleHelpCommand() : base("help", "Display a list of all commands") { }

        protected override async Task Executee()
        {
            AbstractBotClient client = Context.Client;
            string prefix = client.CommandManager.TextCommandHelper.Prefix;
            string result;
            StringBuilder builder = new StringBuilder();
            builder.Append($"List of commands:\n\n");
            foreach (AbstractCommand command in client.CommandManager.Commands)
            {
                builder.Append($"{prefix}{command.Name}");
                foreach (PropertyInfo property in command.GetType().GetProperties().Where(property => property.IsDefined(typeof(CommandArgumentAttribute), false)).ToList())
                {
                    builder.Append($" [{property.GetCustomAttribute<CommandArgumentAttribute>().Name}] ");
                }
                builder.Append($" - {command.Description}\n");
            }
            result = builder.ToString();

            await Context.AnswerAsync(result, null, true);
        }
    }
}
