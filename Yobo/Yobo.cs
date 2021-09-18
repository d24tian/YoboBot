using Discord;
using Discord.Commands;
using Discord.WebSocket;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MurderBot
{
    class Yobo
    {
        DiscordSocketClient client;
        CommandService commands;

        Random rand = new Random();

        List<string> users = new List<string>();

        List<ulong> roles = new List<ulong>();
        ulong[] rolesArray = new ulong[]
        {
            339771450711998464,
            339771255391780865,
            339771354045874177,
            339771362778284033,
            339771375571173376,
            339771383099817985,
            339771391383699458,
            339771399071727618,
            339771406789246976,
            339771413747597312,
            339771420949348363,
            339789212259844096,
            339789225933275136,
            339789236872019978,
            339789246871371777,
            339789260154470412,
            339789280018956298,
            339789292262129666,
            339789303276240896,
            339789314340814858,
            339789326747435027
        };

        public Yobo()
        {
            roles.AddRange(rolesArray);
            
            //Logging
            client = new DiscordSocketClient(input =>
            {
                input.LogLevel = LogSeverity.Info;
                input.LogHandler = Log;
            });

            //Setting up commands
            client.UsingCommands(input =>
            {
                input.PrefixChar = '/';
                input.AllowMentionPrefix = true;
            });

            commands = client.GetService<CommandService>();

            //Commands

            commands.CreateCommand("purge").Parameter("amount", ParameterType.Required)
                .Do(async (e) =>
                {
                    Message[] messagesToDelete;
                    messagesToDelete = await e.Channel.DownloadMessages(Convert.ToInt32(e.GetArg("amount")) + 1);

                    await e.Channel.DeleteMessages(messagesToDelete);
                });

            commands.CreateCommand("announce")
                .Parameter("channel", ParameterType.Required)
                .Parameter("message", ParameterType.Multiple)
                .Do(async (e) =>
                {
                    var userRoles = e.User.Roles;
                    if (userRoles.Any(input => input.Name.ToUpper() == "MANAGEMENT"))
                    {
                        await DoAnnouncement(e);
                    }
                    else
                    {
                        await e.User.SendMessage("You do not have permission to use this command!");
                    }
                });

            commands.CreateCommand("setnick")
                .Description("Changes, or sets, the nickname of the specified user.")
                .Parameter("user")
                .Parameter("newname", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    var user = e.Channel.FindUsers(e.GetArg("user")).FirstOrDefault();
                    string newname = e.GetArg("newname");
                    await user.Edit(nickname: newname).ConfigureAwait(false);
                });

            //GAME GAME GAME GAME GAME GAME GAME GAME GAME GAME GAME GAME GAME GAME GAME GAME GAME GAME

            commands.CreateCommand("start")
                .Do(async (e) =>
                {
                    users.Add(e.User.Name);

                    Channel channel = await e.Server.CreateChannel(e.User.Name.ToString(), ChannelType.Text);

                    var eOverwrite = new ChannelPermissionOverrides(sendMessages: PermValue.Deny, readMessages: PermValue.Deny);
                    var everyone = e.Server.EveryoneRole;
                    await channel.AddPermissionsRule(everyone, eOverwrite);

                    var overwrite = new ChannelPermissionOverrides(sendMessages: PermValue.Allow, readMessages: PermValue.Allow);
                    await channel.AddPermissionsRule(e.User, overwrite);

                    await e.User.AddRoles(e.Server.GetRole(roles[0]));

                    await e.Channel.SendMessage("Game started for " + e.User.Name + " :white_check_mark:");
                });

            commands.CreateCommand("talk")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Usage: \n `/talk [target]'");
                    /*var embed = new EmbedBuilder()
                        .WithAuthor(user)
                        .WithColor(Color.Gold)
                        .WithDescription("Usage: \n `/talk [target]'");*/
                });

            commands.CreateCommand("talk")
                .Parameter("target", ParameterType.Required)
                .Do(async (e) =>
                {
                    string fam = e.GetArg("target").ToLower();

                    if (fam == "shade")
                    {
                        if (e.User.HasRole(e.Server.GetRole(roles[0])))
                        {
                            await e.Channel.SendMessage("Shade: 'It wasn't me, I swear, was the one with the sack.'");
                            await RemoveRole(e, 0);
                            await AddRole(e, 1);
                        }
                        else
                        {
                            await e.Channel.SendMessage("...");
                        }
                    }
                    else if (fam == "olxa")
                    {
                        if (e.User.HasRole(e.Server.GetRole(roles[1])))
                        {
                            await e.Channel.SendMessage("Olxa: Ib! Uy ricgye mo!"); 
                        }
                        else
                        {
                            await e.Channel.SendMessage("...");
                        }
                    }
                    else if (fam == "gobby")
                    {
                        if (e.User.HasRole(e.Server.GetRole(roles[2])) || e.User.HasRole(e.Server.GetRole(roles[3])) || e.User.HasRole(e.Server.GetRole(roles[4])) || e.User.HasRole(e.Server.GetRole(roles[5])))
                        {
                            await e.Channel.SendMessage("Gobby: FZZ A PFKM AD GT VFSX AV F VSPMGFQAS! VMWAEIVZT! TEI SFD IVM /VFSX QE KAML AQ!");
                        }
                        else if (e.User.HasRole(e.Server.GetRole(roles[1])))
                        {
                            await e.Channel.SendMessage("Gobby: Uy ric yqiy Jzwwb huxiyo! Gdyqug ug eb cipv!");
                            await RemoveRole(e, 1);
                            await AddRole(e, 2);
                        }
                        else
                        {
                            await e.Channel.SendMessage("...");
                        }
                    }
                    else if (fam == "bully")
                    {
                        if (e.User.HasRole(e.Server.GetRole(roles[14])))
                        {
                            await e.Channel.SendMessage("Blimey, to be sure! I already told ye that bloody purple kangaroo did it. I have no reason t' lie. Did he say I were bein' stealin' his gold? The ornery cuss's a liar! The ornery cuss can shape shift fer cryin' out loud. ye dern't have th' slightest suspicion that it were bein' that scurvey dog? \n\nOi! If ye want t' talk t' anyone it be that Squib, I heard he has a cousin that knows a lot about thin's., pass the grog! maybe he knows a thin' or two fer whatever ye lookin' fer.");
                            await RemoveRole(e, 14); 
                            await AddRole(e, 15);
                        }
                        else if (e.User.HasRole(e.Server.GetRole(roles[2])))
                        {
                            await e.Channel.SendMessage("Bully: Shineh gold, blood red chezt, purple dancin' kangroo.");
                            await RemoveRole(e, 2);
                            await AddRole(e, 3);
                        }
                        else
                        {
                            await e.Channel.SendMessage("...");
                        }
                    }
                    else if (fam == "mimzy")
                    {
                        if (e.User.HasRole(e.Server.GetRole(roles[13])))
                        {
                            await e.Channel.SendMessage("Mimzy already told you! Mimzy no kill!! Mimzy no harm!! Mimzy want treasure! Bully steal Mimzy treasure!");
                            await RemoveRole(e, 13);
                            await AddRole(e, 14);
                        }
                        else if (e.User.HasRole(e.Server.GetRole(roles[3])))
                        {
                            await e.Channel.SendMessage("Mimzy: You think Mimzy kill? Mimzy no kill, rock man kill!");
                            await RemoveRole(e, 3);
                            await AddRole(e, 4);
                        }
                        else
                        {
                            await e.Channel.SendMessage("...");
                        }
                    }
                    else if (fam == "gemm")
                    {
                        if (e.User.HasRole(e.Server.GetRole(roles[4])))
                        {
                            await e.Channel.SendMessage("Gemm: FLEX, NOW FLEX S'MORE! These arms ain't for killin, they fo flexin!");
                        }
                        else
                        {
                            await e.Channel.SendMessage("...");
                        }
                    }
                    else if (fam == "ragnar")
                    {
                        if (e.User.HasRole(e.Server.GetRole(roles[4])))
                        {
                            await e.Channel.SendMessage("Ragnar: Come on, you think it's me? I ain't the only rock man around. Maybe it's Kaleido or Woodbeard, none of them raid bosses like each other.");
                            await RemoveRole(e, 4);
                            await AddRole(e, 5);
                        }
                        else
                        {
                            await e.Channel.SendMessage("...");
                        }
                    }
                    else if (fam == "woodbeard")
                    {
                        if (e.User.HasRole(e.Server.GetRole(roles[7])))
                        {
                            await e.Channel.SendMessage("Woodbeard: Ahoy, Gobb-! Y'er not gobbay! What'r you doin on ma ship? Whatever it is ye seek, mate, don't ask me, ask that crummay ol' Krackers. He think's he knows erythin anyway!");
                            await RemoveRole(e, 7);
                            await AddRole(e, 8);
                        }
                        else if (e.User.HasRole(e.Server.GetRole(roles[5])))
                        {
                            await e.Channel.SendMessage("Woodbeard: Ye tink it me? Ye rilly tink it me?? It be Kaleido, he hate Asta!");
                        }
                        else
                        {
                            await e.Channel.SendMessage("...");
                        }
                    }
                    else if (fam == "krackers")
                    {
                        if (e.User.HasRole(e.Server.GetRole(roles[8])))
                        {
                            await e.Channel.SendMessage("Krackers: What ye' need, Ye slimey sea slurp? A key? To what? Well.. technically, I could give ye a key to this here Woodbeard's cabin. Ye door is all the way to the end of the corridor, mate, last door to the roight. Now hurry on, Blimey! Before that Woodbeard catches ye. Use /cabinkey to claim the key.");
                            await RemoveRole(e, 8);
                            await AddRole(e, 9);
                        }
                        else
                        {
                            await e.Channel.SendMessage("...");
                        }
                    }
                    else if (fam == "booty")
                    {
                        if (e.User.HasRole(e.Server.GetRole(roles[10])))
                        {
                            await e.Channel.SendMessage("Woodbeard had a little pet, of his own. It was a Booty. This little blue bat loved to hop around the entire realm of the Bit Heroes. Along his way, he even picked up a little portal that produced a lot of energy. /hyperactivate to activate.");
                            await RemoveRole(e, 10);
                            await AddRole(e, 11);
                        }
                        else
                        {
                            await e.Channel.SendMessage("...");
                        }
                    }
                    else if (fam == "kaleido")
                    {
                        if (e.User.HasRole(e.Server.GetRole(roles[12])))
                        {
                            await e.Channel.SendMessage("What are you doing here? Did two worthy opponents come to fight me, as their new leader from the fall of Astaroth? Wait, you think I killed him? Now why the hyper hole would I do that? Look. If there's anyone out there thats mischevious enough to do that its that shape shifter Mimzy. Now get out of here! Unless you want to be eaten or sliced in a million pieces!");
                            await RemoveRole(e, 12);
                            await AddRole(e, 13);
                        }
                        else if (e.User.HasRole(e.Server.GetRole(roles[5])))
                        {
                            await e.Channel.SendMessage("Wasn't me bro.");
                        }
                        else
                        {
                            await e.Channel.SendMessage("...");
                        }
                    }
                    else if (fam == "squib")
                    {
                        if (e.User.HasRole(e.Server.GetRole(roles[17])))
                        {
                            await e.Channel.SendMessage("Really? I've told you to stop talkin to me so many times, god, you're starting to annoy me, if I tell you will you stop? Alright fine, I've heard that everyone in raid two REALLY hates us raid one peeps. Talk to my cousin Mer'lan, he might know somethin.");
                            await RemoveRole(e, 17);
                            await AddRole(e, 18);
                        }
                        else if (e.User.HasRole(e.Server.GetRole(roles[16])))
                        {
                            await e.Channel.SendMessage("Why have you come back? I told you to leave! Fine. I guess i'll have to make you. Abra Cadabra! Ow.. that hurt my brain cells. You know what? I'm not smart at all.. please leave before you harm me again.");
                            await RemoveRole(e, 16);
                            await AddRole(e, 17);
                        }
                        else if (e.User.HasRole(e.Server.GetRole(roles[15])))
                        {
                            await e.Channel.SendMessage("I don't know anything for what you puny mortals are looking for. My cousin, Mer'lan, knows more about your silly arguments than I do. Now shoo, before I have to make you.");
                            await RemoveRole(e, 17);
                            await AddRole(e, 16);
                        }
                        else
                        {
                            await e.Channel.SendMessage("...");
                        }
                    }
                    else if (fam == "merlan")
                    {
                        if (e.User.HasRole(e.Server.GetRole(roles[18])))
                        {
                            await e.Channel.SendMessage("What do you want from me? I have no time for you. Actually.. my doctors appointment is actually an hour from now. I only have time for one of your dumb human questions. Now what is it you seek? You want the knowledge to who killed Astaroth? Ha.. ha... only I know who did it. Now, i can only give you a clue. It is simple. It is a being, not a disease. It could be around us at any time. Jump us. We would not even know it. That is all I will say. Please let me leave... Move... Why aren't you letting me leave? Alright fine, someone called Yeti is hiding something...");
                            await RemoveRole(e, 18);
                            await AddRole(e, 19);
                        }
                        else
                        {
                            await e.Channel.SendMessage("...");
                        }
                    }

                    await CheckRole(e);
                });

            commands.CreateCommand("sack")
                .Do(async (e) =>
                {
                    if (e.User.HasRole(e.Server.GetRole(roles[2])) || e.User.HasRole(e.Server.GetRole(roles[3])) || e.User.HasRole(e.Server.GetRole(roles[4])) || e.User.HasRole(e.Server.GetRole(roles[5])))
                    {
                        await e.Channel.SendMessage("You dig around Gobby's bag. It has a Robby schematic, which has a weird code that appears to be binary. /robby to view the code");
                        await RemoveRole(e, 2);
                        await RemoveRole(e, 3);
                        await RemoveRole(e, 4);
                        await RemoveRole(e, 5);
                        await AddRole(e, 6);
                    }
                    
                    await CheckRole(e);
                });

            commands.CreateCommand("robby")
                .Do(async (e) =>
                {
                    if (e.User.HasRole(e.Server.GetRole(roles[6])))
                    {
                        await e.Channel.SendMessage("01000111 01101111 00100000 01110100 01101111 00100000 01010111 01101111 01101111 01100100 01100010 01100101 01100001 01110010 01100100 00100000 01100110 01101111 01110010 00100000 01101001 01101110 01110011 01110100 01100001 01101100 01101100 01101101 01100101 01101110 01110100 01110011 00101110 00100000 01010100 01100001 01101100 01101011 00100000 01110100 01101111 00100000 01010111 01101111 01101111 01100100 01100010 01100101 01100001 01110010 01100100 00101110");
                        await RemoveRole(e, 6);
                        await AddRole(e, 7);
                    }

                    await CheckRole(e);
                });

            commands.CreateCommand("cabinkey")
                .Do(async (e) =>
                {
                    if (e.User.HasRole(e.Server.GetRole(roles[9])))
                    {
                        await e.Channel.SendMessage("Cabin Key acquired! You now have access to the #woodbeards_cabin channel.");
                        await RemoveRole(e, 9);
                        await AddRole(e, 10);
                        await e.User.AddRoles(e.Server.GetRole(339145788414230538));
                    }

                    await CheckRole(e);
                });

            commands.CreateCommand("hyperactivate")
                .Do(async (e) =>
                {
                    if (e.User.HasRole(e.Server.GetRole(roles[11])))
                    {
                        await e.Channel.SendMessage("A pink goat? No, goats do not walk on two legs. A human? No, it's far too tall. This creature is neither a human nor a goat, yet it resembles both. You should try talking to him!");
                        await RemoveRole(e, 11);
                        await AddRole(e, 12);
                    }

                    await CheckRole(e);
                });

            commands.CreateCommand("reset")
                .Parameter("user")
                .Do(async (e) =>
                {
                    var user = e.Channel.FindUsers(e.GetArg("user")).FirstOrDefault();

                    for (int i = 0; i < 20; i++)
                    {
                        if (user.HasRole(e.Server.GetRole(roles[i])))
                        {
                            await user.RemoveRoles(e.Server.GetRole(roles[i]));
                        }
                    }
                });

            client.ExecuteAndWait(async () =>
            {
                await client.Connect("MzM5NTQ4MzYyOTYzNjgxMjgy.DFlkkQ.zpOIZnOs-av30NsuDWbR5UZXYBE", TokenType.Bot);
            });
        }

        private async Task SetPermissions(CommandEventArgs e, string channelModified, string userChanged, string permissionType, bool changeTo)
        {
            var channel = e.Server.FindChannels(channelModified).FirstOrDefault();
            var user = e.Server.FindUsers(userChanged).FirstOrDefault();

            if (permissionType == "sendMessages")
            {
                if (changeTo)
                {
                    var overwrite = new ChannelPermissionOverrides(sendMessages: PermValue.Allow);
                    await channel.AddPermissionsRule(user, overwrite);
                }
                else if (changeTo == false)
                {
                    var overwrite = new ChannelPermissionOverrides(sendMessages: PermValue.Deny);
                    await channel.AddPermissionsRule(user, overwrite);
                }
            }
            if (permissionType == "readMessages")
            {
                if (changeTo)
                {
                    var overwrite = new ChannelPermissionOverrides(readMessages: PermValue.Allow);
                    await channel.AddPermissionsRule(user, overwrite);
                }
                else if (changeTo == false)
                {
                    var overwrite = new ChannelPermissionOverrides(readMessages: PermValue.Deny);
                    await channel.AddPermissionsRule(user, overwrite);
                }
            }
        }

        private async Task AddRole(CommandEventArgs e, int index)
        {
            await e.User.AddRoles(e.Server.GetRole(roles[index]));
        }

        private async Task RemoveRole(CommandEventArgs e, int index)
        {
            if (e.User.HasRole(e.Server.GetRole(roles[index])))
            {
                await e.User.RemoveRoles(e.Server.GetRole(roles[index]));
            }
        }

        private async Task CheckRole(CommandEventArgs e)
        {
            for (int i = 20; i > 0; i--)
            {
                if (e.User.HasRole(e.Server.GetRole(roles[i])))
                {
                    for (int a = i - 1; a > -1; a--)
                    {
                        if (e.User.HasRole(e.Server.GetRole(roles[a])))
                        {
                            await e.User.RemoveRoles(e.Server.GetRole(roles[a]));
                        }
                    }
                }
            }
        }

        private async Task DoAnnouncement(CommandEventArgs e)
        {
            var channel = e.Server.FindChannels(e.GetArg("channel"), ChannelType.Text).FirstOrDefault();
            var message = e.GetArg("message");

            if (channel != null)
            {
                await channel.SendMessage(message);
            }
            else
            {
                await e.Channel.SendMessage(message);
            }
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
