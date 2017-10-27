
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InBytesBot
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("hi are you diabetic");
            await context.PostAsync("What's your type?");
            context.Wait(ActivityReceivedAsync);
            var activity = await result as Activity;
           
        

        }
       
            private async Task ActivityReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            var reply = activity.CreateReply();
            reply.Attachments = new List<Attachment>();


            if (activity.Text.StartsWith("am type 1 what's my drugs"))
            {
                reply.Attachments.Add(new Attachment()
                {
                    ContentUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/93/Inzul%C3%ADn.jpg/800px-Inzul%C3%ADn.jpg",
                    ContentType = "image/png",
                    Name = "QualityControl.png"
                });

            }
            else if (activity.Text.StartsWith("am type 2 what's my drugs"))
            {
                HeroCard hc = new HeroCard()
                {
                    Title = "Here is your drug?",
                    Subtitle = "pills!"
                };
                List<CardImage> images = new List<CardImage>();
                CardImage ci = new CardImage("https://edrugsearch.com/wp-content/uploads/2016/12/glucophage.png");
                images.Add(ci);
                hc.Images = images;
                reply.Attachments.Add(hc.ToAttachment());

            }
            else if (activity.Text.StartsWith("help"))
            {
                List<CardImage> images = new List<CardImage>();
                CardImage ci = new CardImage("https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Blue_circle_for_diabetes.svg/2000px-Blue_circle_for_diabetes.svg.png");
                images.Add(ci);
                CardAction ca = new CardAction()
                {
                    Title = "Visit Support",
                    Type = "openUrl",
                    Value = "https://www.diabetes.org.uk/"
                };
                ThumbnailCard tc = new ThumbnailCard()
                {
                    Title = "Need help?",
                    Subtitle = "Go to our main site support.",
                    Images = images,
                    Tap = ca
                };
                reply.Attachments.Add(tc.ToAttachment());
            }
            

            await context.PostAsync(reply);
            context.Wait(ActivityReceivedAsync);
        }
    }
}