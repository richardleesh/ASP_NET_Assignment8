using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment8
{
    public static class AutoMapperConfig
    {
        public static IMapper RegisterMappings()
        {
            // AutoMapper create map statements - using AutoMapper instance API
            // new MapperConfiguration(cfg => cfg.CreateMap< FROM , TO >());
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
                // Add more below...
                cfg.CreateMap<Models.Artist, Controllers.ArtistBase>();
                cfg.CreateMap<Models.Artist, Controllers.ArtistWithDetails>();
                cfg.CreateMap<Controllers.ArtistAdd, Models.Artist>();
                cfg.CreateMap<Models.Album, Controllers.AlbumBase>();
                cfg.CreateMap<Models.Album, Controllers.AlbumWithDetails>();
                cfg.CreateMap<Controllers.AlbumAdd, Models.Album>();
                cfg.CreateMap<Models.Genre, Controllers.GenreBase>();
                cfg.CreateMap<Models.Track, Controllers.TrackBase>();
                cfg.CreateMap<Models.Track, Controllers.TrackWithDetails>();
                cfg.CreateMap<Controllers.TrackAdd, Models.Track>();
            });

            var mapper = config.CreateMapper();
            // or: IMapper mapper = new Mapper(config);
            return mapper;

        }
    }
}