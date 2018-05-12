namespace Pixel.FixaBarnkalaset.Core
{
    public class InvitationCardTemplate : Entity<int>
    {
        public string ThumbnailUrl { get; set; }

        public string PreviewUrl { get; set; }

        public string TemplateUrl { get; set; }

        public string HtmlTemplateText { get; set; }

        //public string ReviewTemplateUrl { get; set; }
    }
}
