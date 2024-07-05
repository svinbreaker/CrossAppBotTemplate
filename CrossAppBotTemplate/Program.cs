using CrossAppBotTemplate.Commands;
using Newtonsoft.Json;
using System.Xml.Linq;
using СrossAppBot;
using СrossAppBot.Commands;
using СrossAppBot.Entities;
using СrossAppBot.Events;

namespace CrossAppBotTemplate
{
    class Program
    {
        public static string ProjectPath
        {
            get
            {
                return System.AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\net8.0", "");
            }
        }
        public static string DataPath
        {
            get
            {
                return ProjectPath + @"Data\";
            }
        }
        private static string _botDataFile = DataPath + $@"botsInfo.json";

        public static List<AbstractBotClient> bots { get; set; }


        public static Task Main(string[] args) => new Program().MainAsync();

        async Task MainAsync()
        {
            await InitializeBots();
        }

        private async Task InitializeBots()
        {
            dynamic botsData = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(_botDataFile));

            //Comment out the lines for creating those bots that are not needed
            VkBotClient vkBot = new VkBotClient(Convert.ToString(botsData.vk.token), ulong.Parse(Convert.ToString(botsData.vk.groupId)));
            TelegramBotClient telegramBot = new TelegramBotClient(Convert.ToString(botsData.telegram.token), DataPath + "/Telegram/telegramUsers.json");
            DiscordBotClient discordBot = new DiscordBotClient(Convert.ToString(botsData.discord.token));

            bots = new List<AbstractBotClient> { /*discordBot, vkBot, telegramBot */};

            //Comment out the lines for adding those commands that are not needed
            List<AbstractCommand> universalCommands = new List<AbstractCommand>
            {
                new ExampleCommand(),
                new ExampleCommandWithArgumentAndConditions()
            };
            List<AbstractCommand> emojiCommands = new List<AbstractCommand>
            {
                new ExampleCommandEmojiable()
            };

            List<IArgumentParser> parsers = new List<IArgumentParser>()
            {

            };

            //Comment out the lines for adding those event listeners that are not needed
            foreach (AbstractBotClient bot in bots)
            {
                bot.EventManager.Subscribe<BotConnectedEvent>(OnConnect, RegisterSlashCommands);
                bot.EventManager.Subscribe<BotDisconnectedEvent>(OnDisconnect);
                bot.EventManager.Subscribe<MessageReceivedEvent>(OnMessageReceived);
                bot.EventManager.Subscribe<MessageEditedEvent>(OnMessageEdited);
                //etc.

                bot.CommandManager.SetTextCommandHelper("e.", parsers);

                bot.CommandManager.AddCommands(universalCommands);
                if (bot is IAddReaction)
                {                  
                    bot.CommandManager.AddCommands(emojiCommands);
                }


            }

            await Task.WhenAll(telegramBot.StartAsync(), discordBot.StartAsync(), vkBot.StartAsync());
        }

        private async Task OnConnect(BotConnectedEvent clientEvent)
        {
            Console.WriteLine("Connected to " + clientEvent.Client.Name);
        }

        private async Task OnDisconnect(BotDisconnectedEvent clientEvent)
        {
            Console.WriteLine("Disconnected from " + clientEvent.Client.Name);
        }

        private async Task OnMessageReceived(MessageReceivedEvent clientEvent)
        {

        }

        public async Task OnMessageEdited(MessageEditedEvent clientEvent)
        {

        }

        //etc.

        private async Task RegisterSlashCommands(BotConnectedEvent clientEvent)
        {
            AbstractBotClient bot = clientEvent.Client;

            if (bot is ISlashable slashable)
            {
                await slashable.RegisterSlashCommands(bot.CommandManager.Commands);
            }
        }
    }
}