using BusinessLogic.Models;

namespace WebSite.Models
{
    public class Mappings : AutoMapper.Profile
    {
        public Mappings()
        {
            CreateMap<Data, DataViewModel>()
                    .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                    .ForMember(d => d.Title, m => m.MapFrom(s => s.Title))
                    .ForMember(d => d.Comments, m => m.MapFrom(s => s.Comments))
                    .ForMember(d => d.DataUrl, m => m.MapFrom(s => s.DataUrl));
        }
    }
}
