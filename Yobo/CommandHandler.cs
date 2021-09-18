using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;

namespace Yobo
{
    public class CommandHandler
    {
        private DiscordSocketClient client;

        private CommandService service;

        public CommandHandler(DiscordSocketClient _client)
        {
            client = _client;

            service = new CommandService();

            service.AddModulesAsync(Assembly.GetEntryAssembly());

            client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null)
            {
                return;
            }

            var context = new SocketCommandContext(client, msg);

            int argPos = 0;
            if (msg.HasCharPrefix('/', ref argPos))
            {
                var result = await service.ExecuteAsync(context, argPos);

                /*if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {

                }*/

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}
