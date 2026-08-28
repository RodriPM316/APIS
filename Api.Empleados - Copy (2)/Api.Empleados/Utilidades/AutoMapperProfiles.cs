using Api.Empleados.DTOs;
using Api.Empleados.Entidades;
using AutoMapper;

namespace Api.Empleados.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Autor, AutorDTO>()
                .ForMember(dto => dto.NombreCompleto,
                           config => config.MapFrom(autor => MapearNombreCompleto(autor)));

            CreateMap<Autor, AutorConLibrosDTO>()
                .ForMember(dto => dto.NombreCompleto,
                           config => config.MapFrom(autor => MapearNombreCompleto(autor)));

            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorPatchDTO>().ReverseMap();

            CreateMap<AutorLibro, LibroDTO>()
                .ForMember(dto => dto.Id, config => config.MapFrom(ent => ent.LibroID))
                .ForMember(dto => dto.Titulo, config => config.MapFrom(ent => ent.Libro!.Titulo));

            CreateMap<Libro, LibroDTO>();   
            CreateMap<LibroCreacionDTO, Libro>()
                // 1. Evita que AutoMapper toque la llave primaria del libro original
                .ForMember(ent => ent.Id, opciones => opciones.Ignore())

                // 2. Mapea la relación de autores como ya lo tenías
                .ForMember(ent => ent.Autores, config => config.MapFrom(dto => dto.AutoresIds.Select(id => new AutorLibro() { AutorID = id })));

            CreateMap<Libro, LibroConAutoresDTO>();

            CreateMap<AutorLibro, AutorDTO>()
                .ForMember(dto => dto.Id, config => config.MapFrom(ent => ent.AutorID))
                .ForMember(dto => dto.NombreCompleto, 
                    config => config.MapFrom(ent => MapearNombreCompleto(ent.Autor!)));

            CreateMap<LibroCreacionDTO, AutorLibro>()
                .ForMember(ent => ent.Libro, 
                config => config.MapFrom(dto => new Libro { Titulo = dto.Titulo }));

            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();
            CreateMap<ComentarioPatchDTO, Comentario>().ReverseMap();
        }

        private string MapearNombreCompleto(Autor autor)
        {
            return $"{autor.Nombres} {autor.Apellidos}";
        }
    }
}
