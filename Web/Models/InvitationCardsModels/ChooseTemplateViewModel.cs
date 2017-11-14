using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.Web.Models.InvitationCardsModels
{
    public class ChooseTemplateViewModel : InvitationViewModelBase
    {
        public IEnumerable<TemplateViewModel> AvailableTemplates { get; set; }

        public int SelectedTemplateId { get; set; }



        public class TemplateViewModel
        {
            public int Id { get; set; }

            public string ThumbnailUrl { get; set; }

            public string PreviewUrl { get; set; }

            public string TemplateUrl { get; set; }
        }
    }
}
