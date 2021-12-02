using DSharpPlus.Entities;

namespace Amadeus.Bot.Resources;

public static class ArchiveParts
{
    public static string GetHeaderAndBodyPartOne(DiscordChannel c)
    {
        var styleStr = GetStyle();
        return $@"<!DOCTYPE html>
                <html lang=""en"">
                <head>
                <title>
                    {DateTime.Now:dd.MM.yyyy HH:mm} | {c.Name} (ID: {c.Id})
                </title>
                {styleStr}
                </head>
                <body>
                <header>
                <div id=""header-channel"">
                <a href=""https://discordapp.com/channels/{c.GuildId}/{c.Id}"">
                    Channel: {c.Name} (ID: {c.Id})
                </a>
                </div>
                <div id=""header-participants""><ul>";
    }

    public static string GetAuthorHtmlString(DiscordUser u)
    {
        return $@"<li><a href=""https://discordapp.com/users/{u.Id}"">
                    {u.Username} (ID: {u.Id})</a></li>{Environment.NewLine}";
    }

    public static string GetMessageHtmlString(DiscordMessage m)
    {
        return $@"<div class=""message"">
                <span class=""message_time"">{m.CreationTimestamp:dd.MM.yyyy HH:mm}</span>
                <span class=""message_author"">{m.Author.Username}</span>
                <span class=""message_text"">{m.Content}</span></div>";
    }

    public static string GetBridgeHtmlString()
    {
        return "</ul></div></header><section>";
    }

    public static string GetEndingHtmlString()
    {
        return "</section></body></html>";
    }

    private static string GetStyle()
    {
        return @"
            <style>
            html,body{
                margin: 0;
                background: #36393E;
                font-family: sans-serif;
                color: white;
            }
            a{
                color: white;
                text-decoration: none;
            }
            a:hover{
                text-decoration: underline;
            }             
            header{
                margin: 20px;
            }
            header div{
                margin: 10px;
            }
            #header-channel{
                font-size: 180%
            }
            section{
                margin: 20px;
            }
            section div{
                margin: 10px;
            }
            section span{
                margin-left: 10px;
            }
            .message_time, .message_author{
                color: rgba(255, 255, 255, 0.5)
            }
            .message_time{
                font-size: 75%
            }
            .message:hover > span{
                color: rgba(255, 255, 255, 1)
            }
            @media only screen and (max-width : 540px) {
                .message_text{
                    display: block;
                }
            }
            </style>";
    }
}