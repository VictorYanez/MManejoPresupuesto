using AutoMapper;
using MManejoPresupuesto.Models;

namespace MManejoPresupuesto.Servicios
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cuenta, CuentaCreacionViewModel>();
        }
    }
}
