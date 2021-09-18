using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using static MurderBot.Program;

namespace Yobo.Modules
{
    public class yobo : ModuleBase<SocketCommandContext>
    {

        [Command("hello")]
        public async Task hello()
        {
            await Context.Channel.SendMessageAsync("Hi!");
        }

        [Command("purge")]
        public async Task purge(int amount)
        {
            var messagesToDelete = await Context.Channel.GetMessagesAsync(amount + 1).Flatten();
            
            await Context.Channel.DeleteMessagesAsync(messagesToDelete);
        }

        [Command("list")]
        public async Task list()
        {
            string message = "";

            if (players.Count == 0)
            {
                await Context.Channel.SendMessageAsync("No one is playing right now.");
            }
            else
            {
                foreach (var man in players)
                {
                    await Context.Channel.SendMessageAsync(man.ToString());
                    message += man.ToString() + "\n";
                }
                await Context.Channel.SendMessageAsync(message);
            }
        }

        [Command("start")]
        public async Task start()
        {
            var channel = await Context.Guild.CreateTextChannelAsync(Context.User.Username);
            channels.Add(channel);

            var user = Context.User;

            var overwrite = new OverwritePermissions(sendMessages: PermValue.Allow, readMessages: PermValue.Allow);
            await channel.AddPermissionOverwriteAsync(user, overwrite);

            var eOverwrite = new OverwritePermissions(sendMessages: PermValue.Deny, readMessages: PermValue.Deny);
            await channel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, eOverwrite);

            AddPlayers(Context.User.Username, 0);

            await Context.Channel.SendMessageAsync("Game started for " + Context.User.Username + " :white_check_mark:");

            await channel.SendMessageAsync("Do /talk shade to begin! Use /finish at any time to end the game.");
        }

