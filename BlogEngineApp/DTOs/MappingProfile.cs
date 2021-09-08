using BlogEngineApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.IO;

namespace BlogEngineApp.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<User, UserDTO>();

            CreateMap<UserDTO, User>();

            CreateMap<PostDTO, Post>();

            CreateMap<Post, PostDTO>()
                .ForMember(x => x.PostText, 
                y => y.MapFrom(z => File.ReadAllText(Directory.GetCurrentDirectory()+  
                "\\Archivos\\" + z.IdArchivos +"."+z.IdArchivosNavigation.Extension)));

        }
    }
}