        [Command("talk")]
        public async Task talk(string target)
        {
            int location = RefreshTarget(Context.User.Username);

            if (target == null)
            {
                await Context.Channel.SendMessageAsync("Usage:\n/talk [target]");
            }

            if (target.ToLower() == "shade")
            {
                if (players[location].status == 77)
                {
                    await Context.Channel.SendMessageAsync("Shade: T'was not me, I said. It was the one with the sack! That is all I know. My orb is missing. You have my orb! I shall read the future for you, mortal. Nosdoodoo is in this orb... He is trying to eat Jeb! Jeb is fighting back. Jeb slices Nosdoodoo. Nosdoodoo is dying. Go to Nosdoodoo! Nosdoodoo knows something!");
                    players[location].status = 78;
                }
                else if (players[location].status == 0)
                {
                    await Context.Channel.SendMessageAsync("Shade: It wasn't me, I swear, it was the one with the sack.");
                    players[location].status = 1;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "olxa")
            {
                if (players[location].status == 1)
                {
                    await Context.Channel.SendMessageAsync("Olxa: Ib! Uy ricgye mo!");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "gobby")
            {
                if (players[location].status == 2 || players[location].status == 3 || players[location].status == 4 || players[location].status == 5)
                {
                    await Context.Channel.SendMessageAsync("Gobby: FZZ A PFKM AD GT VFSX AV F VSPMGFQAS! VMWAEIVZT! TEI SFD IVM /VFSX QE KAML AQ!");
}
                else if (players[location].status == 1)
                {
                    await Context.Channel.SendMessageAsync("Gobby: Uy ric yqiy Jzwwb huxiyo! Gdyqug ug eb cipv!");
                    players[location].status = 2;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "bully")
            {
                if (players[location].status == 14)
                {
                    await Context.Channel.SendMessageAsync("Blimey, to be sure! I already told ye that bloody purple kangaroo did it. I have no reason t' lie. Did he say I were bein' stealin' his gold? The ornery cuss's a liar! The ornery cuss can shape shift fer cryin' out loud. ye dern't have th' slightest suspicion that it were bein' that scurvey dog? \n\nOi! If ye want t' talk t' anyone it be that Squib, I heard he has a cousin that knows a lot about thin's. Pass the grog! Maybe he knows a thin' or two fer whatever ye lookin' fer.");
                    players[location].status = 15;
                }
                else if (players[location].status == 2)
                {
                    await Context.Channel.SendMessageAsync("Bully: Shineh gold, blood red chezt, purple dancin' kangroo.");
                    players[location].status = 3;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "mimzy")
            {
                if (players[location].status == 38)
                {
                    await Context.Channel.SendMessageAsync("Mimzy: Mimzy no kill! Mimzy has told you! Mimzy has mercy! Mimzy no hurt! Kaleido lie! Mimzy swears! Mimzy says Dryad casts spell on Astaroth. Mimzy believes Mimzy.");
                    players[location].status = 39;
                }
                else if (players[location].status == 13)
                {
                    await Context.Channel.SendMessageAsync("Mimzy: Mimzy already told you! Mimzy no kill!! Mimzy no harm!! Mimzy want treasure! Bully steal Mimzy treasure!");
                    players[location].status = 14;
                }
                else if (players[location].status == 3)
                {
                    await Context.Channel.SendMessageAsync("Mimzy: You think Mimzy kill? Mimzy no kill, rock man kill!");
                    players[location].status = 4;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "gemm")
            {
                if (players[location].status == 4)
                {
                    await Context.Channel.SendMessageAsync("Gemm: FLEX, NOW FLEX S'MORE! These arms ain't for killin, they fo flexin!");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "ragnar")
            {
                if (players[location].status == 4)
                {
                    await Context.Channel.SendMessageAsync("Ragnar: Come on, you think it's me? I ain't the only rock man around. Maybe it's Kaleido or Woodbeard, none of them raid bosses like each other.");
                    players[location].status = 5;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "woodbeard")
            {
                if (players[location].status == 7)
                {
                    await Context.Channel.SendMessageAsync("Woodbeard: Ahoy, Gobb-! Y'er not gobbay! What'r you doin on ma ship? Whatever it is ye seek, mate, don't ask me, ask that crummay ol' Krackers. He think's he knows erythin anyway!");
                    players[location].status = 8;
                }
                else if (players[location].status == 5)
                {
                    await Context.Channel.SendMessageAsync("Woodbeard: Ye tink it me? Ye rilly tink it me?? It be Kaleido, he hate Asta!");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "krackers")
            {
                if (players[location].status == 8)
                {
                    await Context.Channel.SendMessageAsync("Krackers: What ye' need, Ye slimey sea slurp? A key? To what? Well... Technically, I could give ye a key to this here Woodbeard's cabin. Ye door is all the way to the end of the corridor, mate, last door to the roight. Now hurry on, Blimey! Before that Woodbeard catches ye. Use /cabinkey to claim the key.");
                    players[location].status = 9;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "booty")
            {
                if (players[location].status == 10)
                {
                    await Context.Channel.SendMessageAsync("Woodbeard had a little pet, of his own. It was a Booty. This little blue bat loved to hop around the entire realm of the Bit Heroes. Along his way, he even picked up a little portal that produced a lot of energy. /hyperactivate to activate.");
                    players[location].status = 11;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "kaleido")
            {
                if (players[location].status == 54)
                {
                    await Context.Channel.SendMessageAsync("Kaleido: I did want to use, Sunder. But everyone is taking advantage of Astaroth. His gear is... Enchanted. We don't know what is enchanted, how it's enchanted, we don't know anything. I wanted to be more powerful and crit more, which is why I wanted to fuse weapons. Heh, I'm not the only one. Why don't ya' ask Blubber why he took his helmet and immortal resolve? That blob of slime doesn't want to dry out, and he's behind something.");
                    players[location].status = 55;
                }
                else if (players[location].status == 37)
                {
                    await Context.Channel.SendMessageAsync("Kaleido: Roar!! I told you I have no reason to kill Astaroth! Sure I hated him, but everyone else did! He stole my thunder! I told you, it was that pesky Mimzy that did it! He could have disguised as anyone and killed him to frame people! I swear, it wasn't me! Stop blaming me!");
                    players[location].status = 38;
                }
                else if (players[location].status == 12)
                {
                    await Context.Channel.SendMessageAsync("Kaleido: What are you doing here? Did two worthy opponents come to fight me, as their new leader from the fall of Astaroth? Wait, you think I killed him? Now why the hyper hole would I do that? Look. If there's anyone out there thats mischevious enough to do that its that shape shifter Mimzy. Now get out of here! Unless you want to be eaten or sliced in a million pieces!");
                    players[location].status = 13;
                }
                else if (players[location].status == 5)
                {
                    await Context.Channel.SendMessageAsync("Kaleido: Wasn't me bro.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "squib")
            {
                if (players[location].status == 17)
                {
                    await Context.Channel.SendMessageAsync("Squib: Really? I've told you to stop talkin to me so many times, god, you're starting to annoy me, if I tell you will you stop? Alright fine, I've heard that everyone in raid two REALLY hates us raid one peeps. Talk to my cousin Mer'lan, he might know somethin.");
                    players[location].status = 18;
                }
                else if (players[location].status == 16)
                {
                    await Context.Channel.SendMessageAsync("Squib: Why have you come back? I told you to leave! Fine. I guess i'll have to make you. Abra Cadabra! Ow... That hurt my brain cells. You know what? I'm not smart at all... Please leave before you harm me again.");
                    players[location].status = 17;
                }
                else if (players[location].status == 15)
                {
                    await Context.Channel.SendMessageAsync("Squib: I don't know anything for what you puny mortals are looking for. My cousin, Mer'lan, knows more about your silly arguments than I do. Now shoo, before I have to make you.");
                    players[location].status = 16;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "merlan")
            {
                if (players[location].status == 30)
                {
                    await Context.Channel.SendMessageAsync("Merlan: Why have you come back? I have told you I only have time for only one of your Astaroth cursed questions! Unless you have something I want, you better not come here again. Woah wait! Why... You have Arcanot! Thank you so much, brave adventurer! I can answer any question, or give a clue to which you might desire. What? I don't know who killed Astaroth... Sorry about that, adventurer. But, with this new clue, I can give you an idea to who the killer is. Hmm..all I see is something red. Something is shining inside of it. Something seems to be guarding it, inside or outside of it.. that is all I can do for now adventurer. I will now go back to my slumber. Oh, and there's one more thing. Ask Bebemenz. He has been communicating with Kaleido, I have just learned. Talk to him to make sure nothing funky goes down. He's a trickster...");
                    players[location].status = 31;
                }
                else if (players[location].status == 16 || players[location].status == 17 || players[location].status == 18)
                {
                    await Context.Channel.SendMessageAsync("Merlan: What do you want from me? I have no time for you. Actually... My doctors appointment is actually an hour from now. I only have time for one of your dumb human questions. Now what is it you seek? You want the knowledge to who killed Astaroth? Ha... Ha... Only I know who did it. Now, i can only give you a clue. It is simple. It is a being, not a disease. It could be around us at any time. Jump us. We would not even know it. That is all I will say. Please let me leave... Move... Why aren't you letting me leave? Alright fine, someone called Yeti is hiding something, do /tundra go meet him");
                    players[location].status = 19;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "yeti")
            {
                if (players[location].status == 20)
                {
                    await Context.Channel.SendMessageAsync("Yeti: Oi! What are you doing here, ice hole? I'm surprised you've made it this far. Can I help you to some frozen spaghetti? No? Never mind. Anyway, how can I help you? Wait, you think I killed Astaroth! Hah! Why would I do that? Ask any of my peaceful inhabitants in my tundra.");
                    players[location].status = 21;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "mcgoblestein")
            {
                if (players[location].status == 21)
                {
                    await Context.Channel.SendMessageAsync("McGobblestein: Gobble gobble gobble. Gobble gobble? GOBBLE GOBBLE GOBBLE!! SKREEEE!");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "pengey")
            {
                if (players[location].status == 21)
                {
                    await Context.Channel.SendMessageAsync("Pengey: Yeti has been here the whole time. But someone here wasn't... Baumo. He's been doing some freaky stuff with Brute. I don't know what though. You can ask him, but you have to keep nagging him. He's tough as a rock.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "baumo")
            {
                if (players[location].status == 23)
                {
                    await Context.Channel.SendMessageAsync("Baumo: Stupid cretin! I'll give you what you're looking for. Talk to Brute. He knows something. I don't. Goodbye.");
                    players[location].status = 24;
                }
                else if (players[location].status == 22)
                {
                    await Context.Channel.SendMessageAsync("Baumo: I said leave! Do you really want to fight? My snowballs can easily turn to hail as they crack that puny skull of yours.");
                    players[location].status = 23;
                }
                else if (players[location].status == 21)
                {
                    await Context.Channel.SendMessageAsync("Baumo: Look at me. Could I have been able to kill Astaroth? All I am is a tree that has been snowed on. Good day.");
                    players[location].status = 22;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "brute")
            {
                if (players[location].status == 24)
                {
                    await Context.Channel.SendMessageAsync("Brute: Are you accusing me? Why would I kill Astaroth? I'm a sasquatch! I have enough fame. I do know something about olxa... Did you talk to him yet? He probably said he's done nothing. He's a flipping pack rat! How could you trust him so easily? Check that sack with /sack.");
                    players[location].status = 25;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "blubber")
            {
                if (players[location].status == 60)
                {
                    await Context.Channel.SendMessageAsync("Blubber: Oh sweet, you have the jar! Alright, now let me empty this real quick.. there we go! Now I put some of this slime in the jar, hehe! It sounds like someones farting! Now the shart joke really is funny. Well, anyway, heres the portal opening. Do /portal to enter!");
                    players[location].status = 61;
                }
                else if (players[location].status == 55)
                {
                    await Context.Channel.SendMessageAsync("Blubber: Smell that? I shar- oh. You've already been here. Did you notice anything, sexier about me? Let me practice my voice. Muahaha! I am your new lord and savior! Bow down to meh! Heh. Wait, what about this flame? It's Astaroth's. So what? Everyone else took stuff from him. But it does something weird... It opened some sort of ripple in the world when I shook it really hard. Kinda weird. You want to test it out? Not so fast, bubbo. Give me a big jar, like a T-Rex jar, and I'll open the portal up for you.");
                    players[location].status = 56;
                }
                else if (players[location].status == 26)
                {
                    await Context.Channel.SendMessageAsync("Blubber: Heh. Do you smell that? I sharted. Just kidding! Thats the normal smell of the sewers. How can I help you? What? You found goo on the side of a schematic..? Was it a Blargnar schematic? ...No? An Olxaroth schematic? I don't know anything about that. My good friend, Warty, might. We share the same sewer system, just a long, long distance away. Continue down the right and you should find him. He might know something.");
                    players[location].status = 27;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "warty")
            {
                if (players[location].status == 27)
                {
                    await Context.Channel.SendMessageAsync("Warty: Wuh? Who sent you ? You better have a good reason or you're going to get eaten like everyone else! Blubber sent you? Of course he did... Little prankster. Always being the nosy little glob that he is. Whats with the Olxaroth schemati- Hey, wait a second. Why is Blubbers sticky glue all over this thing? Oh, must be from that fight he had with Grimz the other day... Ask him whats going on with that, and maybe they can work this out.");
                    players[location].status = 28;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "grimz")
            {
                if (players[location].status == 28)
                {
                    await Context.Channel.SendMessageAsync("Grimz: I ain't no nothing. I am death. All I know is when people die and when people live. Kind of like modern day Santa. Bargz knows more than I do.");
                    players[location].status = 29;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "bargz")
            {
                if (players[location].status == 29)
                {
                    await Context.Channel.SendMessageAsync(" Bargz: Ahoy, to be sure! Why have ye come here? Aarrr! ye want t' be knowin' who keel-hauled Astaroth? Ohh, so ye're th' investigator that everyone's lookin' at. Anyway, how may I help ye, to be sure? Ah, th' killer, by Woodbeard's sword. ye may have gone t' Mer'lan before, but he be been desirin' this. Its an orb, which grants even more power t' look towards th' future, by Woodbeard's sword. I guarantee ye if ye give this t' that scurvey dog, he might be tellin' ye, or give ye a hint t' who th' killer may be.");
                    players[location].status = 30;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "bebemenz")
            {
                if (players[location].status == 36)
                {
                    await Context.Channel.SendMessageAsync("Bebemenz: Nyehehe! Do you have my jar? Yay! My jar! Oh boy, oh boy, oh boy! Alright. I promised to tell you. Kaleido has been waiting for this day to come for a long, long time. He has a bigger grudge on Astaroth than Mimzy on Bully. Kaleido is jealous that people farm Astaroth's raid more than his, because they don't get critted so easy. Less and less people visited Kaleido when Woodbeard came along. Now he is first. Now he has power! Go talk to him about this!");
                    players[location].status = 37;
                }
                else if (players[location].status == 31)
                {
                    await Context.Channel.SendMessageAsync("Bebemenz: Nyehehe... What do you want from me? Kaleido? I ain't got nothing to do with that ugly shepherd! He's a goat! I'm a baby! He could eat me for crying out loud, nyehehe! Well. If you have a bottle of T-Rex spit, i'll be happy to tell you everything. Don't ask, its a great moisturizer...");
                    players[location].status = 32;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "rexie")
            {
                if (players[location].status == 56)
                {
                    await Context.Channel.SendMessageAsync("Rexie: 	Rawr! Are you back so soon? I have no more jars.. I know it seems unlikely but I just gave it to Sammy. He said it makes really soft silk, and he uses it to make his bed and to wrap other things to eat. He's probably in his cave by now. I'm sure theres something he wants *other* than that jar.");
                    players[location].status = 57;
                }
                else if (players[location].status == 32)
                {
                    await Context.Channel.SendMessageAsync("Rexie: Rawr! What brings you to my cave? T-Rex spit? Who asked? Bebemenz? I would give it to you... But Zorg just got the last batch before I ran out of liquid. He ran down that way, if you run fast enough you might reach him in time... He's probably oiling his well earned abs of his... *sighs*");
                    players[location].status = 33;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "zorg")
            {
                if (players[location].status == 84)
                {
                    await Context.Channel.SendMessageAsync("Zorg: Zorg welcomes you back! Woof! How Zorg can help you? You want Quirrel feather? Zorg just got feather! Hehe, Zorg tickle himself with feather. Feather soft. You give Zorg 2 feather before.. so I give you one feather! Arf!");
                    players[location].status = 85;
                }
                if (players[location].status == 35)
                {
                    await Context.Channel.SendMessageAsync("Zorg: Zorg wait here for 30 minutes. You come back with Quirrel feather! Oh, joy! Zorg happy now. Here, jar! Goodbye! *bounces off with happy whimpers*.");
                    players[location].status = 36;
                }
                else if (players[location].status == 33)
                {
                    await Context.Channel.SendMessageAsync("Zorg: Ball? Are you a ball I can throw? Hmm.. you are squishy. No? You are not ball? Why are you here with Zorg? You want this jar of T-Rex liquid? But Zorg just got this jar! Zorg must not give it to you easily! Zorg want Quirrel feather from mask. Feather tickles. Zorg wait here. Come to me when you have feather!");
                    players[location].status = 34;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "quirrel")
            {
                if (players[location].status == 84)
                {
                    await Context.Channel.SendMessageAsync("Quirrel: No! You're not getting one of my feathers after what you have done to me! Now shoo! Before I will cast my spell! Don't look around hero, there are no mirrors! Go get it from someone else!");
                }
                if (players[location].status == 53)
                {
                    await Context.Channel.SendMessageAsync("Quirrel: Hello adventurer, what are you doing here? Hisss! Get that mirror away from me! Please! I'll tell you about Astaroth! I... I want to become stronger... I am very weak... No one does my dungeon anymore because they are too excited with raid one... Maybe if I become higher in power I will move to the start of Ashvale... Then people will acknowledge me... But it seems not. I have Sunder and Astaroth's Finger. The finger gives me more power, so I might be moved up to dungeon two in Ashvale... But I have failed. As for Sunder, Kaleido wanted it. Don't ask me... He said he could fuse it with his sword...");
                    players[location].status = 54;
                }
                else if (players[location].status == 34)
                {
                    await Context.Channel.SendMessageAsync("Quirrel: Hello, traveler. What do you need today? Why, you need a feather from my mask? That I cannot do, no no no! But, I can give this to you. It is a feather from the majestic Purple Eagle. It is the same feather that I put on my mask. Have a good day! Hehehe!");
                    players[location].status = 35;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "dryad")
            {
                if (players[location].status == 39)
                {
                    await Context.Channel.SendMessageAsync("Dryad: What brings you to my forest? Not to harm it, I presume? Ok good. Wait, what? I didn't cast a spell on Astaroth... Who told you? Mimzy? Pesky little Golum cousin.. they always lie. Golum love gold. They won't talk regularly, but i will give you this bow of Destiny. It is made out of solid natural gold. I have plenty, but the Golum do not know that. Take this, and give it to them. They will succumb to any riches and tell you what you want to know.");
                    players[location].status = 40;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "golum")
            {
                if (players[location].status == 40)
                {
                    await Context.Channel.SendMessageAsync("Golum: Golum? Golum Golum! That's pure gold!! If you give to me, I'll tell you what you want to hear! Gold, please! What? Who killed Astaroth? Well... I know this. All I know is that Lord Cerulean was with Astaroth before the day he died. Fishy, I know. But I don't think he could have killed him, judging he's a ghost...");
                    players[location].status = 41;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "cerulean" || target.ToLower() == "lordcerulean")
            {
                if (players[location].status == 48)
                {
                    await Context.Channel.SendMessageAsync("Lord Cerulean: What? A meeting? Oh, that was the ritual of RnG. It is a big cult we do every single year. We all gather in the center of Bit Valley, and RnG tells us and our minions what loot we give to the new travelers, and the ones visiting. Although, Grimz's minions were acting very, very strange during this.. I do not know why. You should check in with them.");
                    players[location].status = 49;
                }
                else if (players[location].status == 41)
                {
                    await Context.Channel.SendMessageAsync("Lord Cerulean: Welcome to my tomb. May I help you with anything? Astaroth? Why, I saw him about 4 days ago. Why? What happened? What? He's dead? How terrible! Hmm.. I saw Jack the other day too, and he seemed to act quite weird for a ghost pirate.. maybe see whats going on with that pile of bones.");
                    players[location].status = 42;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "jack")
            {
                if (players[location].status == 47)
                {
                    await Context.Channel.SendMessageAsync("Jack: Ye better not be talkin' t' be unless ye have me bombs, we'll keel-haul ye, and a bottle of rum! ye do! Yaaarrrrr! Thank ye so much, me bucko. Now. I'll be tellin' ye this, simple, yet crucial, fact. Everyone in zone one were bein' gatherin', about a day or so. Cerulean seemed t' be th' head o' this operation, and all o' them had their minions. I would suggest talkin' t' that scurvey dog about it, because 2 days later Astaroth were bein' poxed. Suspicious, right? Ye should talk t' that scurvey dog about it.");
                    players[location].status = 48;
                }
                else if (players[location].status == 42)
                {
                    await Context.Channel.SendMessageAsync("Jack: Blegh, by Woodbeard's bones. I dern't be knowin' anythin' about Astaroth. And swab the deck! I spit on that scurvey dog. Now get lost, before I have t' throw one o' me bombs at yo- wait. I dern't have any bombs.. Woodbeard di'nae get me supply yet, I'll warrant ye! Damn Corsair. Get me a bomb and I'll consider talkin' t' ye. I do know, that ye Shrump has me bombs. Go talk to him.");
                    players[location].status = 43;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "shrump")
            {
                if (players[location].status == 46)
                {
                    await Context.Channel.SendMessageAsync("Shrump: You have my tiny head? I hope Derwin didn't try to rob you? Almost? Well that's fine anyway. Wow, this is an extremely tiny head. It'll still do, just not the amount of bombs it would normally produce. And... there. 20 bombs for you. Enjoy.");
                    players[location].status = 47;
                }
                else if (players[location].status == 43)
                {
                    await Context.Channel.SendMessageAsync("Shrump: Don't sit on me!! I'm not a chair!! Oh. Ok. Bombs? I don't have bombs. I need a shrunken head to do that, and I've ran out. Derwin usually supplies me. But he needs to borrow a purple mushroom for his stew. I'll give you this, but make sure he gives me a head first. Then go over and talk to Kroger, who will shrink the head for you. Then i can make you a plenty amount of bombs.");
                    players[location].status = 44;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "derwin")
            {
                if (players[location].status == 44)
                {
                    await Context.Channel.SendMessageAsync("Derwin: Purple cat toes! You have a purple mushroom! I'll just take this from yo- what? You want something for it? Surely it can't be too costly. Alright. You want a shrunken head? Oh no no no, only Kroger can do that. But I have this, a normal head. Don't ask. Just go over to Kroger and he'll shrink the head for you. Eurika!");
                    players[location].status = 45;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "kroger")
            {
                if (players[location].status == 45)
                {
                    await Context.Channel.SendMessageAsync("Kroger: Heh! Do you have some head shrinking I need to do? Yourself will do nicely.. oh. No? You have a head? Well.. ok. Mentrion Roth! There. Shrunken. Using it as an accessory? Oh, going to Shrump for bombs I see. Careful, kid.");
                    players[location].status = 46;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "booboo")
            {
                if (players[location].status == 49)
                {
                    await Context.Channel.SendMessageAsync("Booboo: Boo! Heh! Did I get you? No? ..dangit! I'll get another person one day. What? You what to know why we were acting strange? Look at me! I'm hiding nothing up my sleeves. You can see right through me!");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "batty")
            {
                if (players[location].status == 52)
                {
                    await Context.Channel.SendMessageAsync("Batty: Oh, joy! You have my schematic?! Thank you so much! I'll tell you why I was acting weird. Someone hypnotized me! It was Quirrel! I remember now! He held up his staff.. did some weird tiki shake with that mask of his and boom! He controlled me to do whatever he wanted me to do. He said for me to sabotage Grimz for something, gain his power and give it to him.. something about taking over, I think. Anyway, take this. Its a mirror. If Quirrel sees himself, he goes into a weakening phase where he'll do anything to get it off. He cannot be free until you take the mirror off. Go talk to him again.");
                    players[location].status = 53;
                }
                else if (players[location].status == 49)
                {
                    await Context.Channel.SendMessageAsync("Batty: Why are you here?! Get out! This is our personal space! Listen, I'm really, really upset right now... someone has stolen my booty schematic... oh what would I do without it...");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "tubbo")
            {
                if (players[location].status == 49)
                {
                    await Context.Channel.SendMessageAsync("Tubbo: ...Oh god I really need to shave.. wait, what? You heard nothing! What you doing here? Acting strange? The only familiar here acting strange is batty. He lost his favorite booty schematic, and he won't talk to anyone until he gets it back... I would go to the town and talk to Quinn, who has all the schematics, but i'm pretty self conscious about my pits right now... maybe you can ask?");
                    players[location].status = 50;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "quinn")
            {
                if (players[location].status == 50)
                {
                    await Context.Channel.SendMessageAsync("Quinn: Get away from here dumb grasshopper! Unless you got a familiar to put in my stable, get out! What? You want a schematic? Hehe, I ran out today. But, I still have the special paper you use to make it... What kind of schematic? Common? Well that'll cost you 1,000 gold... Perfect! Here ya go. Tealk makes the schematics work. Just go talk to him, tell him you already payed me.");
                    players[location].status = 51;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "tealk")
            {
                if (players[location].status == 51)
                {
                    await Context.Channel.SendMessageAsync("Tealk: Mehhh... you're not Qwinn? What could I pawssibly do for you thats sho impawtant and a washte of my time? What? Shchematicsh? Huh.. so Quinn weally did send you. Alwight, I'll put the code onto the shchematic. Did you pay him though? How much? Oh, 1,000 gold. All wight. And who is this shchematic for? Booty? Alwight. That'll cost 100 gold for washting my time. Alwight, thank you. Now be gone, meatbag.");
                    players[location].status = 52;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "sammy")
            {
                if (players[location].status == 59)
                {
                    await Context.Channel.SendMessageAsync("Sammy: He actually gave you his silk?! Wow, thats really impressive. Well, here's the jar. Now I can have my bed... Oh boy! I heard this stuff is even softer than the temper pedic matresses! Here's your jar!");
                    players[location].status = 60;
                }
                else if (players[location].status == 57)
                {
                    await Context.Channel.SendMessageAsync("Sammy: Zzzzz... You want this jar? No way, loser! This makes some of the softest silk in the world. You're going to need to do something better than that. Actually... There is something. Nock's silk he uses to make his bows are a lot softer than mine... I don't know how he does it. If you get some of his silk, I'll give ya this jar, normie.");
                    players[location].status = 58;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "nock")
            {
                if (players[location].status == 82)
                {
                    await Context.Channel.SendMessageAsync("Nock: Back for more of my silk, are ya? I'm not gonna torture you this time. Just gonna give ya the rope. Here ya go. Have fun.");
                    players[location].status = 83;
                }
                else if (players[location].status == 58)
                {
                    await Context.Channel.SendMessageAsync("Nock: Stop right there, intruder! What are you doing here? To see me? Why? For some of my silk? I'll only give you a few strands. But you have to listen to me. You have to do the most dangerous, treacherous thing to get this silk. Just listen to my joke. Nock Nock, who's there! Hahaha! Alright. Here you go.");
                    players[location].status = 59;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "blargnar")
            {
                if (players[location].status == 62)
                {
                    await Context.Channel.SendMessageAsync("Blargnar: Hey bro! Welcome to the fusion dimension dude. How can I help ya? Astaroth is dead in your world? No way, dude! Thats totes not cool. Somethings going on with Olxaroth here, I think. I mean, I don't know why you would be interested little dude, but before I chow down on some crystals and get my spike game up, try talking to this weird green dude with a staff. I'm pretty sure they call him bob or mer'lan in your world. One of the two. Your world is weird.");
                    players[location].status = 63;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "borlan")
            {
                if (players[location].status == 63)
                {
                    await Context.Channel.SendMessageAsync("Borlan: Hello, human. Welcome to the world of the fusions. I see you picked up the amulet I left you? Its for humans like you. They don't want to be distorted in this world, because, well, there is no fusion in the entire realm that matches with humans. I was about to go to your dungeon, to check out some.. 'things'. You can search me - to my spells, books, anything. I have nothing. I'll blow up if I do have it before I go to your world. But, I was about to drop this off. You're powerful, and I'm defenseless right now. So just take this schematic, and run off. I have nothing else to do. Please don't hurt me. Do /schematic to read it.");
                    players[location].status = 64;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "korgz")
            {
                if (players[location].status == 68)
                {
                    await Context.Channel.SendMessageAsync("Korgz: You have my scythe, for real? Good job. Now here's the key. Careful though, that being in there is quite terrifying if you anger him. Krives can help you open the door.");
                    players[location].status = 69;
                }
                else if (players[location].status == 66)
                {
                    await Context.Channel.SendMessageAsync("Korgz: I have the key. Yeah. But what are you gonna do if I give it to ya? Nothin? Sorry bub, I don't roll that way. The only thing you can do is find me my rainbow scythe. Chances are Quirinus stole it. Go get my scythe if you want the key, nerd.");
                    players[location].status = 67;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "quirinus")
            {
                if (players[location].status == 67)
                {
                    await Context.Channel.SendMessageAsync("Quirinus: I don't have the scythe. Now run along, before I'm going to have to kill you. Wait, what are you doing with that mirr - AHH! GET THAT AWAY FROM ME! I HAVE THE SCYTHE! I HAVE THE SCYTHE! I'LL GIVE IT TO YOU! JUST GET THAT BLOOD CURSED THING AWAY FROM MY SIGHT!");
                    players[location].status = 68;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "krives")
            {
                if (players[location].status == 69)
                {
                    await Context.Channel.SendMessageAsync("Krives: Hey, good job. Now, if I can just open the door with the key... Ok perfect. Be careful in there, if you anger the floating pink one you're in for a tough time. Sheesh! Here's your key back. You might need it for later, I don't know for what though.");
                    players[location].status = 70;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "olxaroth")
            {
                if (players[location].status == 70)
                {
                    await Context.Channel.SendMessageAsync("Olxaroth: YIWV YD MOEE'C JIJB.");
                    players[location].status = 71;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "gemmi")
            {
                if (players[location].status == 75)
                {
                    await Context.Channel.SendMessageAsync("Gemmi: You have it! Ok, now I'll just fix it up... There. Now show this to Shade. Do /portal to enter the portal, but hurry before it closes.");
                    players[location].status = 76;
                }
                else if (players[location].status == 71)
                {
                    await Context.Channel.SendMessageAsync("Gemmi: Hello. What are you doing here? Better not say mean things or I'll call my dad and he'll beat you up! What? Astaroth is dead? He had it coming... I can tell you what I know. Someone named Shade in your world knows something.. I don't. Now, he won't talk unless you give him this orb... If only I can find it. Hey! Wait a bloody second! The hamburger stole it! Damn thief... Get it to me and I'll make it work for you!");
                    players[location].status = 72;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "boiguh" || target.ToLower() == "boi")
            {
                if (players[location].status == 74)
                {
                    await Context.Channel.SendMessageAsync("Boiguh: Oi! Boi! You got my bombs? Sweet, heres your stupid orb. Damn thing doesn't even work anymore. Haha, stick it boi!");
                    players[location].status = 75;
                }
                else if (players[location].status == 72)
                {
                    await Context.Channel.SendMessageAsync("Boiguh: What do you want, boi? Yea I stole Gemmi's orb! He's a little cryboi! What? Why do you want it back? Ohh, you want to harass him, right? Haha! Well, you might just give it to him... So I'll give it to you if you give me some of Jacked's bombs. They're like poprocks, but they are as addicting as cigarettes. Get me some, boi!");
                    players[location].status = 73;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "jacked")
            {
                if (players[location].status == 73)
                {
                    await Context.Channel.SendMessageAsync("Jacked: Ye want some o' me bombs? Walk the plank! I mean, Beido stocked me up on a lot, so I guess ye can have some o' me bombs.");
                    players[location].status = 74;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "nosdoodoo")
            {
                if (players[location].status == 78)
                {
                    await Context.Channel.SendMessageAsync("Nosdodoo: Nosdoodoo is dying... Nosdoodoo know nothing! Jeb knows all. Nosdoodoo give you Nosdoodoo's spike.. Jeb want Nosdoodoo spike for Jeb nail polish. Take spike from Nosdoodoo, and end Nosdoodoo misery. Goodbye.");
                    players[location].status = 79;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "jeb")
            {
                if (players[location].status == 79)
                {
                    await Context.Channel.SendMessageAsync("Jeb: Yes, I killed Nosdoodoo. He was the ugliest thing in Lakehaven! Not in a peaceful territory like this... The center lake is where you look at yourself to see if you are truly... Beautiful. Nosdoodoo can't see his own reflection. That means he's the ugliest in the entire realm of Lakehaven. You have his spike? Wonderful! If I can just take that to give myself a pedicure... No? What do you want? Fine, I guess I'll give you one of my claws. Its for beauty, isn't it! Of course it is. Here... The sharpest Jeb claw. Go cleanse it in the lake with /lake, you might see something. I don't know. Now, shoo!");
                    players[location].status = 80;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "zorul")
            {
                if (players[location].status == 83)
                {
                    await Context.Channel.SendMessageAsync("Zorul: Yesssssss, you come back withhhhhhh Zorul rope... Veryyyy goooooood... Zorul know thisssss... Torlim hiding... Torlimmmm wasssss parttt offff Astarottthhh killll... Torlimmm willll nottt tellllll youuuuu soooo easyyyyy... Youuuu neeeeeeed Quirrellll feather... Tickleeeee himmmmm and gettttttt himmmm to telllll youuuu...");
                    players[location].status = 84;
                }
                else if (players[location].status == 81)
                {
                    await Context.Channel.SendMessageAsync("Zorul: Yessssss I remember thissss claw... Thissss, thisss is jebssss claw? Jeb wanted to sssseeeee meee today, but Jeb no come to meeeee. Why Jeb no come to meeee? I hasssss informationnn for herrrrrrr. There isssss also secretttt language onnnn claw... I read to you... if youuu bring meeee Nock rope... Nock makesssss softtt rope.");
                    players[location].status = 82;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "torlim")
            {
                if (players[location].status == 88)
                {
                    await Context.Channel.SendMessageAsync("Torlim: Back from the portal little dude? Alright, go for it. Tell me anything. WHAT?!!? *hiss* I'M ALWAYS RIGHT! ARE YOU ACCUSING ME OF LYING?!?! I NEVER LIE! TORLIM DID NOT USE HIS POISON! MIMZY DID! MIMZY DISGUISED HIMSELF AS ME TO TAKE THE POISON, PUT IT IN REXIE'S JARS, AND PUT IT IN ASTAROTH'S DARK ENERGY PULSE. IT WAS NOT ME. NYXN HAS THE THINGS YOU NEED.");
                    players[location].status = 89;
                }
                else if (players[location].status == 85)
                {
                    await Context.Channel.SendMessageAsync("Torlim: What's up, little guy? What about my poison? I ain't poison no Astaroth! Was someone else. I ain't no snitch, though. You're gonna have to get me to talk someway else. What? NO!! Not the feather! I'll do anythi- HAHAHHAHAAHAHHAHAHAHA! OK! OK OK OK OK OK! I'LL TELL YOU! Puh.. Pengz wanted it.. y'know, Blubber's portal is pretty much dead so if you want to go between those trees there, and do /oogabooga, the portal should open up. Weird, I know, but Kroger is weird too.");
                    players[location].status = 86;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "pengz")
            {
                if (players[location].status == 87)
                {
                    await Context.Channel.SendMessageAsync("Pengz: Torlim accused me of poisoning Astaroth, in your world? I can't even access it if i brought anything. Derwin can't hook me up because they don't allow fusions in the market. He's probably lying to you. Go back to your world. You have no business here. He'll talk, if you push him. You probably tried a feather right? If you tell him he's not right, he'll go ballistic.");
                    players[location].status = 88;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
                }
            }
            else if (target.ToLower() == "nyxn")
            {
                if (players[location].status == 89)
                {
                    await Context.Channel.SendMessageAsync("Nyxn: Puny hero.. are you coming back for more of my trails? They're full, since there is no easy raid anymore.. You're gonna have to come in another time. What? You want to jail someone? Or kill them? Meh, I'll just give you these cuffs and Phobos. Phobos is an item that only stabs with proof - it will not work on a false person. Same with the cuffs. /handcuff or /kill - is your decision. Not mine.");
                    players[location].status = 90;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("...");
}
            }
        }

        [Command("sack")]
        public async Task sack()
        {
            int location = RefreshTarget(Context.User.Username);

            if (players[location].status == 25)
            {
                await Context.Channel.SendMessageAsync("You look inside Olxa's sack, and you see something glowing. When you pick it up, it looks like a leg schematic with some writing on it. The schematic says, 'Jivm laqp Fvqfweqp. Ymsegm celmwjiz.'. There's also some weird goo on the corner of the schematic...");
                players[location].status = 26;
            }
            else if (players[location].status == 2 || players[location].status == 3 || players[location].status == 4 || players[location].status == 5)
            {
                await Context.Channel.SendMessageAsync("You dig around Gobby's bag. It has a Robby schematic, which has a weird code that appears to be binary. /robby to view the code");
                players[location].status = 6;
            }
        }

        [Command("robby")]
        public async Task robby()
        {
            int location = RefreshTarget(Context.User.Username);

            if (players[location].status == 6)
            {
                await Context.Channel.SendMessageAsync("01000111 01101111 00100000 01110100 01101111 00100000 01010111 01101111 01101111 01100100 01100010 01100101 01100001 01110010 01100100 00100000 01100110 01101111 01110010 00100000 01101001 01101110 01110011 01110100 01100001 01101100 01101100 01101101 01100101 01101110 01110100 01110011 00101110 00100000 01010100 01100001 01101100 01101011 00100000 01110100 01101111 00100000 01010111 01101111 01101111 01100100 01100010 01100101 01100001 01110010 01100100 00101110");
                players[location].status = 7;
            }
        }

        [Command("cabinkey")]
        public async Task cabinkey()
        {
            int location = RefreshTarget(Context.User.Username);
            var user = Context.User as SocketGuildUser;

            if (players[location].status == 9)
            {
                await Context.Channel.SendMessageAsync("Cabin Key acquired! You now have access to the #woodbeards_cabin channel.");
                players[location].status = 10;
                await user.AddRoleAsync(Context.Guild.GetRole(339145788414230538));
            }
        }

        [Command("hyperactivate")]
        public async Task hyperactivate()
        {
            int location = RefreshTarget(Context.User.Username);

            if (players[location].status == 11)
            {
                await Context.Channel.SendMessageAsync("A pink goat? No, goats do not walk on two legs. A human? No, it's far too tall. This creature is neither a human nor a goat, yet it resembles both. You should try talking to him!");
                players[location].status = 12;
            }
        }

        [Command("tundra")]
        public async Task tundra()
        {
            int location = RefreshTarget(Context.User.Username);
            var user = Context.User as SocketGuildUser;

            if (players[location].status == 19)
            {
                await Context.Channel.SendMessageAsync("Tundra Ticket acquired! You now have access to the #yetis_tundra channel, and may talk to Yeti.");
                players[location].status = 20;
                await user.AddRoleAsync(Context.Guild.GetRole(339109894986661889));
            }
        }

        [Command("portal")]
        public async Task portal()
        {
            int location = RefreshTarget(Context.User.Username);

            if (players[location].status == 76)
            {
                await Context.Channel.SendMessageAsync("You enter the portal, clutching the orb in your hand. It glows with all sorts of colors, probably due to the climate change. You should go look for Shade.");
                players[location].status = 77;
            }
            if (players[location].status == 61)
            {
                await Context.Channel.SendMessageAsync("You notice that everything is different in this world. You find a mysterious amulet on the ground, next to a sign that says 'Wear this amulet if you don't want to become one too.' You pick it up, and wear it around your neck. A faint glow of amethyst starts to emerge from the amulet. You look around, almost in the exact same spot you were before, but the landscape is more cruel, and the sky is red. None of the familiars were, well, familiar, yet it seemed that they were fused with other familiars. You see something that looks like a blubber mixed with ragnar.");
                players[location].status = 62;
            }
        }

        [Command("schematic")]
        public async Task schematic()
        {
            int location = RefreshTarget(Context.User.Username);

            if (players[location].status == 64)
            {
                await Context.Channel.SendMessageAsync("'LAZZ NE QE SIMWFOM'V OIDNMED AD F OFT. GFXM VIWM QPM OEEWV FWM ZESXMO. - ZEWO SMWIZMFD' /oidnmed");
                players[location].status = 65;
            }
        }

        [Command("dungeon")]
        public async Task dungeon()
        {
            int location = RefreshTarget(Context.User.Username);

            if (players[location].status == 65)
            {
                await Context.Channel.SendMessageAsync("The door is locked, and there is a giant bubble which looks like it needs a Skeleton Key to unlock. You don't have one, because they are exterminated, and replaced with usually the head of the owner. Out of nowhere, a Krives appears. He said the door was locked for today, because he said a small demon with a rainbow scythe ran off with it.");
                players[location].status = 66;
            }
        }

        [Command("lake")]
        public async Task lake()
        {
            int location = RefreshTarget(Context.User.Username);

            if (players[location].status == 80)
            {
                await Context.Channel.SendMessageAsync("The calm breeze floats all around you, and a soft bell chimes within your ears. It is truly peaceful - No monsters, no yelling. Jut the soft waves of the rippling lake. It is crystal clear, almost like Nosdoodoo's eyes when you ripped the spike off of him. No, you must not think of that. Happy thoughts. You squat down, and wash the claw in the river. The claw becomes a bright yellow, and there are engravings inside of it. The claw reads - 'TaLK tO zORuL'");
                players[location].status = 81;
            }
        }

        [Command("oogabooga")]
        public async Task oogabooga()
        {
            int location = RefreshTarget(Context.User.Username);

            if (players[location].status == 86)
            {
                await Context.Channel.SendMessageAsync("A portal opened up! It's quite small, so you have to squeeze between the two trees. When you enter, it seems you are in a different part of the dimension, almost where everything is distorted and gravity doesn't exist. Island float, water floats diagonally. Weird. You search around, and find a penguin with a scythe and a beard... creepy.");
                players[location].status = 87;
            }
        }

        [Command("handcuff")]
        public async Task handcuff()
        {
            int location = RefreshTarget(Context.User.Username);

            if (players[location].status == 90)
            {
                await Context.Channel.SendMessageAsync("Mimzy: What?!? Take handcuffs off Mimzy! Mimzy is sorry for what Mimzy did! Mimzy no kill no more. Handcuffs on Mimzy? No? Ok.. Mimzy will own up to what Mimzy has done. Good work, Anti-Mimzy! But I still have to go to jail.. :(");
                await Context.Channel.SendMessageAsync("Congratulations, you have won! Do /finish to remove this channel.");
                players[location].status = -1;
            }
        }

        [Command("kill")]
        public async Task kill()
        {
            int location = RefreshTarget(Context.User.Username);

            if (players[location].status == 90)
            {
                await Context.Channel.SendMessageAsync("Mimzy: Mimzy has seen you are back, back to accuse Mimzy of kill Astaroth? MIMZY HAS HAD IT!!! MIMZY DID KILL!! MIMZY IS KILLER!! MIMZY HAS NO ATTENTION!!! MIMZY WANTS ALL ATTENTI-!!! *You stab Mimzy with the dagger. Mimzy lies dead.*");
                await Context.Channel.SendMessageAsync("Congratulations, you have won! Do /finish to remove this channel.");
                players[location].status = -1;
            }
        }

        [Command("finish")]
        public async Task finish()
        {
            int location = RefreshTarget(Context.User.Username);

            await channels[location].DeleteAsync();
            players.Remove(players[location]);
        }

        public int RefreshTarget(string username)
        {
            for(int i = 0; i < players.Count; i++)
            {
                if (players[i].name == username)
                {
                    return i;
                }
            }
            return -1;
        }

        public void AddPlayers(string username, int progress)
        {
            players.Add(new Player { name = username, status = progress });
        }
    }
}
